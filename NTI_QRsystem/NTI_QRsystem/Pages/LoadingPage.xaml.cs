using NTI_QRsystem.Components;
using NTI_QRsystem.DBK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;

namespace NTI_QRsystem.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPage : ContentPage
	{
        public static Account _a;
        public static LoadingPage p;

        public LoadingPage ()
		{
            p = this;
			InitializeComponent ();
            backgrounds1.Source = App.getImage("bg");
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                an.Play();
                if (App.CheckInternetConnection())
                {
                    load();
                }
                else
                {
                    an.Pause();
                    an.IsVisible = false;
                    new Popup(new ErrorMessage("Se till att din mobil är ansluten till internet."), this).Show();
                }
                return false;
            });
		}

        private async void load()
        {
            await DB.LoadAccounts();
            await DB.LoadLectures();
            await DB.LoadInfos();
            if (App.Current.Properties.ContainsKey("LoggedIn"))
            {
                var nm = App.Current.Properties["LoggedIn"] as string;
                Account acc = DB.getAccountByName(nm);
                if(acc != null && acc.isLogged)
                {
                   // OpenPage();
                    //return;
                }
            }
            
            Navigation.PushAsync(new LoginPage());
        }

        public void OpenPage()
        {
            var lg = App.Current.Properties["LoggedIn"] as string;
            _a = DB.getAccountByName(lg);
            if (_a.isAdmin)
            {
                if (DB.IsDevice(_a))
                {
                    Navigation.PushAsync(new QRScreen());
                } else
                {
                    Navigation.PushAsync(new TeacherPage());
                }
            }
            else
            {
                 
               Navigation.PushAsync(new StartSidan());
               
            }
        }

    }
}