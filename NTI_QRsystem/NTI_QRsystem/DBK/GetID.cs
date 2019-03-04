using Android.OS;
using Foundation;
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
        //private static GetID _Default;
        //public static GetID Default
        //{
        //    get
        //    {
        //        return _Default ?? (_Default = new _GetID());
        //    }
        //}

        public static string Get()
        {
            var ider = DependencyService.Get<IDGetter>();
            return ider?.Get();
        }

        /// <summary>
        /// Gets a device unique identifier depending on the platform
        /// </summary>
        /// <returns>string representing the unique id</returns>
        public string GetDeviceIdInternal()
        {
           var id = default(string);
            
            
             
              
            //Ios
                //id = UIDevice.CurrentDevice.IdentifierForVendor.ToString();
             
             //Android
                // id = Build.Serial;
                
             
          
            return id;
        }
    }
}
