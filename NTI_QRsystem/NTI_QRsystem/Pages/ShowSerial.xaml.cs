﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NTI_QRsystem.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowSerial : ContentPage
    {
        public ShowSerial()
        {
            InitializeComponent();

            idnmr.Text = NTI_QRsystem.DBK.GetID.Default.DeviceId;
        }
    }
}