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
        /* Class:      MainPage
        * Programmer:  Harry Martin
        * Type:        Content Page (UI)
        * Description: This page is a static page which accepts user input.
        *              Utilizes the ActiveProfile Singleton container and POST/GET
        *              requests to either verify entered credentials with those stored online
        *              OR to build a new user profile and store online.
        *              Utilizes Cryptography to has passwords.
        */
        UserProfile testP;
        public MainPage()
        {
            testP = ActiveProfile.Instance;
            InitializeComponent();
        }

        async void RootLogin(object sender, EventArgs e) //LOGIN button
        {
            /* Method:      RootLogin
            * Programmer:   Harry Martin
            * Description:  This method accepts the user input
            *               And attempt to verify the entered password
            *               against the hashed stored one
            */
            //Get the values from the user prompts
            String w_username = UserN.Text;
            String w_password = PassW.Text;

            //Write these to a local user profile object
            UserProfile local_UP = new UserProfile();
            local_UP.username = w_username;
            local_UP.password = CryptoHandler.Hash(w_password);

            String userJson = WebMethod.GetResult("load", "users/" + local_UP.username, "");
            UserProfile v_test = JsonParser.ParseToUser(userJson);

            //DEBUG: await DisplayAlert("Alert", "Comparing: \n" + local_UP.password + "\n and: \n" + v_test.password, "OK");

            switch (ActiveProfile.LoadProfile(local_UP)) //switch depending on results of profile loading methodlogy
            {
                case (0):
                    await DisplayAlert("Alert", "Account not found.", "OK");
                    break;
                case (1):
                    await Navigation.PushAsync(new MenuPage { Title = "Main Menu" });
                    break;
                case (2):
                    await DisplayAlert("Alert", "Login Failed.\nWrong password.", "OK");
                    break;
            }            
        }

        async void RootCreate(object sender, EventArgs e) //CREATE account button
        {
            /* Method:      RootCreate
            * Programmer:   Harry Martin
            * Description:  This method accepts the user input
            *               Creates a new user object and writes it to the online database
            *               This will overwrite any existing user on the server with the same name
            *               The password will be hashed (SHA254)
            */
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
