using NTI_QRsystem.Components;
using NTI_QRsystem.DBK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : CarouselPage
	{
        private int fails, timeLeft;
        private bool clicked,_clicked;

		public LoginPage ()
		{
			InitializeComponent ();
            background1.Source = App.getImage("bg");
            background2.Source = App.getImage("bg2");
            background3.Source = App.getImage("bg");
            if (App.Current.Properties.ContainsKey("TimeLeft"))
            {
                int vr = int.Parse(App.Current.Properties["TimeLeft"] as string);
                if(vr > 0)
                {
                    timeLeft = vr;
                    lbl_time.Text = _G();
                    CountDown();
                }
            }
            Calc(username); Calc(password);
        }

        private void Calc(View e)
        {
            e.HeightRequest = App.ScreenHeight / 16;
            e.Margin = new Thickness(App.ScreenWidth / 30, 0, App.ScreenWidth / 30, 0);
        }

        private void RunTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                clicked = false;
                return false;
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (clicked || _clicked)
            {
                return;
            }
            clicked = true;
            RunTimer();
            if(timeLeft > 0)
            {
                new Popup(new ErrorMessage(_G()), this).Show();
                return;
            }
            string t1 = username.Text, t2 = password.Text;
            if (t1 == null || t2 == null)
            {
                new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
            }
            else
            {
                Start();
                var id = GetID.Get();
                for (int x = 0; x < DB.accounts.Count; x++)
                {
                    Account acc = DB.accounts[x];
                    if (acc.Username == t1 && acc.Password == t2)
                    {
                        if (!acc.isLogged)
                        {
                            Log(acc, id);
                            return;
                        }
                        if(acc.MobileID == null)
                        {
                            Log(acc, id);
                        } else
                        {
                            if(acc.MobileID == id)
                            {
                                Log(acc, id);
                            } else
                            {
                                new Popup(new ErrorMessage("Du är redan inloggad på en annan mobil!\nKontakta Rektorn"), this).Show();
                                Stop();
                            }
                        }
                        return;
                    } 
                }
                fails++;
                if(fails > 2)
                {
                    timeLeft = fails < 7 ? (fails-2) * 60 : 60*5;
                }
                CountDown();
                Stop();
                new Popup(new ErrorMessage("Fel användarnamn eller lösenord!"), this).Show();
            }
        }

        private async void Log(Account acc, string id)
        {
            Stop();
            _clicked = true;
            App.Current.Properties["LoggedIn"] = acc.Username;
            await App.Current.SavePropertiesAsync();
            acc.isLogged = true;
            acc.MobileID = id;
            await DB.EditAccount(acc);
            LoadingPage.p.OpenPage();
            Navigation.RemovePage(this);
        }

        private void Start()
        {
            an.IsVisible = true;
            an.Play();
        }

        private void Stop()
        {
            an.IsVisible = false;
            an.Pause();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            CurrentPage = this.Children[1];
        }

        private void CountDown()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                timeLeft--;
                if(timeLeft > 0)
                {
                    lbl_time.IsVisible = true;
                    lbl_time.Text = _G();
                    CountDown();
                } else
                {
                    lbl_time.IsVisible = false;
                }
                App.Current.Properties["TimeLeft"] = timeLeft+"";
                App.Current.SavePropertiesAsync();
                return false;
            });
        }

        private string _G()
        {
            var i = App.CountMinutes(timeLeft);
            var min = i > 1 ? "minuter" : "minut";
            return "Du måste vänta " + i +" "+min+" innan du kan logga in igen!";
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            CurrentPage = this.Children[2];
        }
    }
}