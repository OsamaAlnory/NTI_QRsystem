using NTI_QRsystem.Components;
using NTI_QRsystem.DBK;
using NTI_QRsystem.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace NTI_QRsystem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartSidan : ContentPage
	{
		public StartSidan ()
		{
			InitializeComponent ();
            img.Source = App.getImage("background");
            cp.Text = "Copyright © 2019 NTIGymnasiet all rights reserved.";
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Scanner();
                return false;
            }
            );
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Scanner();
        }
        public async void Scanner()
        {
            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) => {
            ScannerPage.IsScanning = false;

              
                Device.BeginInvokeOnMainThread(() => {
                     Navigation.PopAsync();
                    Recognize(result.Text);
                     
                });
            };


            await Navigation.PushAsync(ScannerPage);
        }

        private async void Recognize(string code)
        {
            var s = LoadingPage._a;
            var f = code.Split(' ');
            var c1 = f[0];
            TimeSpan d = TimeSpan.Parse(App.GetTime(DateTime.Now.TimeOfDay));
            bool found = false;
            for(int x = 0; x < DB.lectures.Count; x++)
            {
                Lecture lecture = DB.lectures[x];
                if(lecture.Rid == c1)
                {
                    found = true;
                    if(lecture.Class == s.Class)
                    {
                        TimeSpan clsstime = TimeSpan.Parse(f[1]);
                        var tt = App.GetTotalSeconds(d.Subtract(clsstime));
                        if (tt < App.REFRESH_TIME*2)
                        {
                             await DB.LoadInfos();
                            if (!DB.CheckStudent(s))
                            {
                                TimeSpan difference = App.GetTotalSeconds(d) > App.GetTotalSeconds(lecture.LecTime)
                                    ? d.Subtract(lecture.LecTime) : TimeSpan.Parse("00:00:00");
                                bool _A = App.GetTotalSeconds(difference) >= 60;
                                await DB.FullyAddInfo(new Info {LecId = lecture.Rid, Studentname = s.Username,
                                ATime= _A ? difference : TimeSpan.Parse("00:00:00")});
                                string l = "";
                                if(_A)
                                {
                                    l = "\nDu är "+ App.GetTime(difference) +" sen!";
                                }
                                Msg(new Popup(new SuccessMessage("Du har registererat klart din närvaro!" + l), this));
                                App.PlaySound("success");
                            } else
                            {
                                Msg(new Popup(new ErrorMessage("Du har redan registererat din närvaro!"), this));
                            }
                        } else
                        {


                            // Show Error / Try again //Fuskare
                            Msg(new Popup(new ErrorMessage("Du befinner inte dig på skolan, försök inte att fuska!"), this));
                        }
                    } else
                    {
                        // Not his class, kick his ass :'(

                        Msg(new Popup(new ErrorMessage("Det här är inte din klass!"), this));
                    }
                }
            }
            if (!found)
            {
                Msg(new Popup(new ErrorMessage("Ogiltig kod!"), TeacherPage.tp));
            }
        }

        private void Msg(Popup p)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                p.Show();
                return false;
            });
        }

        private void OpenAbout(object s, EventArgs e)
        {

        }

    }
}