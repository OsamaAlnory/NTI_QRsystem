using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using NTI_QRsystem;
using NTI_QRsystem.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Brightness_IOS))]
namespace NTI_QRsystem.iOS
{
    public class Brightness_IOS : Brightness
    {
        public void SetScreenBrightness(float value)
        {
            UIScreen.MainScreen.Brightness = value;
        }
    }
}