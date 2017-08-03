using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DGDGConnect
{
    public class QuizRun : ContentPage
    {

        int questionViewRows = 2;
        Quiz thisQuiz;

        public QuizRun(Quiz p_Quiz)
        {
            thisQuiz = p_Quiz;

            StackLayout PageStack = new StackLayout
            {
                Children = {
                    new Label { Text = "Your are currently doing the " + thisQuiz.title.ToString()}
                }
            };

            Grid QuizViewGrid = BuildQuestionsView(); //Instantiate and get the Grid view containing the of quiz options.

            PageStack.Children.Add(QuizViewGrid);

            Content = PageStack;
            //Content = QuizViewGrid; //Set the Grid as the page content
        }

        Grid BuildQuestionsView()
        {
            /* Method: BuildQuestionsView
             * Programmer: Harry Martin
             * Description: This method manages the construction of the Grid View
             Which includes the binding of the quiz question objects to their UI elements */
            Grid quizGrid = BuildQuestionGrid();

            int index = 0;
            foreach (QuizQuestion quizQuestion in thisQuiz.questions)
            {
                index++;
                //Create the element vars
                var questionLabel = new Label { FontAttributes = FontAttributes.Bold };
                var helpLabel = new Label { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };

                //Set the Bindings
                questionLabel.SetBinding(Label.TextProperty, "text");
                helpLabel.SetBinding(Label.TextProperty, "help");

                //Set the Binding Contexts
                questionLabel.BindingContext = quizQuestion;

                helpLabel.BindingContext = quizQuestion;

                /*Add the elements to the parent Grid
                 * [index * questionViewRows] = The current Quiz Index's row*/
                quizGrid.Children.Add(questionLabel, 0, index * questionViewRows);
                quizGrid.Children.Add(helpLabel, 0, index * questionViewRows+1);

            }

            return quizGrid;
        }

        Grid BuildQuestionGrid()
        {
            /* Method: BuildQuestionGrid
             * Programmer: Harry Martin
             * Description: This method builds the Grid container that will hold all the Quiz question UI elements
             This is done dynamically based on the number of quiz questions in the Quiz object loaded*/
            var questionGrid = new Grid()
            {
                RowSpacing = 2,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                    {
                    new RowDefinition { Height = GridLength.Auto }
                    },
                ColumnDefinitions =
                    {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto }
                    }
            };

            foreach (QuizQuestion quizQuestion in thisQuiz.questions)
            {
                for (int i = 1; i <= questionViewRows; i++)
                {
                    questionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
            }

            questionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //Adds a final row to prevent stretching of final element

            return questionGrid;
        }
    }
}