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
            background.Source = App.getImage("background");
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            string a1 = username.Text, a2 = password.Text;
            if(a1 == null || a2 == null)
            {
                DisplayAlert("Fel","WTF","Avbryt");
            } else
            {

            }
        }
    }
}