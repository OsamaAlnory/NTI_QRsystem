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
    public partial class AddStudents : ContentPage
    {
        public AddStudents()
        {
            InitializeComponent();
            image1.Source = App.getImage("bg");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string el1 = usnm.Text, el2 = uspassword.Text, el3 = uscs.Text, el4 = prnr.Text;




            if ( string.IsNullOrEmpty(el1) || string.IsNullOrEmpty(el2) || string.IsNullOrEmpty(el3) || string.IsNullOrEmpty(el4))
            {
                new Popup(new ErrorMessage("Fyll i alla fälten!"), this).Show();
            }
            else
            {
                for (int i = 0; i < DB.accounts.Count; i++)
                {
                    var accs = DB.accounts[i];
                    if (accs.Username == el1 || accs.Pnumber == el4)
                    {
                        new Popup(new ErrorMessage("Kontot finns Redan!"), this).Show();
                        return;
                    }
                }


                await DB.AddAccount(new Account() { Username = el1, Password = el2, Class = el3, Pnumber = el4, isAdmin = false });
                new Popup(new SuccessMessage("Kontot har Lagts till "), this).Show();

            }
        }
    }
}