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
        
        public QuizHome()
        {
            loadJson();

            InitializeComponent();
        }

        void loadJson()
        {
            
            /*This try statement will attempt to load the json file from the local resources
             * The stream reader will convert that stream into a string variable
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
                
                Quiz[] temp = JsonConvert.DeserializeObject<Quiz[]>(jsonInput);
                DisplayAlert("Alert", "Json File Loaded. Title value: " + temp[2].title.ToString(), "OK");
            }
            catch (FileNotFoundException ex)
            {
                DisplayAlert("Alert", "JSON File not found...", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Unknown exception..." + ex + "/n stream value: " + stream, "OK");
            }
        }

        /* This method builds the custom list viewcell template that is used by the quiz ListView */
        /*void BuildQuizTemplates()
        {
           
            QuizBoolTemplate = new DataTemplate(() => {
                var grid = new Grid() //The grid defines the layout of the elements added below this definition
                { 
                    RowSpacing = 2,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                    {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                var commentLabel = new Label { FontAttributes = FontAttributes.Italic };
                var imageLabel = new Label { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
                var switcher = new Switch { };

                nameLabel.SetBinding(Label.TextProperty, "name");
                commentLabel.SetBinding(Label.TextProperty, "comment");
                imageLabel.SetBinding(Label.TextProperty, "image");
                switcher.SetBinding(Switch.IsToggledProperty, "ToggleAnswer");

                //Set the position of each element in the grid
                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(commentLabel, 1, 1);
                grid.Children.Add(imageLabel, 0, 0);
                grid.Children.Add(switcher, 1, 3);

                return new ViewCell { View = grid };
            });
        }*/
    }
}