using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace DGDGConnect
{
    public static class JsonParser
    {
        /* Class: JsonParser
        * Programmer: Harry Martin
        * Description: This class will deserialize the passed string into the methods object type. */

        public static Quiz[] ParseToQuiz(String data)
        {
            Quiz[] quizArray;
            quizArray = JsonConvert.DeserializeObject<Quiz[]>(data);
            return quizArray;
        }

        public static UserProfile ParseToUser(String data)
        {
            UserProfile user;
            user = JsonConvert.DeserializeObject<UserProfile>(data);
            return user;
        }

    }
}
