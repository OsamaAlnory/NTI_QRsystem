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
	public partial class QR_Generator : ContentPage
	{
        public Random random = new Random();
        public const int TIMER = 1;

		public QR_Generator ()
		{
			InitializeComponent ();
            b.BarcodeOptions.Width = 300;
            b.BarcodeOptions.Height = 300;
            b.BarcodeValue = "www.google.com" + random.Next(10000);
            Device.StartTimer(TimeSpan.FromSeconds(TIMER), () => {
                b.BarcodeValue = "www.google.com"+random.Next(10000);
                //stkqr.Children.Insert(1, Barcode);
                return true;
            });
        }
	}
}