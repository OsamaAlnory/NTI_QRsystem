using NTI_QRsystem.Components;
using NTI_QRsystem.DBK;
using NTI_QRsystem.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace NTI_QRsystem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTeachers : ContentPage
	{
       

        public AddTeachers ()
		{
			InitializeComponent ();
            image1.Source = App.getImage("bg");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var el1 = usnm.Text; var el2 = uspassword.Text;
             
            if ( el1 != null  && el2 != null)
            {
                if (DB.getAccountByName(el1) == null)
                {
                    await DB.AddAccount(new Account() { Username = el1, Password = el2, isAdmin = true });
                    new Popup(new SuccessMessage("Kontot har Lagts till "), this).Show();
                }
                else
                {
                     
                  new Popup(new ErrorMessage("Kontot finns Redan!"), this).Show();
                    
                }
            }
            else
            {
                new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
            }
        }
    }
}