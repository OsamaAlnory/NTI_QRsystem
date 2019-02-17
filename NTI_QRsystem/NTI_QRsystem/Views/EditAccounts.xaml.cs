﻿using NTI_QRsystem.Components;
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
            Account _f = null;
            for (int i = 0; i < DB.accounts.Count; i++)
            {
                var _ax = DB.accounts[i];
                if (src.Text == _ax.Username)
                {
                    _f = _ax;
                    f_acc = _ax;
                     break;
                 }
            }
            if (_f != null)
            {
                if (_f.isAdmin)
                {
                    h1.IsVisible = false; h2.IsVisible = false;
                }
                else
                {
                    h1.IsVisible = true; h2.IsVisible = true;
                }

               usnm.Text = _f.Username;
               uspassword.Text = _f.Password;
               uscs.Text = _f.Class;
               uspnr.Text = _f.Pnumber;
                stk.IsVisible = true;
                Logutbtn.IsVisible = true;
                redigbtn.IsVisible = true;
                if (_f.isLogged)
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
            if ( string.IsNullOrEmpty(el1) && string.IsNullOrEmpty(el2))
            {
                bool t = true;
                if (!f_acc.isAdmin)
                {
                    if(string.IsNullOrEmpty(el3) || string.IsNullOrEmpty(el4))
                    {
                        t = false;
                    }
                }
                if (t)
                {
                    f_acc.Username = el1; f_acc.Password = el2; f_acc.Class = el3; f_acc.Pnumber = el4;
                    await DB.EditAccount(f_acc);
                    new Popup(new SuccessMessage("Uppgifterna har ändrats!"), this).Show();
                    return;
                }
            }
            new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();


        }
    }
}