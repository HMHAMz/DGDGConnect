using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DGDGConnect
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallerTutorialPage : ContentPage
    {
        string translatedNumber;

        public CallerTutorialPage()
        {
            InitializeComponent();
        }

        /*   EXTERNAL CODE
            URL: https://developer.xamarin.com/guides/xamarin-forms/getting-started/hello-xamarin-forms/quickstart/
            AUTHOR: developer.xamarin.com
        */
        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = Core.PhoneTranslator.ToNumber(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    dialer.Dial(translatedNumber);
                }
            }
        }
    }
}