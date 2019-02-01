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

        private Lecture lec;

        public  TeacherPage()
        {
            InitializeComponent();
            background5.Source = App.getImage("bg");
            lec = DB.lectures[0];
            Update();
            Timer();
        }

        private void Timer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                Update();
                return true;
            });
        }

       private async void Update()
        {
            await DB.LoadInfos();
            if(lec == null)
            {
                return;
            }
            var _s = new List<Studentinfo>();
            for (int i = 0; i < DB.accounts.Count; i++)
            {
                var ac = DB.accounts[i];
                if (!ac.isAdmin)
                {
                    if(ac.Class == lec.Class)
                    {
                        var b = DB.CheckStudent(ac, lec);
                        string str = b != null ? b.ATime.ToString() : "Frånvarande";
                        _s.Add(new Studentinfo { Studentname = ac.Username, ATime = str,
                            color = b != null ? Color.Green: Color.Red });
                    }
                }
            }
            elever.ItemsSource = _s;
            if(_s.Count > 0)
            {
                no.IsVisible = false;
                elever.IsVisible = true;
            } else
            {
                no.IsVisible = true;
                elever.IsVisible = false;
            }
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            //Skapa Lektioner
            new Popup(new ErrorMessage("Fel din jävel!!"), this, PopupType.ERROR).Show();
        }
    }
}