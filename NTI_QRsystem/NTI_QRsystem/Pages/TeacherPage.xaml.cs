using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTI_QRsystem.Components;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherPage : ContentPage
    {
        public TeacherPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            new Popup(new ErrorMessage("Fel din jävel!!"), this, PopupType.ERROR).Show();
        }
    }
}