using NTI_QRsystem.DBK;
using NTI_QRsystem.Pages;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LectionCreator : StackLayout, CustomPopup, PopupComponent
	{
        private static Random r = new Random();
        private static string chrs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private bool clicked;

		public LectionCreator ()
		{
			InitializeComponent ();
            {
                var lst = new List<string>();
                lst.Add("Välj en sal.");
                for (int x = 0; x < DB.accounts.Count; x++)
                {
                    var a = DB.accounts[x];
                    if (a.Class == "Device" && !lst.Contains(a.Class))
                    {
                        lst.Add(a.Username);
                    }
                }
                sals.ItemsSource = lst;
                sals.SelectedIndex = 0;
            }
            {
                var lst = new List<string>();
                lst.Add("Välj en klass.");
                for (int x = 0; x < DB.accounts.Count; x++)
                {
                    var a = DB.accounts[x];
                    if (!a.isAdmin && !lst.Contains(a.Class))
                    {
                        lst.Add(a.Class);
                    }
                }
                clss.ItemsSource = lst;
                clss.SelectedIndex = 0;
            }
            if(TeacherPage.list.Count > 0)
            {
                DateTime _date = DateTime.Now;
                var _day = _date.DayOfWeek;
                var _time = TimeSpan.Parse(App.GetTime(_date.TimeOfDay));
                for (int x = 0; x < TeacherPage.list.Count; x++)
                {
                    var _item = TeacherPage.list[x];
                    if (IDay.GetDayByTranslatedDay(_item.LecDay) == _day
                        && App.GetTotalSeconds(TimeSpan.Parse(_item.LecTime).Subtract(_time))
                        <= 60*5)
                    {
                        clss.SelectedItem = _item.Class;
                        sals.SelectedItem = _item.Room;
                        time.Time = TimeSpan.Parse(_item.LecTime);
                        break;
                    }
                }
            }
        }

        public void ChangeFrame(Frame frame)
        {
            
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (clicked)
            {
                return;
            }
            if(sals.SelectedIndex > 0 && clss.SelectedIndex > 0)
            {
                if(TeacherPage.lec == null)
                {
                    clicked = true;
                    var l = DB.CheckLecture(sals.SelectedItem.ToString());
                    if (l == null)
                    {
                        Lecture lec = new Lecture
                        {
                            AdminID = LoadingPage._a.Username,
                            Class = clss.SelectedItem.ToString(),
                            DeviceID = sals.SelectedItem.ToString(),
                            LecTime = time.Time,
                            Rid = GenerateRandomId(), Extra=""
                        };
                        await DB.FullyAddLecture(lec);
                        TeacherPage.lec = lec;
                        await Navigation.PopPopupAsync();
                        new Popup(new SuccessMessage("Du har just skapat en lektion i sal "
                            +sals.SelectedItem.ToString()+"!"), TeacherPage.tp).Show();
                        TeacherPage.tp.Update(true);
                    } else
                    {
                        new Popup(new ErrorMessage("Det finns redan en lektion skapad i denna sal!"), TeacherPage.tp).Show();
                    }
                }
                else
                {
                    new Popup(new ErrorMessage("Du har redan skapat en lektion i denna sal!"), TeacherPage.tp).Show();
                }
            } else
            {
                new Popup(new ErrorMessage("Fyll i alla fälten."), TeacherPage.tp).Show();
            }
        }

        private static string GenerateRandomId()
        {
            string a = "";
            for(int x = 0; x < 2; x++)
            {
                a += chrs.ElementAt(r.Next(chrs.Length));
            }
            for(int x = 0; x < 3; x++)
            {
                a += r.Next(10);
            }
            for(int x = 0; x < 2; x++)
            {
                a += chrs.ElementAt(r.Next(chrs.Length));
            }
            for(int x = 0; x < 2; x++)
            {
                a += r.Next(10);
            }
            return a;
        }

        public PopupType GetPopupType()
        {
            return PopupType.INPUT;
        }

        public void ChangeAnimation(ScaleAnimation animation)
        {
            
        }

        public void OnClosed()
        {
            
        }
    }
}