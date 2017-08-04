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
        Stream stream;
        Quiz[] quizArray;
        int quizViewRows = 2;

        public QuizHome()
        {
            LoadJson(); //Load the Json resource file into memory

            Grid QuizViewGrid = BuildOptionsView(); //Instantiate and get the Grid view containing the of quiz options.

            var scrollView = new ScrollView();

            StackLayout pageStack = new StackLayout() { HorizontalOptions = LayoutOptions.StartAndExpand };

            pageStack.Children.Add(QuizViewGrid);

            scrollView.Content = pageStack; //enables Scroll functionality 

            Content = scrollView;

        }


        void LoadJson()
        {
            /* Method: LoadJson
             * Programmer: Harry Martin
             * Description: This try statement will attempt to load the json file from the local resources
             The stream reader will convert that stream into a string variable
             Our Json API will then deserialize that string into the referenced object.
             If the attempt fails, the details will be displayed in an alert message.*/
            try
            {
                var assembly = typeof(LoadResourceText).GetTypeInfo().Assembly;
                stream = assembly.GetManifestResourceStream("DGDGConnect.quizzes_sample_xamarin.json");
                string jsonInput = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    jsonInput = reader.ReadToEnd();
                }
                
                quizArray = JsonConvert.DeserializeObject<Quiz[]>(jsonInput);
                // DisplayAlert("Alert", "Json File Loaded. Title #2 value: " + quizArray[1].title.ToString(), "OK"); //! Debug code, to be removed
            }
            catch (FileNotFoundException ex)
            {
                DisplayAlert("Alert", "JSON File not found...", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Unknown exception..." + ex + "\n Json Stream Value: " + stream, "OK");
            }
        }

        Grid BuildOptionsView()
        {
            /* Method: BuildOptionsView
             * Programmer: Harry Martin
             * Description: This method manages the construction of the Grid View construction
             Which includes the binding of the loaded quiz objects to their UI elements */
            Grid quizGrid = BuildQuizGrid();

            int index = 0;
            foreach (Quiz quizFocus in quizArray)
            {
                index++;
                //Create the element vars
                var titleLabel = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Start};
                var scoreLabel = new Label { FontAttributes = FontAttributes.Italic };
                var idLabel = new Label { HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
                var goButton = new Button { Text = "Go", BackgroundColor = Color.FromHex("#95e4ff") };
                //var switcher = new Switch { }; //!Debug Code, to be removed

                //Set the button links
                    goButton.Clicked += delegate {
                    OpenQuiz(quizFocus);
                };

                //Set the Bindings
                titleLabel.SetBinding(Label.TextProperty, "title");
                //scoreLabel.SetBinding(Label.TextProperty, "score"); //! Removed from UI
                idLabel.SetBinding(Label.TextProperty, "id");

                //Set the Binding Contexts
                titleLabel.BindingContext = quizFocus;
                //scoreLabel.BindingContext = quizFocus; //! Removed from UI
                idLabel.BindingContext = quizFocus;

                //switcher.SetBinding(Switch.IsToggledProperty, "ToggleAnswer");

                /*Add the elements to the parent Grid
                 *index * quizViewRows = The current Quiz Index's row*/
                quizGrid.Children.Add(titleLabel, 1, index * quizViewRows);
                //quizGrid.Children.Add(scoreLabel, 1, index * quizViewRows + 1); //! Removed from UI
                quizGrid.Children.Add(idLabel, 0, index * quizViewRows);
                quizGrid.Children.Add(goButton, 2, index * quizViewRows);

                //Set any advance row/grid spanning
                Grid.SetRowSpan(idLabel, 2);
                Grid.SetRowSpan(goButton, 2);
                Grid.SetColumnSpan(goButton, 2);


                //grid.Children.Add(switcher, 1, 3);

            }
            
            return quizGrid;
        }

        Grid BuildQuizGrid()
        {
            /* Method: BuildQuizGrid
             * Programmer: Harry Martin
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

            foreach (Quiz quizFocus in quizArray)
            {
                for (int i = 1; i <= quizViewRows; i++)
                {
                    quizGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    //DisplayAlert("Alert", "Row added to quizGrid: " + i, "OK"); //! Debug code, to be removed
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