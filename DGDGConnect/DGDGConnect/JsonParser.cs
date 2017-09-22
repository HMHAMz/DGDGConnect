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
        * Description: This class will deserialize the passed data string into the methods object type. */

        public static Quiz[] ParseToQuizArray(String data)
        {
            Quiz[] quizArray;
            quizArray = JsonConvert.DeserializeObject<Quiz[]>(data);
            return quizArray;
        }

        public static List<Quiz> ParseToQuizList(String data)
        {
            List<Quiz> quizList;
            quizList = JsonConvert.DeserializeObject<List<Quiz>>(data);
            return quizList;
        }

        public static Quiz ParseToQuiz(String data)
        {
            Quiz quiz;
            quiz = JsonConvert.DeserializeObject<Quiz>(data);
            return quiz;
        }

        public static List<String> ParseToStringList(String data)
        {
            List<String> list;
            list = JsonConvert.DeserializeObject<List<String>>(data);
            return list;
        }


        public static UserProfile ParseToUser(String data)
        {
            UserProfile user;
            user = JsonConvert.DeserializeObject<UserProfile>(data);
            return user;
        }

    }
}
