using NTI_QRsystem.DB;
using NTI_QRsystem.Pages;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
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
        public static Dictionary<string, string> strings = new Dictionary<string, string>();
        public static Dictionary<string, ISimpleAudioPlayer> sounds = new Dictionary<string, ISimpleAudioPlayer>();
        public const int QR_SIZE = 1000;
        private const string PATH = "NTI_QRsystem";
        public const int REFRESH_TIME = 3;
        public const int FAKE_REFRESH_TIME = 800;

        public App()
        {
            InitializeComponent();
            images.Add("background", loadImage("background.png"));
            images.Add("bg", loadImage("bg.png"));
            images.Add("bg2", loadImage("bg2.JPG"));
            RegisterSound("success", "scan_success.wav");
            RegisterSound("s","s.wav");
            //App.Current.Properties.Remove("LoggedIn");
            //App.Current.SavePropertiesAsync();

            //MainPage = new NavigationPage(new LoadingPage());
            
            MainPage = new Pages.RectorPage();
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

        public static void PlaySound(string key)
        {
            sounds[key].Play();
        }

        private static void RegisterSound(string key, string path)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.
               GetManifestResourceStream(PATH + ".sounds." + path);
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);
            sounds.Add(key, player);
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
            var h = t.Hours > 9 ? "" + t.Hours : "0" + t.Hours;
            var m = t.Minutes > 9 ? "" + t.Minutes : "0" + t.Minutes;
            var s = t.Seconds > 9 ? "" + t.Seconds : "0" + t.Seconds;
            return h + ":" + m + ":" + s;
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
