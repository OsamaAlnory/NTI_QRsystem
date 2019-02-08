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

		public ConfirmationMessage (string msg)
		{
			InitializeComponent ();
            lbl.Text = msg;
		}

        public PopupType GetPopupType()
        {
            return PopupType.INFO;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (clicked)
            {
                return;
            }
            if(TeacherPage.lec != null)
            {
                clicked = true;
                await Pages.DBK.FullyRemoveLecture(TeacherPage.lec);
                TeacherPage.lec = null;
                await Navigation.PopPopupAsync();
                new Popup(new SuccessMessage("Lektionen har avslutats!"), TeacherPage.tp).Show();
                TeacherPage.lec = null;
                TeacherPage.tp.Update(true);
            }
        }
    }
}