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
    public partial class AdminPage : ContentPage
    {
        /* Class:        AdminPage
        *  Programmer:   Harry Martin
        *  Type:         Content Page (UI)
        *  Description:  This class defines the methods the admin page uses
        */
        public AdminPage()
        {
            InitializeComponent();
        }

        void LoadLocal()
        {
            QuizContainer.LoadSampleQuiz();
        }

        void LoadWeb()
        {

            String loadResult = QuizContainer.LoadWebQuizzes();
            DisplayAlert("Alert", "Web Quizzes Load Result: " + loadResult, "OK");
            
        }

        void QuizUpload()
        {
            String QuizID = QuizEntry.Text;
            if (QuizContainer.UploadQuiz(QuizID) == 1) {
                DisplayAlert("Alert", "Quiz '" + QuizID + "' succesfully uploaded.", "OK");
            } else
            {
                DisplayAlert("Alert", "Unable to upload '" + QuizID + "'.", "OK");
            }
        }
    }
}