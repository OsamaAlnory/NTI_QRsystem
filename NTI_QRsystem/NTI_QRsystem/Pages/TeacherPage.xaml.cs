﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTI_QRsystem.Components;
using NTI_QRsystem.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherPage : TabbedPage
    {
        public static Lecture lec;
        public static TeacherPage tp;
        public static ObservableCollection<ListItem> list = new ObservableCollection<ListItem>();
        public static ListItem lastClicked = null;
        bool clicked = false;

        public TeacherPage()
        {
            tp = this;
            InitializeComponent();
            background5.Source = App.getImage("bg");
            bkg.Source = App.getImage("bg");
            lec = DB.GetLectureByTeacher(LoadingPage._a);
            Update();
            Timer();
            Schema();
        }

        private void Schema()
        {
            sch.ItemsSource = list;
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
            await DBK.LoadInfos();
            del_button.IsEnabled = true;
            var _s = new List<Studentinfo>();
            if(lec != null)
            {
                for (int i = 0; i < DBK.accounts.Count; i++)
                {
                    var ac = DBK.accounts[i];
                    if (!ac.isAdmin)
                    {
                        if (ac.Class == lec.Class)
                        {
                            var b = DBK.CheckStudent(ac, lec);
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

        private void sch_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var _item = e.Item as ListItem;
            if (lastClicked != null)
            {
                lastClicked.ItemColor = App.UNCLICKED;
                RefreshList(lastClicked);
                if(_item == lastClicked)
                {
                    lastClicked = null;
                    btn_r.IsEnabled = false;
                    return;
                }
            }
            _item.ItemColor = Color.Blue;
            RefreshList(_item);
            lastClicked = _item;
            btn_r.IsEnabled = true;
        }

        public void DisableButton()
        {
            btn_r.IsEnabled = false;
        }

        private void RefreshList(ListItem litem)
        {
            var _index = SchematicPanel._Get(litem);
            list.RemoveAt(_index);
            list.Insert(_index, litem);
        }

        private void btn_r_Clicked(object sender, EventArgs e)
        {
            if (clicked)
            {
                return;
            }
            clicked = true;
            RunTimer();
            new Popup(new SchematicPanel(lastClicked), this).Show();
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            if (clicked)
            {
                return;
            }
            clicked = true;
            RunTimer();
            new Popup(new SchematicPanel(null), this).Show();
        }

        private void RunTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                clicked = false;
                return false;
            });
        }

        public static void RefreshData()
        {
            App.Current.Properties["Schema"] = ConvertSchemaToString();
            App.Current.SavePropertiesAsync();
        }

        public static string ConvertSchemaToString()
        {
            string res = "";
            for(int x = 0; x < list.Count; x++)
            {
                var _a = list[x];
                if(x > 0)
                {
                    res += ";";
                }
                res += _a.LecDay + "," + _a.LecTime + "," + _a.Class + "," + _a.Room;
            }
            return res;
        }

    }
}