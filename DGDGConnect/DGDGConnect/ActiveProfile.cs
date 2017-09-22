using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGDGConnect
{
    public sealed class ActiveProfile
    {
        /* Class:        ActiveProfile
        *  Programmer:   Harry Martin
        *  Description:  This class is implemented as a singleton and stores the active UserProfile
        *                It also implements the profile loading methods
        */
        private static UserProfile instance = null;
        private static readonly object padlock = new object();

        public static UserProfile Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserProfile();
                    }
                    return instance;
                }
            }
        }

        public static int VerifyActiveUser()
        {
            String userJson = WebMethod.GetResult("load", "users/" + instance.username, "");
            if (userJson != "Web request failed.")
            {
                UserProfile v_test = JsonParser.ParseToUser(userJson);

                if (v_test.password == instance.password)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            } else
            {
                return 0;
            }
        }

        public static int LoadProfile(UserProfile open_UP)
        {
            /* Method:      LoadProfile
            *  Description: Accepts a UserProfile object
            *               Which is then used to download load the matching database stored user profile
            */
            String userJson = WebMethod.GetResult("load", "users/" + open_UP.username, "");
            if (userJson != "Web request failed.")
            {
                UserProfile v_test = JsonParser.ParseToUser(userJson);

                if (v_test.password == open_UP.password)
                {
                    instance = v_test;
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 0;
            }
        }

        public static void UnloadProfile()
        {
            instance = new UserProfile();
        }

    }
}
