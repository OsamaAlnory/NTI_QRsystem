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
	public partial class LoadingPage : ContentPage
	{
        public static Account _a;
        public static LoadingPage p;

        public LoadingPage ()
		{
            p = this;
			InitializeComponent ();
            if (App.CheckInternetConnection())
            {
                an.Play();
                load();
            } else
            {
                DisplayAlert("Fel", "No internet connection!", "Avbryt");
            }
		}

        private async void load()
        {
            await DB.LoadAccounts();
            if (App.Current.Properties.ContainsKey("LoggedIn"))
            {
                OpenPage();
            } else
            {
                Navigation.PushAsync(new LoginPage());
            }
        }

        public void OpenPage()
        {
            _a = DB.getAccountByName(App.Current.Properties["LoggedIn"] as string);
            if (_a.isAdmin)
            {

            }
            else
            {
                Navigation.PushAsync(new StartSidan());
            }
        }

    }
}