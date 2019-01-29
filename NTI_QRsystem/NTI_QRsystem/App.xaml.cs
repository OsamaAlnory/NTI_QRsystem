using NTI_QRsystem.DBK;
using NTI_QRsystem.Pages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NTI_QRsystem
{
    public partial class App : Application
    {

        public static Dictionary<string, ImageSource> images = new Dictionary<string, ImageSource>();
        public const int QR_SIZE = 300;
        private const string PATH = "NTI_QRsystem";
        public const int REFRESH_TIME = 5;
        public const int FAKE_REFRESH_TIME = 800;

        public App()
        {
            InitializeComponent();
            images.Add("background", loadImage("background.png"));
            //DB.AddLecture(new Lecture { AdminID="Osama", Class="2_TEK", DeviceID="EK",
            //Rid="EB12L",LecTime=DateTime.Now,Extra="test"});
            //App.Current.Properties.Remove("LoggedIn");
            //App.Current.SavePropertiesAsync();
            MainPage = new NavigationPage(new Pages.LoadingPage());
        }

        public static bool CheckInternetConnection()
        {
            string CheckUrl = "http://google.com";
            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 8000;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        public static ImageSource getImage(string key)
        {
            return images.ContainsKey(key) ? images[key] : loadImage("no-image.png");
        }

        public static ImageSource loadImage(string path)
        {
            return ImageSource.FromResource(PATH+".images." + path,
             Assembly.GetExecutingAssembly());
        }

        public static string GetTime(TimeSpan t)
        {
            return t.Hours + ":" + t.Minutes + ":" + t.Seconds;
        }

        public static int GetTotalSeconds(TimeSpan t)
        {
            return Math.Abs(t.Hours * 3600 + t.Minutes * 60 + t.Seconds);
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
