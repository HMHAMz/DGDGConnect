using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DGDGConnect
{
   
    public partial class MainPage : ContentPage
    {
        ActiveProfile testP;
        public MainPage()
        {
            testP = ActiveProfile.Instance;
            InitializeComponent();
        }

        void JustButtonMethod(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CallerTutorialPage());
        }

        async void RootLogin(object sender, EventArgs e)
        {

            await DisplayAlert("Alert", "Username test: " + testP.GetName() , "OK");
            testP.SetName("Edit Test Name");
            //await Navigation.PushAsync(new QuizHome { Title = "Quiz Home" });
            await Navigation.PushAsync(new TestPage { Title = "Test Page" });
        }

    }

    

}
