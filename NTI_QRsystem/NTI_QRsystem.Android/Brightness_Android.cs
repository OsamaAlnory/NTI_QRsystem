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
using NTI_QRsystem;
using NTI_QRsystem.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(Brightness_Android))]
namespace NTI_QRsystem.Droid
{
    public class Brightness_Android : Brightness
    {
        public void SetScreenBrightness(float brightness)
        {
            var window = CrossCurrentActivity.Current.Activity.Window;
            var attributesWindow = new WindowManagerLayoutParams();
            attributesWindow.CopyFrom(window.Attributes);
            attributesWindow.ScreenBrightness = brightness;
            window.Attributes = attributesWindow;
        }
    }
}