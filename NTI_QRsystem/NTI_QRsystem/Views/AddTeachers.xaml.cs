using NTI_QRsystem.Components;
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
            string el1 = usnm.Text, el2 = uspassword.Text;
           


           
            if (el1 == null && el2 == null)
            {
                new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
            }
            else
            {
                for (int i = 0; i < DBK.accounts.Count; i++)
                {
                    DB.Account accs = DBK.accounts[i];
                    if (accs.Username == el1)
                    {
                        new Popup(new ErrorMessage("Kontot finns Redan!"), this).Show();
                        return;
                    }
                }
                 
                 
                await DBK.AddAccount(new DB.Account (){Username = el1, Password = el2, isAdmin = true });
                new Popup(new SuccessMessage("Kontot har Lagts till "), this).Show();







            }
        }
    }
}