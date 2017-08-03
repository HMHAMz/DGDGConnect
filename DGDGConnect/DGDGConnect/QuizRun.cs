using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DGDGConnect
{
    public class QuizRun : ContentPage
    {
        public QuizRun(Quiz thisQuiz)
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Your are currently doing the " + thisQuiz.title.ToString()}
                }
            };
        }
    }
}