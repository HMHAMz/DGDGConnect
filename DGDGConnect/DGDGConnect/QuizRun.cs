﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DGDGConnect
{
    public class QuizRun : ContentPage
    {
        /* Class:       QuizRun
         * Programmer:  Harry Martin
         * Type:        Content Page (UI)
         * Description: This page accepts a Quiz object and builds the quiz content dynamically
         *              based on the variables of the Quiz loaded.
         */

        int questionViewRows = 3;   //number of rows per Quiz Question
        Quiz thisQuiz;              //quiz object
        int currentPage = 1;        //page counter
        int minQ = 0;               //page low question number
        int maxQ = 0;               //page upper question number
        List<int> PageQIDs = new List<int>();   //List of question ID's 
        List<QuizQuestion> ThisPageQs = new List<QuizQuestion>();   //List containing loaded Quiz questions

        //QuizQuestion[] PageQuestions = new QuizQuestion[] { }; 

        public QuizRun(Quiz p_Quiz)
        {
            /* Method:      QuizRun
            *  Description: Accepts a Quiz object and builds the page content         
            */
            thisQuiz = p_Quiz;

            //allows for content to be updated during operation, allowing for multiple views of content
            updatePageQuestions();
            updatePageContent(); 
        }

        void updatePageQuestions()
        {
            /* Method:      updatePageQuestions
            *  Description: Builds the 'current' page questions list, based on the current page ID
            *               If there is only one 'page' defined, then all questions are built
            */
            try
            {
                if (thisQuiz.questionsPerPage != null)
                {
                    minQ = 1; // reset min question
                    PageQIDs.Clear(); // reset the PageQID list

                    if (ThisPageQs != null || ThisPageQs.Count != 0)
                        ThisPageQs.Clear();
                //Get min and max Q from quiz information and current page number
                    for (int i = currentPage; i > 1; i--) //for every page up until current, add question count
                    {
                        minQ = minQ + thisQuiz.questionsPerPage[i - 2];
                    }
                    maxQ = minQ + (thisQuiz.questionsPerPage[currentPage - 1]-1); //get max question id
                    //DEBUG: DisplayAlert("Alert", "\nPage Min Q id= " + minQ + "Page Max Q id:" + maxQ, "OK");
                //Build list of question ID's from min and max question vars
                    for (int i = minQ; i <= maxQ; i++)
                    {
                        PageQIDs.Add(i);
                    }
                //Build list of complete questions from Question ID's
                    foreach (int QID in PageQIDs)
                    {
                        foreach (QuizQuestion Question in thisQuiz.questions)
                        {
                            if (Question.id == QID)
                            {
                                ThisPageQs.Add(Question);
                            }
                        }
                    }
                }
                else
                {
                    ThisPageQs = thisQuiz.questions;
                    minQ = 0;
                    maxQ = thisQuiz.questions.Count();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Update Page Questions failed for quiz: " + thisQuiz.title + ex, "OK");
            }
        }

        void updatePageContent()
        {
            /* Method:      updatePageContent
            *  Description: Builds the current page's content view, based on the current question list    
            */
            try
            {
                var scrollView = new ScrollView();

                StackLayout pageStack = new StackLayout { HorizontalOptions = LayoutOptions.Center };

                var Header = new Label { Text = "You are completing the " + thisQuiz.title.ToString() + ".", HorizontalTextAlignment = TextAlignment.Center };

                pageStack.Children.Add(Header);

                Grid QuizViewGrid = BuildQuestionsView(); //Instantiate and get the Grid view containing the of quiz options.

                pageStack.Children.Add(QuizViewGrid);

                scrollView.Content = pageStack; //enables Scroll functionality 

                Content = scrollView;
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Update Page Content failed..." + thisQuiz.title + ex, "OK");
            }
        }

        Grid BuildQuestionsView()
        {
            /* Method: BuildQuestionsView
             * Programmer: Harry Martin
             * Description: This method manages the construction of the Grid View
             *              Which includes the binding of the quiz question objects to their UI elements 
             */

            //Get the number of questions per page and build the appropriate variables such that the pages can be dynamically built
            Grid quizGrid = BuildQuestionGrid();

            int index = 0;
            try
            {
                foreach (QuizQuestion quizQuestion in ThisPageQs)
                {
                    //Create the element vars
                    var questionLabel = new Label { FontAttributes = FontAttributes.Bold };
                    var helpLabel = new Label { HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };

                    //Set the Bindings
                    questionLabel.SetBinding(Label.TextProperty, "text");
                    helpLabel.SetBinding(Label.TextProperty, "help");

                    //Set the Binding Contexts
                    questionLabel.BindingContext = quizQuestion;
                    helpLabel.BindingContext = quizQuestion;

                    /*Add the elements to the parent Grid
                        * [index * questionViewRows] = The current Quiz Index's row*/
                    quizGrid.Children.Add(questionLabel, 0, index * questionViewRows);
                    quizGrid.Children.Add(helpLabel, 0, index * questionViewRows + 1);

                    //Set advance row/grid spanning
                    Grid.SetColumnSpan(questionLabel, 2);
                    Grid.SetColumnSpan(helpLabel, 2);

                    //The following if statements will get the question type and dynamically implement the required data entry element
                    if (quizQuestion.type == "date")
                    {
                        DatePicker datePicker = new DatePicker
                        {
                            Format = "D",
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.End
                        };

                        quizGrid.Children.Add(datePicker, 0, index * questionViewRows + 2);

                        Grid.SetColumnSpan(datePicker, 2); //Set advance row/grid spanning
                    }
                    else if (quizQuestion.type == "textbox")
                    {
                        var textbox = new Entry { Placeholder = "Short answer..." };
                        quizGrid.Children.Add(textbox, 0, index * questionViewRows + 2);

                        Grid.SetColumnSpan(textbox, 2); //Set advance row/grid spanning
                    }
                    else if (quizQuestion.type == "textarea")
                    {
                        var textarea = new Editor() { HeightRequest = 100 };
                        quizGrid.Children.Add(textarea, 0, index * questionViewRows + 2);

                        Grid.SetColumnSpan(textarea, 2); //Set advance row/grid spanning
                    }
                    else if (quizQuestion.type == "choice")
                    {
                        var picker = new Picker();
                        foreach (string p_choice in quizQuestion.options)
                        {
                            picker.Items.Add(p_choice);
                        }
                        quizGrid.Children.Add(picker, 0, index * questionViewRows + 2);

                        Grid.SetColumnSpan(picker, 2); //Set advance row/grid spanning
                    }
                    //END Quiz Answer Type Statement

                    index++;
                }


                quizGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //Adds a final row to prevent stretching of final element

                //DEBUG: DisplayAlert("Alert", "\n Page MinQ id: " + minQ + " \nPage MaxQ id:" + maxQ, "OK");

                /* Add the 'Next Page' button IF there are more questions to display.
                 * OR add the 'Complete' button otherwise... */
                if (maxQ < thisQuiz.questions.Count())
                {
                    var nextButton = new Button { Text = "Next Page", VerticalOptions = LayoutOptions.Start};

                    quizGrid.Children.Add(nextButton, 0, maxQ * questionViewRows + 3);
                    Grid.SetColumnSpan(nextButton, 2); //Set advance row/grid spanning

                    //Set the button links
                    nextButton.Clicked += delegate
                    {
                        if (thisQuiz.questionsPerPage.Count() > currentPage)
                        {
                            currentPage++;
                            updatePageQuestions();
                            updatePageContent();
                        }
                    };
                }
                else
                {
                    var completeButton = new Button { Text = "Complete" };

                    quizGrid.Children.Add(completeButton, 0, maxQ * questionViewRows + 3);
                    Grid.SetColumnSpan(completeButton, 2); //Set advance row/grid spanning

                    //Set the button links
                    completeButton.Clicked += delegate
                    {
                        Navigation.PopAsync();
                    };
                }
                DisplayAlert("Alert", "DEBUG: \nNumber of QuizGrid Elements: " + quizGrid.Children.Count(), "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Unknown exception..." + ex, "OK");
            }

            return quizGrid;
        }

        Grid AddNewRow(Grid q_Grid)
        {
            /* Method:      QuizRun
            * Description:  Currently unused, this method simply adds a new row to the Grid element passed. 
            */
            q_Grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto, });
            return q_Grid;
        }

        Grid BuildQuestionGrid()
        {
            /* Method: BuildQuestionGrid
             * Programmer: Harry Martin
             * Description: This method builds the Grid container that will hold all the Quiz question UI elements
                            This is done dynamically based on the number of quiz questions in the Quiz object loaded
             */
            var questionGrid = new Grid()
            {
                RowSpacing = 2,
                VerticalOptions = LayoutOptions.StartAndExpand,
                //HorizontalOptions = LayoutOptions.StartAndExpand,
                RowDefinitions =
                    {
                    //new RowDefinition { Height = GridLength.Auto }
                    },
                ColumnDefinitions =
                    {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) }
                    }
            };

            questionGrid.Children.Clear();
            questionGrid.RowDefinitions.Clear();
            try
            {
                int count = 0;
                foreach (int QID in PageQIDs)
                {
                    count++;
                    //DisplayAlert("Alert", "Adding Question = " + q_index, "OK"); //!Debug - To Remove
                    for (int i = 1; i <= questionViewRows; i++)
                    {
                        questionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto, });
                        //DisplayAlert("Alert", "Adding Question Row = " + i, "OK");//!Debug - To Remove
                    }
                }
                int count2 = questionGrid.RowDefinitions.Count;
                DisplayAlert("Alert", "DEBUG: \nTotal Rows added: " + count.ToString() + "\n Total rows: " + count2.ToString(), "OK");
                questionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //Adds a final row to prevent stretching of final element
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Unknown exception..." + ex, "OK");
            }
            return questionGrid;
        }
    }
}