using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NTI_QRsystem
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Load();
              MainPage = new NavigationPage(  new StartSidan());
            //MainPage = new Pages.LoginPage();
           // MainPage = new QR_Generator();
        }

        private async void Load()
        {
            await DBK.DB.LoadAccounts();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
