using NTI_QRsystem.Components;
using NTI_QRsystem.DBK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : CarouselPage
	{

		public LoginPage ()
		{
			InitializeComponent ();
            background1.Source = App.getImage("bg");
            background2.Source = App.getImage("bg2");
            background3.Source = App.getImage("bg");
           // background5.Source = App.getImage("bg");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string t1 = username.Text, t2 = password.Text;
            if (t1 == null || t2 == null)
            {
                new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
            }
            else
            {
                for (int x = 0; x < DB.accounts.Count; x++)
                {
                    Account acc = DB.accounts[x];
                    if (acc.Username == t1 && acc.Password == t2)
                    {
                        App.Current.Properties["LoggedIn"] = acc.Username;
                        await App.Current.SavePropertiesAsync();
                        acc.isLogged = true;
                        await DB.EditAccount(acc);
                        LoadingPage.p.OpenPage();
                        Navigation.RemovePage(this);
                        return;
                    } 
                }
                new Popup(new ErrorMessage("Fel användarnamn eller lösenord!"), this).Show();
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            CurrentPage = this.Children[1];
        }
    }
}