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
                    mycode.Text = result.Text;
                });
            };

        }
    }
}