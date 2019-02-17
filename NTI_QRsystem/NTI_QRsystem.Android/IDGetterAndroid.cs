using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NTI_QRsystem.DBK;
using NTI_QRsystem.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(IDGetterAndroid))]
namespace NTI_QRsystem.Droid
{
    public class IDGetterAndroid : IDGetter
    {
        public string Get()
        {
            return Build.Serial;
        }
    }
}