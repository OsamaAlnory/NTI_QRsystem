using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SuccessMessage : StackLayout, PopupComponent
	{
		public SuccessMessage (string message)
		{
			InitializeComponent ();
            lbl.Text = message;
		}

        public PopupType GetPopupType()
        {
            return PopupType.INFO;
        }

        public void OnClosed()
        {
            animation.Pause();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            OnClosed();
            await Navigation.PopPopupAsync();
        }
    }
}