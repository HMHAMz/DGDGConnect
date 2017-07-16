using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using DGDGConnect.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace DGDGConnect.iOS
{
    public class PhoneDialer : IDialer
    {
        public bool Dial (string number)
        {
            return UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + number));
        }
    }
}