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

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDevice))]
namespace NTI_QRsystem.Droid
{
   public class AndroidDevice : IDevice 
    {
        public string GetIdentifier()
        {
            // return  Settings.Secure.GetString(Forms.Context.ContentResolver, Settings.Secure.AndroidId);
            return null;
        }
    }
}