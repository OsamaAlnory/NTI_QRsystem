using Android.OS;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;


namespace NTI_QRsystem.DBK
{
     public class GetID
     {
        private static GetID _Default;
        public static GetID Default
        {
            get
            {
                return _Default ?? (_Default = new GetID());
            }
        }

        /// <summary>
        /// Gets a device unique identifier
        /// </summary>
        public string DeviceId
        {
            get
            {
                return GetDeviceIdInternal();
            }
        }

        /// <summary>
        /// Gets a device unique identifier depending on the platform
        /// </summary>
        /// <returns>string representing the unique id</returns>
        private string GetDeviceIdInternal()
        {
            var id = default(string);
#if __IOS__
            id = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();

            
         
#elif WINDOWS_PHONE
            id = Windows.Phone.System.Analytics.HostInformation.PublisherHostId;
#else

            // id = Android.OS.Build.Serial;

          id= UIDevice.CurrentDevice.IdentifierForVendor.ToString();
#endif



            return id;
        }
    }
}
