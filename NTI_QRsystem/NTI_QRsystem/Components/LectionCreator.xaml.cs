using NTI_QRsystem.DB;
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
                for (int x = 0; x < Pages.DBK.accounts.Count; x++)
                {
                    var a = Pages.DBK.accounts[x];
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
                for (int x = 0; x < Pages.DBK.accounts.Count; x++)
                {
                    var a = Pages.DBK.accounts[x];
                    if (!a.isAdmin && !lst.Contains(a.Class))
                    {
                        lst.Add(a.Class);
                    }
                }
                clss.ItemsSource = lst;
                clss.SelectedIndex = 0;
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
                    var l = Pages.DBK.CheckLecture(sals.SelectedItem.ToString());
                    if (l == null)
                    {
                        Lecture lec = new Lecture
                        {
                            AdminID = LoadingPage._a.Username,
                            Class = clss.SelectedItem.ToString(),
                            DeviceID = sals.SelectedItem.ToString(),
                            LecTime = time.Time,
                            Rid = GenerateRandomId()
                        };
                        await Pages.DBK.FullyAddLecture(lec);
                        TeacherPage.lec = lec;
                        await Navigation.PopPopupAsync();
                        new Popup(new SuccessMessage("Du har just skapat en lektion i sal "
                            +sals.SelectedItem.ToString()+"!"), TeacherPage.tp).Show();
                        TeacherPage.tp.Update(true);
                    } else
                    {
                         new Popup(new ErrorMessage("Du har redan skapat en lektion i denna sal!"), TeacherPage.tp).Show();
                    }
                }
                else
                {
                    new Popup(new ErrorMessage("Fyll i alla fälten."), TeacherPage.tp).Show();
                }
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
    }
}