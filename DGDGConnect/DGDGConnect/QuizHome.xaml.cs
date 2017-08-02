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
        /*DataTemplate QuizBoolTemplate;

        private ObservableCollection<MenuModel> quizQuestionContainer { get; set; }//Observable Collection is used such that the ListView will update real-time if items are removed or added! */
        public QuizHome()
        {
            loadJsonAsync();
            /*BuildQuizTemplates();

            quizQuestionContainer = new ObservableCollection<MenuModel>(); //Contains menu items as an Observable Collection, meaning they can be adjusted during runtime and updated
            ListView menuView = new ListView();
            menuView.ItemsSource = quizQuestionContainer;
            menuView.ItemTemplate = QuizBoolTemplate; //Applies the item template to the menu list view

            //Add the Menu Items to the list, using the model defined in the MenuModel class.
            quizQuestionContainer.Add(new MenuModel("Games", "DGDG member games", false, "image.jpg", "Games Link"));
            quizQuestionContainer.Add(new MenuModel("Social", "Connect with other members", false, "image.jpg", "Social Link"));
            quizQuestionContainer.Add(new MenuModel("Events", "Get details on upcoming events", false, "image.jpg", "Events Link"));
            quizQuestionContainer.Add(new MenuModel("News", "News worth reading", false, "image.jpg", "News Link"));
            quizQuestionContainer.Add(new MenuModel("Resources", "DGDG curated resources", false, "image.jpg", "Resources Link"));



            //Create the StackLayout that contains the Menu
            var QuizViewStack = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
            };
            QuizViewStack.Children.Add(menuView); //Add the menu list view to the Menu View Stack

            Content = QuizViewStack; //Add the StackLayout onto the view content*/

            InitializeComponent();
        }

        void loadJsonAsync()
        {
            
            try
            {
                //string jsonInput = System.IO.File.ReadAllText("example.json", Encoding.UTF8);
                //Console.OutputEncoding = Encoding.UTF8;
                //Console.WriteLine(allText);
                var assembly = typeof(LoadResourceText).GetTypeInfo().Assembly;
                stream = assembly.GetManifestResourceStream("DGDGConnect.quizzes_sample_xamarin.json");
                string jsonInput = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    jsonInput = reader.ReadToEnd();
                }
                //IFolder rootFolder = FileSystem.Current.LocalStorage;
                //var dataFile = await rootFolder.GetFileAsync("quizzes_sample_xamarin.json");

                //var jsonInput = await dataFile.ReadAllTextAsync();
                Quiz temp = JsonConvert.DeserializeObject<Quiz>(jsonInput);
                DisplayAlert("Alert", "Unknown exception..." + temp.title.ToString(), "OK");
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