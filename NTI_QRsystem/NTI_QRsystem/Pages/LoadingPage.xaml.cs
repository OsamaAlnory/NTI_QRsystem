﻿using NTI_QRsystem.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;
using NTI_QRsystem.DBK;

namespace NTI_QRsystem.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPage : ContentPage
	{
        public static Account _a;
        public static LoadingPage p;
        public static bool A = false;

        public LoadingPage ()
		{
            p = this;
			InitializeComponent ();
            backgrounds1.Source = App.getImage("bg");
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                A = true;
                Check();
                return false;
            });
		}

        public void Check()
        {
            an.IsVisible = true;
            an.Play();
            if (App.CheckInternetConnection())
            {
                load();
            }
            else
            {
                an.Pause();
                an.IsVisible = false;
                new Popup(new RetryPopup("Se till att din mobil är ansluten till internet."
                    , () => {
                        Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                            Check();
                            return false;
                        });
                    }), this).Show();
            }
        }

        private async void load()
        {
            A = false;
            if(App.Current.Properties.ContainsKey("Schema"))
            {
                var _zx = (App.Current.Properties["Schema"] as string).Split(';');
                for(int x = 0; x < _zx.Length; x++)
                {
                    var _ab = _zx[x].Split(',');
                    if(_ab.Length == 4)
                    {
                        TeacherPage.list.Add(new ListItem
                        {
                            LecDay = _ab[0],
                            LecTime = _ab[1],
                            Class = _ab[2],
                            Room = _ab[3], ItemColor=App.UNCLICKED
                        });
                    }
                }
            }
            await DB.LoadAccounts();
            await DB.LoadLectures();
            await DB.LoadInfos();
            for(int x = 0; x < DB.infos.Count; x++)
            {
                var _ = DB.infos[x];
                if(DB.GetLectureById(_.LecId) == null)
                {
                    await DB.RemoveInfo(_);
                }
            }
            //for(int x = 0; x < DB.accounts.Count; x++)
            //{
            //    var s = DB.accounts[x]; s.isLogged = false; await DB.EditAccount(s);
            //}
            var id = GetID.Get();
            var D = DB.CheckMobileID(id);
            if (App.Current.Properties.ContainsKey("LoggedIn"))
            {
                var nm = App.Current.Properties["LoggedIn"] as string;
                Account acc = DB.getAccountByName(nm);
                if (acc != null && acc.isLogged)
                {
                    if(acc.MobileID != null && acc.MobileID == id)
                    {
                        OpenPage();
                        return;
                    }
                }
            } else
            {
                if (D != null && D.isLogged)
                {
                    App.Current.Properties["LoggedIn"] = D.Username;
                    await App.Current.SavePropertiesAsync();
                    //if (!D.isLogged) {
                    //    D.isLogged = true;
                    //    await DB.EditAccount(D);
                    //}
                    OpenPage();
                    return;
                }
            }
            Navigation.PushAsync(new LoginPage());
        }

        public void OpenPage()
        {
            var lg = App.Current.Properties["LoggedIn"] as string;
            val.Text = "Välkommen tillbaka\n" + lg + "!";
            an.Pause();
            an.Animation = "success.json";
            an.Loop = false;
            an.Play();
            WaitAndOpen(lg);
        }

        private void WaitAndOpen(string lg)
        {
            Device.StartTimer(TimeSpan.FromSeconds(2), () => {
                _a = DB.getAccountByName(lg);
                if (_a.isAdmin)
                {
                    if(_a.Username == App.R_NAME)
                    {
                        Navigation.PushAsync(new RectorPage());
                    } else
                    {
                        if (DB.IsDevice(_a))
                        {
                            Navigation.PushAsync(new QRScreen());
                        }
                        else
                        {
                            Navigation.PushAsync(new TeacherPage());
                        }
                    }
                    
                }
                else
                {
                    Navigation.PushAsync(new StartSidan());
                }
                Navigation.RemovePage(this);
                return false;
            });
        }

    }
}