using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTI_QRsystem.Components;
using NTI_QRsystem.DBK;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherPage : ContentPage
    {
        public  TeacherPage()
        {
            InitializeComponent();
            background5.Source = App.getImage("bg");

            var Students = new List<Studentinfo>
            {
                new Studentinfo("Mohanad","Ek",DateTime.Now.TimeOfDay.ToString()),
                 //new Studentinfo("Linus","Mallala"),
                 // new Studentinfo("Suleiman","Ek"),
                 //  new Studentinfo("Josef","Ek"),
                 //   new Studentinfo("Sara","Ek"),
                 //    new Studentinfo("Matias","Ek"),
                 //     new Studentinfo("Isak","Ek"),
            };
            elever.ItemsSource = Students;
            //Connect the listview ***
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            new Popup(new ErrorMessage("Fel din jävel!!"), this, PopupType.ERROR).Show();
        }
    }
}