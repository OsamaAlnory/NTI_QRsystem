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
	public partial class SuccessMessage : StackLayout
	{
		public SuccessMessage ( string message)
		{

			InitializeComponent ();
            lbl.Text = message;

		}

        private async Task Button_Clicked (object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}