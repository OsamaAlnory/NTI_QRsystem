using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using NTI_QRsystem.DBK;
using NTI_QRsystem.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IDGetterIOS))]
namespace NTI_QRsystem.iOS
{
    public class IDGetterIOS : IDGetter
    {
        public string Get()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        }
    }
}