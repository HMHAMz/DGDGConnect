using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DGDGConnect
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        /* Class:        TestPage
        *  Programmer:   Harry Martin
        *  Dependencies: ActiveProfile, UserProfile, LoadResourceText, CryptoHandler, Jsonparser
        *  Description:  This content page is for debugging and code-testing purposes
        *                and should not be included in any public build 
        */

        public TestPage()
        {
            InitializeComponent();
        }

        async void QuizNav(object sender, EventArgs e) //Quiz Page Navigation
        {
            await Navigation.PushAsync(new QuizHome { Title = "Quiz Home" });
        }

        async void Exit(object sender, EventArgs e) //Back to Root Navigation
        {
            await Navigation.PopToRootAsync();
        }

        void TestSingleton() 
        {
            /* Test Profile Singleton Code: */
            UserProfile testP = ActiveProfile.Instance;
            DisplayAlert("Alert", "Username test: " + testP.username, "OK");
        }

        void TestHTTP()
        {
            /* Test HTTP GET Code */
            String testString = LoadResourceText.GetNetwork("http://introtoapps.com/datastore.php?action=list&appid=12345678", "GET");
            DisplayAlert("Alert", "Test get: " + testString, "OK");
        }

        void TestHash()
        {
            /* Test Hash Code: */
            String password = Test3Entry.Text;
            String hashed = CryptoHandler.Hash(password);
            DisplayAlert("Alert", "Hashed Password: " + hashed, "OK");
        }

        void TestUserLoad()
        {
            /* Test User Loading Code: */
            String userJson = LoadResourceText.GetLocal("DGDGConnect.user_sample_harry.json");
            UserProfile test = JsonParser.ParseToUser(userJson);
            DisplayAlert("Alert", "Loaded user password: " + test.password, "OK");
        }
    }
}