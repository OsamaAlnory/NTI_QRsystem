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
                                TimeSpan difference = d.Subtract(lecture.LecTime);
                                await DB.FullyAddInfo(new Info {LecId = lecture.Rid, Studentname = s.Username,
                                ATime=difference});
                                string l = "";
                                if(App.GetTotalSeconds(difference) >= 60)
                                {
                                    l = "\nDu är "+App.GetTime(difference)+" sen!";
                                }
                                new Popup(new SuccessMessage("Du har registererat klart din närvaro!"+l), this).Show();
                            } else
                            {
                                new Popup(new ErrorMessage("Du har redan registererat din närvaro!"), this).Show();
                            }
                        } else
                        {
                            

                            // Show Error / Try again //Fuskare
                            new Popup(new ErrorMessage("Du befinner inte dig på skolan, försök inte att fuska!"), this).Show();
                        }
                    } else
                    {
                        // Not his class, kick his ass :'(
                        
                        new Popup(new ErrorMessage("Det här är inte din klass!"), this).Show();
                    }
                }
            }
            if (!found)
            {
                new Popup(new ErrorMessage("Ogiltig kod!"), TeacherPage.tp).Show();
            }
        }

        private void OpenAbout(object s, EventArgs e)
        {

        }

    }
}