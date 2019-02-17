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

namespace NTI_QRsystem.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAccounts : ContentPage
	{
		public EditAccounts ()
		{
			InitializeComponent ();
            image1.Source = App.getImage("bg");
		}



        private async void Button_Clicked(object sender, EventArgs e)
        {
         string el1 = usnm.Text, el2 = uspassword.Text, el3 = uscs.Text, el4 = uspnr.Text, el5 = usmid.Text;

             if (el1 == null && el2 == null && el2 == null && el3 == null && el4 == null && el5 == null)
             {
                new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
             }
            else
            {
                await DB.EditAccount(new Account() { Username = el1, Password = el2, Class = el3,
                Pnumber = el4, MobileID = el5});
                new Popup(new SuccessMessage("Kontot har Lagts till "), this).Show();
            }
        }
    }
}