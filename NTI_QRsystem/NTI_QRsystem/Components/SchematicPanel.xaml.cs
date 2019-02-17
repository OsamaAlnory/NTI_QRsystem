using NTI_QRsystem.DBK;
using NTI_QRsystem.Pages;
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
	public partial class SchematicPanel : StackLayout, PopupComponent
	{
        public static string[] d = { "Lördag","Söndag","Mondag","Tisdag","Onsdag","Torsdag",
        "Fredag"};
        private ListItem item;

		public SchematicPanel (ListItem item)
		{
            this.item = item;
			InitializeComponent ();
            if(item != null)
            {
                btn_1.IsVisible = true;
                lbl_main.Text = "Redigera schemat";
            }
            var days = new List<string>();
            if(item == null)
            {
                days.Add("Välj en dag.");
            }
            foreach(var v in d){days.Add(v);}
            iday.ItemsSource = days;
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
                iroom.ItemsSource = lst;
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
                iclass.ItemsSource = lst;
                if (item != null)
                {
                    iday.SelectedIndex = GetDayIndex(item.LecDay);
                    if(iclass.ItemsSource.Count > 0)
                    {
                        iclass.SelectedIndex = iclass.ItemsSource.IndexOf(item.Class);
                    }
                    if (iroom.ItemsSource.Count > 0)
                    {
                        iroom.SelectedIndex = iroom.ItemsSource.IndexOf(item.Room);
                    }
                    itime.Time = TimeSpan.Parse(item.LecTime);
                }
                else
                {
                    iday.SelectedIndex = 0;
                    iroom.SelectedIndex = 0;
                    iclass.SelectedIndex = 0;
                }
            }
		}

        private int GetDayIndex(string day)
        {
            for(int x = 0; x < d.Length; x++)
            {
                if(d[x] == day)
                {
                    return x;
                }
            }
            return -1;
        }

        public PopupType GetPopupType()
        {
            return PopupType.INPUT;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(item == null && (iday.SelectedIndex == 0 || iclass.SelectedIndex == 0
                || iroom.SelectedIndex == 0))
            {
                new Popup(new ErrorMessage("Fyll i alla fälten."), TeacherPage.tp).Show();
                return;
            }
            if(item == null)
            {
                for (int x = 0; x < TeacherPage.list.Count; x++)
                {
                    var _a = TeacherPage.list[x];
                    if (_a.LecDay == iday.SelectedItem.ToString() && _a.LecTime ==
                        App.GetTime(itime.Time, false))
                    {
                        new Popup(new ErrorMessage("Det finns redan en lektion med samma tid!"), TeacherPage.tp).Show();
                        return;
                    }
                }
            }
            ListItem newItem = new ListItem { LecDay = iday.SelectedItem.ToString(),
            LecTime=App.GetTime(itime.Time,false),ItemColor=item!=null?item.ItemColor:App.UNCLICKED,
            Class=iclass.SelectedItem.ToString(),Room=iroom.SelectedItem.ToString()};
            if(item != null)
            {
                var _index = _Get(item);
                TeacherPage.list.RemoveAt(_index);
                TeacherPage.list.Insert(_index, newItem);
                TeacherPage.lastClicked = newItem;
            } else
            {
                TeacherPage.list.Add(newItem);
            }
            Navigation.PopPopupAsync();
            TeacherPage.RefreshData();
        }

        private void btn_1_Clicked(object sender, EventArgs e)
        {
            if(TeacherPage.lastClicked != null)
            {
                TeacherPage.list.Remove(TeacherPage.lastClicked);
                TeacherPage.lastClicked = null;
                TeacherPage.tp.DisableButton();
                Navigation.PopPopupAsync();
                TeacherPage.RefreshData();
            }
        }

        public static int _Get(ListItem item)
        {
            for(int x = 0; x < TeacherPage.list.Count; x++)
            {
                var a = TeacherPage.list[x];
                if (a.LecDay == item.LecDay && a.LecTime == item.LecTime)
                {
                    return x;
                }
            }
            return -1;
        }

    }
}