using NTI_QRsystem.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace NTI_QRsystem.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QRScreen : ContentPage
	{
        public Random random = new Random();
        private string code = "none";

		public QRScreen ()
		{
			InitializeComponent ();
            b.BarcodeOptions.Width = App.QR_SIZE;
            b.BarcodeOptions.Height = App.QR_SIZE;
            b.BarcodeValue = code;
            Load();
            Device.StartTimer(TimeSpan.FromMilliseconds(App.FAKE_REFRESH_TIME), () => {
                b.BarcodeValue = code+" "+random.Next(99);
                l.Text = b.BarcodeValue;
                return true;
            });
            Device.StartTimer(TimeSpan.FromSeconds(App.REFRESH_TIME), () => {
                A();
                return true;
            });
        }

        private async void Load()
        {
            await A();
        }

        private async Task A()
        {
            await DB.LoadInfos();
            await DB.LoadLectures();
            var lec = DB.GetLecByDevice(LoadingPage._a);
            if(lec != null)
            {
                lec.Extra = App.GetTime(DateTime.Now.TimeOfDay);
                code = lec.Rid.Trim()+" "+lec.Extra.Split(' ')[0];
                info.Text = lec.Class;
                stk_qr.IsVisible = true;
                stk.IsVisible = false;
                an_no.Pause();
                await DB.EditLec(lec);
            } else
            {
                stk_qr.IsVisible = false;
                stk.IsVisible = true;
                an_no.Play();
            }
        }
    }
}