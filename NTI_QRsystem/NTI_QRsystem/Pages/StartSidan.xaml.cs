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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    Recognize(result.Text);
                });
            };
        }

        private void Recognize(string code)
        {
            var s = LoadingPage._a;
            var c1 = code.Split(' ')[0];
            TimeSpan d = TimeSpan.Parse(App.GetTime(DateTime.Now.TimeOfDay));
            for(int x = 0; x < DB.lectures.Capacity; x++)
            {
                Lecture lecture = DB.lectures[x];
                if(lecture.Rid == c1)
                {
                    if(lecture.Class == s.Class)
                    {
                        TimeSpan clsstime = TimeSpan.Parse(lecture.Extra.Split(' ')[0]);
                        if (App.GetTotalSeconds(d.Subtract(clsstime)) < 10)
                        {
                            // Success
                            DisplayAlert("Success", "You've just!", "Ok");
                        } else
                        {
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