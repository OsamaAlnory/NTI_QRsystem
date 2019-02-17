using NTI_QRsystem.Pages;
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
	public partial class ConfirmationMessage : StackLayout, PopupComponent
	{
        bool clicked;
        Action onAccept;
        Action onCancel;

		public ConfirmationMessage (string msg, Action onAccept, Action onCancel)
		{
			InitializeComponent ();
            this.onAccept = onAccept;
            this.onCancel = onCancel;
            lbl.Text = msg;
		}

        public PopupType GetPopupType()
        {
            return PopupType.INFO;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (clicked)
            {
                return;
            }
            clicked = true;
            onCancel?.Invoke();
            await Navigation.PopPopupAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (clicked)
            {
                return;
            }
            clicked = true;
            onAccept?.Invoke();
            await Navigation.PopPopupAsync();
        }
    }
}