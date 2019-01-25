using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NTI_QRsystem
{
    public partial class App : Application
    {

        public static Dictionary<string, ImageSource> images = new Dictionary<string, ImageSource>();

        public App()
        {
            InitializeComponent();
            Load();
            images.Add("background", loadImage("background.png"));
              //MainPage = new NavigationPage(  new StartSidan());
            MainPage = new NavigationPage(new Pages.LoginPage());
           //MainPage = new QR_Generator();
        }

        private async void Load()
        {
            await DBK.DB.LoadAccounts();
        }

        public static ImageSource getImage(string key)
        {
            return images.ContainsKey(key) ? images[key] : loadImage("no-image.png");
        }

        public static ImageSource loadImage(string path)
        {
            return ImageSource.FromResource("NTI_QRsystem.images." + path,
             Assembly.GetExecutingAssembly());
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
