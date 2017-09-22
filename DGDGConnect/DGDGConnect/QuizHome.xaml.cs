using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; //ObservableCollection
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DGDGConnect
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizHome : ContentPage
    {
        /* Class:       QuizHome
         * Programmer:  Harry Martin
         * Type:        Content Page (UI)
         * Description: This page utilizes the QuizContainer Singleton to build the Quiz list display dynamically
         *              The list is clickable such that individual quizzes can be selected and openned
         */
        List<Quiz> ActiveQuizzes;
        int quizViewRows = 2;

        public QuizHome()
        {
            if (QuizContainer.Instance.Count() == 0) // If no quiz data has currently been loaded, attempt to load the local sample quizzes
            {
                if (QuizContainer.LoadSampleQuiz() == 1)
                {
                    DisplayAlert("Alert", "Local Quizzes Succesfully Loaded", "OK");
                }
                else
                {
                    DisplayAlert("Alert", "Unable to load quizzes...", "OK");
                }
            }

            ActiveQuizzes = QuizContainer.Instance;

            Grid QuizViewGrid = BuildOptionsView(); //Instantiate and get the Grid view containing the of quiz options.

            var scrollView = new ScrollView();

            StackLayout pageStack = new StackLayout() { HorizontalOptions = LayoutOptions.StartAndExpand };

            pageStack.Children.Add(QuizViewGrid);

            scrollView.Content = pageStack; //enables Scroll functionality 

            Content = scrollView;

        }

        Grid BuildOptionsView()
        {
            /* Method:      BuildOptionsView
             * Programmer:  Harry Martin
             * Description: This method manages the construction of the Grid View construction
                            Which includes the binding of the loaded quiz objects to their UI elements */
            Grid quizGrid = BuildQuizGrid();

            int index = 0;
            foreach (Quiz quizFocus in ActiveQuizzes)
            {
                index++;
                //Create the element vars
                var titleLabel = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Start};
                var scoreLabel = new Label { FontAttributes = FontAttributes.Italic };
                var idLabel = new Label { HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                var goButton = new Button { Text = "Go" };

                //Set the button links
                goButton.Clicked += delegate {
                    OpenQuiz(quizFocus);
                };

                //Set the Bindings
                titleLabel.SetBinding(Label.TextProperty, "title");
                idLabel.SetBinding(Label.TextProperty, "id");

                //Set the Binding Contexts
                titleLabel.BindingContext = quizFocus;
                idLabel.BindingContext = quizFocus;

                /*Add the elements to the parent Grid
                 * (index) X (quizViewRows) = The current Quiz Index's row*/
                quizGrid.Children.Add(titleLabel, 1, index * quizViewRows);
                quizGrid.Children.Add(idLabel, 0, index * quizViewRows);
                quizGrid.Children.Add(goButton, 2, index * quizViewRows);

                //Set any advance row/grid spanning
                Grid.SetRowSpan(idLabel, 2);
                Grid.SetRowSpan(goButton, 2);
                Grid.SetColumnSpan(goButton, 2);
            }
            return quizGrid;
        }

        Grid BuildQuizGrid()
        {
            /* Method:      BuildQuizGrid
             * Programmer:  Harry Martin
             * Description: This method builds the Grid container that will hold all the Quiz list UI labels
                            This is done dynamically based on the number of quiz elements in the Quiz Array loaded*/
            var quizGrid = new Grid() 
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

            foreach (Quiz quizFocus in ActiveQuizzes)
            {
                for (int i = 1; i <= quizViewRows; i++)
                {
                    quizGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
            }
            quizGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //Adds a final row to prevent stretching of final element

            return quizGrid;
        }

        async void OpenQuiz(Quiz TargetQuiz)
        {
            /* Method: OpenQuiz
            * Programmer: Harry Martin
            * Description: This method adds a new QuizRun page to the Navigation stack and passes the selected quiz object */
            await Navigation.PushAsync(new QuizRun(TargetQuiz) { Title = "Title: " + TargetQuiz.title.ToString() });
        }
      
    }
}