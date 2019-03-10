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

[assembly: ExportRenderer(typeof(IPicker), typeof(CustomPicker))]
namespace NTI_QRsystem.iOS
{
    public class CustomPicker : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
                Control.TextColor = UIColor.Black;

            }
        }
    }
}