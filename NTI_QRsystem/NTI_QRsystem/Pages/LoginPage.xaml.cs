﻿using NTI_QRsystem.DBK;
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
            string t1 = username.Text, t2 = password.Text;
            if (t1 == "" || t2 == "")
            {
                DisplayAlert("Error!", "Fyll i fälten!", "Avbryt");
            }
            else
            {
                for (int x = 0; x < DB.accounts.Count; x++)
                {
                    Account acc = DB.accounts[x];
                    if (acc.Username == t1 && acc.Password == t2)
                    {
                        App.Current.Properties["LoggedIn"] = acc.Username;
                        App.Current.SavePropertiesAsync();
                        LoadingPage.p.OpenPage();
                        Navigation.RemovePage(this);
                        return;
                    }
                }
                DisplayAlert("Fel","Fel användarnamn eller lösenord!","Avbryt");
            }
        }

    }
}