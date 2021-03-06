﻿using NTI_QRsystem.MenuItems;
using NTI_QRsystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RectorPage : MasterDetailPage
    {
        public List<MasterPageItems> menuList { get; set; }

        public RectorPage()
        {
            InitializeComponent();
            masterbg.Source = App.getImage("bg2");
            menuList = new List<MasterPageItems>();

            // Adding menu items to menuList and you can define title ,page and icon
            menuList.Add(new MasterPageItems() { Title = "Lägga till Lärare", Icon = "setting.png", TargetType = typeof(AddTeachers) });
            menuList.Add(new MasterPageItems() { Title = "Lägga till Elerver", Icon = "home.png", TargetType = typeof(AddStudents) });
            menuList.Add(new MasterPageItems() { Title = "Redigera Konton", Icon = "help.png", TargetType = typeof(EditAccounts) });
            
     

            // Setting our list to be ItemSource for ListView in RectorPage.xaml
            NavigationList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Views.AddTeachers)));

            
        }
       
        

        private void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItems)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}