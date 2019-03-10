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
        
        private Account f_acc = null;
		public EditAccounts ()
		{
			InitializeComponent ();
            image1.Source = App.getImage("bg");
            Calc(usnm); Calc(uspassword); Calc(uscs); Calc(uspnr);
		}

        private void Calc(View e)
        {
            e.HeightRequest = App.ScreenHeight / 16;
            e.Margin = new Thickness(App.ScreenWidth / 30, 0, App.ScreenWidth / 30, 0);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            f_acc.isLogged = false;
            await DB.EditAccount(f_acc);
            Logutbtn.IsEnabled = false;

        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string el1 = usnm.Text, el2 = uspassword.Text, el3 = uscs.Text, el4 = uspnr.Text;
            for (int i = 0; i < DB.accounts.Count; i++)
            {
                var _ax = DB.accounts[i];
                if (src.Text == _ax.Username)
                {
                    f_acc = _ax;
                     break;
                }
            }
            if (f_acc != null)
            {
                if (f_acc.isAdmin)
                {
                    h1.IsVisible = false; h2.IsVisible = false;
                }
                else
                {
                    h1.IsVisible = true; h2.IsVisible = true;
                }

               usnm.Text = f_acc.Username;
               uspassword.Text = f_acc.Password;
               uscs.Text = f_acc.Class;
               uspnr.Text = f_acc.Pnumber;
               stk.IsVisible = true;
               Logutbtn.IsVisible = true;
               redigbtn.IsVisible = true;
                if (f_acc.isLogged)
                {
                    Logutbtn.IsEnabled = true;
                } else
                {
                    Logutbtn.IsEnabled = false;
                }
            }
            else
            {
              
                new Popup(new ErrorMessage("Kontot finns inte!"), this).Show();
            }
        }

        private async void Redigbtn_Clicked(object sender, EventArgs e)
        {
            string el1 = usnm.Text, el2 = uspassword.Text, el3 = uscs.Text, el4 = uspnr.Text;
            if (f_acc.isAdmin)
            {
                if (string.IsNullOrEmpty(el1) && string.IsNullOrEmpty(el2))
                {
                    h1.IsVisible = false; h2.IsVisible = false;
                    new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
                }
                else
                {
                    f_acc.Username = el1; f_acc.Password = el2;
                    await DB.EditAccount(f_acc);
                    new Popup(new SuccessMessage("Uppgifterna har ändrats!"), this).Show();

                }
               
                
            }
            else
            {
                
                if (!f_acc.isAdmin)
                {
                    if (string.IsNullOrEmpty(el1) && string.IsNullOrEmpty(el2) && string.IsNullOrEmpty(el3) && string.IsNullOrEmpty(el4))
                    {
                       
                        new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
                        return;

                    }
                    else
                    {
                        h1.IsVisible = true; h2.IsVisible = true;
                        f_acc.Username = el1; f_acc.Password = el2; f_acc.Class = el3; f_acc.Pnumber = el4;
                        await DB.EditAccount(f_acc);
                        new Popup(new SuccessMessage("Uppgifterna har ändrats!"), this).Show();
                    }
                }
                return;
            }
           


        }
    }
}