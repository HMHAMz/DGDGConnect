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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        async void QuizNav(object sender, EventArgs e) //Quiz Page Navigation
        {
            await Navigation.PushAsync(new QuizHome { Title = "Quiz Home" });
        }

        async void TestNav(object sender, EventArgs e) //Quiz Page Navigation
        {
            await Navigation.PushAsync(new TestPage { Title = "Testing Page" });
        }

        async void AdminNav(object sender, EventArgs e) //Quiz Page Navigation
        {
            await Navigation.PushAsync(new AdminPage { Title = "Quiz Admin Page" });
        }

        async void Exit(object sender, EventArgs e) //Back to Root Navigation
        {
            ActiveProfile.UnloadProfile();
            await Navigation.PopToRootAsync();
        }

    }
}