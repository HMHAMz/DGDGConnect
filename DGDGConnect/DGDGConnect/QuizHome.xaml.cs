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
            

            Content = QuizViewGrid; //Add the StackLayout onto the view content
            /*var stack = new StackLayout();

            for (int i = 0; i < 3; i++)
            {
                stack.Children.Add(new Button { Text = "Button " + i });
            }

            //Content = new ScrollView { Content = stack };*/

            //Content = new StackLayout { Content = QuizViewGrid }; //Add the StackLayout onto the view content

            //DisplayAlert("Alert", "Grid built and content set.", "OK"); //! Debug code, to be removed
            //InitializeComponent();
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
                DisplayAlert("Alert", "Json File Loaded. Title #2 value: " + quizArray[1].title.ToString(), "OK"); //! Debug code, to be removed
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
                var titleLabel = new Label { FontAttributes = FontAttributes.Bold };
                var commentLabel = new Label { FontAttributes = FontAttributes.Italic };
                var imageLabel = new Label { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
                //var switcher = new Switch { }; //!Debug Code, to be removed
                var testLabel = new Label { Text = "Test", FontAttributes = FontAttributes.Bold };

                titleLabel.SetBinding(Label.TextProperty, "title");
                commentLabel.SetBinding(Label.TextProperty, "id");
                imageLabel.SetBinding(Label.TextProperty, "score");
                titleLabel.BindingContext = quizFocus;
                commentLabel.BindingContext = quizFocus;
                imageLabel.BindingContext = quizFocus;
                //switcher.SetBinding(Switch.IsToggledProperty, "ToggleAnswer");

                quizGrid.Children.Add(testLabel, 1, index * quizViewRows);//! Debug code, to be removed
                quizGrid.Children.Add(titleLabel, 1, index*quizViewRows);
                //DisplayAlert("Alert", "titlelabel added to quizRow #: " + index * quizViewRows, "OK"); //! Debug code, to be removed
                quizGrid.Children.Add(commentLabel, 1, index * quizViewRows + 1);
                quizGrid.Children.Add(imageLabel, 0, index * quizViewRows);
                //grid.Children.Add(switcher, 1, 3);

                //DisplayAlert("Alert", "Quiz " + index + " processed...", "OK"); //! Debug code, to be removed
            }
            //DisplayAlert("Alert", "Grid row definitions: " + quizGrid.RowDefinitions.Count(), "OK"); //! Debug code, to be removed
            return quizGrid;
        }

        Grid BuildQuizGrid()
        {
            /* Method: BuildQuizGrid
             * Programmer: Harry Martin
             * Description: This method builds the Grid container that will hold all the Quiz list UI labels
             This is done dynamically based on the number of quiz elements in the Quiz Array loaded*/
            var quizGrid = new Grid() //The grid defines the layout of the elements added below this definition
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
            return quizGrid;
        }
      
        }
    }