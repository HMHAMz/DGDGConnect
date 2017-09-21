using Newtonsoft.Json;
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
        UserProfile testP;
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
            //Get the values from the user prompts
            String w_username = UserN.Text;
            String w_password = PassW.Text;

            //Write these to a local user profile object
            UserProfile local_UP = new UserProfile();
            local_UP.username = w_username;
            local_UP.password = CryptoHandler.Hash(w_password);
            //await DisplayAlert("Alert", "Password Hash: " + local_UP.password, "OK");
            String userJson = WebMethod.GetResult("load", "users/" + local_UP.username, "");
            UserProfile v_test = JsonParser.ParseToUser(userJson);

            //DEBUG: await DisplayAlert("Alert", "Comparing: \n" + local_UP.password + "\n and: \n" + v_test.password, "OK");

            switch (ActiveProfile.LoadProfile(local_UP))
            {
                case (0):
                    await DisplayAlert("Alert", "Account not found.", "OK");
                    break;
                case (1):
                    await Navigation.PushAsync(new MenuPage { Title = "Main Menu" });
                    //await Navigation.PushAsync(new TestPage { Title = "Test Page" });
                    break;
                case (2):
                    await DisplayAlert("Alert", "Login Failed.\nWrong password.", "OK");
                    break;
            }            
        }

        async void RootCreate(object sender, EventArgs e)
        {
            //Clear current loaded user (if any)
            ActiveProfile.UnloadProfile();

            //Get the values from the user prompts
            String w_username = UserN.Text;
            String w_password = PassW.Text;

            //Write these to a local user profile object
            UserProfile local_UP = ActiveProfile.Instance;
            local_UP.username = w_username;
            local_UP.password = CryptoHandler.Hash(w_password);

            //Serialize and encode the object
            String enc_json = WebMethod.Serialize(local_UP);

            //DEBUG: await DisplayAlert("Alert", "About to attempt following POST:  \n" + enc_json, "OK");
            //Do the HTTP request and get the result
            String createResult = WebMethod.GetResult("save", "users/" + w_username, enc_json, "POST");

            if (createResult == "ok")
            {
                await DisplayAlert("Alert", "User succesfully created.", "OK");
            } else {
                await DisplayAlert("Alert", createResult, "OK");
            }
        }

    }

    

}
