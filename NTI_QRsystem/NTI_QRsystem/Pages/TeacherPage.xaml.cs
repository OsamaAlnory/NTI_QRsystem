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
    public partial class TeacherPage : TabbedPage
    {

        public static Lecture lec;
        public static TeacherPage tp;

        public TeacherPage()
        {
            tp = this;
            InitializeComponent();
            background5.Source = App.getImage("bg");
            lec = DB.GetLectureByTeacher(LoadingPage._a);
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

        public void Update()
        {
            Update(false);
        }

       public async void Update(bool force)
        {
            if(lec == null && !force)
            {
                del_button.IsEnabled = false;
                return;
            }
            await DB.LoadInfos();
            del_button.IsEnabled = true;
            var _s = new List<Studentinfo>();
            if(lec != null)
            {
                for (int i = 0; i < DB.accounts.Count; i++)
                {
                    var ac = DB.accounts[i];
                    if (!ac.isAdmin)
                    {
                        if (ac.Class == lec.Class)
                        {
                            var b = DB.CheckStudent(ac, lec);
                            string str = b != null ? b.ATime.ToString() : "Frånvarande";
                            _s.Add(new Studentinfo
                            {
                                Studentname = ac.Username,
                                ATime = str,
                                color = b != null ? Color.Green : Color.Red
                            });
                        }
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
            new Popup(new LectionCreator(), this).Show();
        }

        private void del_button_Clicked(object sender, EventArgs e)
        {
            new Popup(new ConfirmationMessage("Är du säker på att avsluta denna lektion?"), this).Show();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.PlaySound("success");
        }
    }
}