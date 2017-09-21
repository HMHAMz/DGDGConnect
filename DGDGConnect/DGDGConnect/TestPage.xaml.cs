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
        public TestPage()
        {
            InitializeComponent();
            /* Method: TestPage
             * Content from External Resource: https://github.com/xamarin/xamarin-forms-samples/blob/master/UserInterface/Layout/LayoutSamples/ScrollingDemoCode.cs
             * Description: This piece of code helped my discover that InitializeComponent() should be removed, lest content be overridden by the XAML
            Title = "ScrollView Demo - C#";
            var scroll = new ScrollView();
            var label = new Label { Text = "Position" };
            var target = new Entry();
            var stack = new StackLayout();
            scroll.Content = stack;
            stack.Children.Add(label);
            stack.Children.Add(new BoxView { BackgroundColor = Color.Red, HeightRequest = 600, WidthRequest = 150 });
            stack.Children.Add(target);
            Content = scroll;
            scroll.ScrollToAsync(target, ScrollToPosition.Center, true);
            scroll.Scrolled += (object sender, ScrolledEventArgs e) => {
                label.Text = "Position: " + e.ScrollX + " x " + e.ScrollY;
            };*/
            //DisplayAlert("Alert", "Grid row definitions check 2: " + QuizViewGrid.RowDefinitions.Count(), "OK"); //! Debug code, to be removed
            //var QuizScrollView = new ScrollView();

            //var QuizStack = new StackLayout();

            //QuizScrollView.Content = QuizStack;

            //QuizStack.Children.Add(QuizViewGrid); //Add the menu list view to the Menu View Stack

            //QuizStack.Children.Add(new Button { Text = "Button " });
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
            /* Test GET Code: 
            SlowWebHandler WebHandler = new SlowWebHandler();
            WebQueryObj TestObj = new WebQueryObj();
            String testString = WebHandler.GetHTMLResponse(TestObj);
            DisplayAlert("Alert", "Test get: " + testString, "OK");*/
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