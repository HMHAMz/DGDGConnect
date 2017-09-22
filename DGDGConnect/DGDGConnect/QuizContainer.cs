using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGDGConnect
{
    public sealed class QuizContainer
    {
        /* Class:        QuizContainer
        *  Programmer:   Harry Martin
        *  Description:  This class is implemented as a singleton and stores the Quiz containing list
        *                It also implements the quiz loading methods
        */
        private static List<Quiz> instance = null;
        private static readonly object padlock = new object();

        public static List<Quiz> Instance //returns the existing instance OR build one if it doesn't exist
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new List<Quiz>();
                    }
                    return instance;
                }
            }
        }

        public static int LoadWebQuiz(String QuizID)
        {
            /* Method:      LoadWebQuiz
            *  Description: Accepts a QuizID String
            *               Which is then used to download the matching database quiz and parse to an object
            */
            String quizJson = WebMethod.GetResult("load", "quizzes/" + QuizID, "");
            if (quizJson != "Web request failed.")
            {
                Quiz v_quiz = JsonParser.ParseToQuiz(quizJson);

                instance.Add(v_quiz);

                return 1;
            }
            return 0;
        }

        public static int UploadQuiz(String QuizID)
        {
            /* Method:      UploadQuiz
            *  Description: Accepts a QuizID String
            *               Which is then used to upload the matching locally (in memory) stored quiz in the online database
            */
            if (instance.Count != 0)
            {
                foreach (Quiz f_quiz in instance)
                {
                    if (f_quiz.id == QuizID)
                    {
                        String quizJson = WebMethod.Serialize(f_quiz);
                        String uploadResult = WebMethod.GetResult("save", "quizzes/" + QuizID, quizJson , "POST");

                        if (uploadResult != "Web request failed.")
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }

                    }
                }
            }
            return 0;
        }

        public static String LoadWebQuizzes()
        {
            /* Method:      WebQuizzes
            *  Description: Downloads all the quizes stored in the online database
            */
            UnloadQuizzes();

            String listJson = WebMethod.GetResult("list", "quizzes", "");

            if (listJson != "Web request failed.")
            {

                List<String> WebQuizList = JsonParser.ParseToStringList(listJson);

                if (WebQuizList.Count != 0)
                {
                    foreach (String f_quiz_id in WebQuizList)
                    {
                        LoadWebQuiz(f_quiz_id);
                    }
                    return "Quizzes Loaded";
                }
                return "Web request passed. None loaded.";
            } else
            {
                return "Web request failed. \nRequest: " + listJson;
            }
        }

        public static int LoadSampleQuiz()
        {
            /* Method:      LoadSampleQuiz
            *  Description: Loads the locally stored sample quiz array into memory
            */
            List<Quiz> t_quizList;

            String data = LoadResourceText.GetLocal("DGDGConnect.quizzes_sample_xamarin.json");
            if (data != "File not found." && data != "Unknown exception.")
            {
                t_quizList = JsonParser.ParseToQuizList(data);

                instance = t_quizList;
                return 1;
            } else
            {
                return 0;
            }

        }

        public static void UnloadQuizzes()
        {
            /* Method:      UnloadQuizzes
            *  Description: Unloads all quiz data from the local singleton
            */
            instance = new List<Quiz>();
        }

    }
}
