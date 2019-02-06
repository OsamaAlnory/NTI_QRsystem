using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using NTI_QRsystem;
using NTI_QRsystem.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IEntry), typeof(IEntryRenderer))]
namespace NTI_QRsystem.iOS
{
    public class IEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}