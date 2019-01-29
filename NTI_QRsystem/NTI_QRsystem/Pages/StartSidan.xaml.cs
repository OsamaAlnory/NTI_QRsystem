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
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Scanner();

            //Old code!!
            //var scan = new ZXingScannerPage();
            //await Navigation.PushAsync(scan);
            //scan.OnScanResult += (result) =>
            //{
            //    Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        await Navigation.PopAsync();
            //        try
            //        {
            //            Recognize(result.Text);
            //        } catch(Exception ex)
            //        {
            //            DisplayAlert("dwa", ""+ex.Message, "Avbr");
            //        }
            //    });
            //};
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

        private void Recognize(string code)
        {
            var s = LoadingPage._a;
            var f = code.Split(' ');
            var c1 = f[0];
            TimeSpan d = TimeSpan.Parse(App.GetTime(DateTime.Now.TimeOfDay));
            for(int x = 0; x < DB.lectures.Count; x++)
            {
                Lecture lecture = DB.lectures[x];
                if(lecture.Rid == c1)
                {
                    if(lecture.Class == s.Class)
                    {
                        TimeSpan clsstime = TimeSpan.Parse(f[1]);
                        var tt = App.GetTotalSeconds(d.Subtract(clsstime));
                        if (tt < App.REFRESH_TIME*2)
                        {
                            // Success
                            DisplayAlert("Success", "You've just!", "Ok");
                        } else
                        {
                            DisplayAlert("Error", "Fel "+tt+" "+d+" "+clsstime
                                +" "+d.Subtract(clsstime), "Avbryt");
                            // Show Error / Try again
                        }
                    } else
                    {
                        // Not his class, kick his ass :'(
                        DisplayAlert("Fel", "Detta är inte din klass!", "Avbryt");
                    }
                }
            }
        }

        private void OpenAbout(object s, EventArgs e)
        {

        }

    }
}