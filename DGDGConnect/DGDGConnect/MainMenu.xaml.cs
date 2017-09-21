using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel; //ObservableCollection

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DGDGConnect
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {

        DataTemplate MenuTemplate;


        private ObservableCollection<MenuModel> menuContainer { get; set; }//Observable Collection is used such that the ListView will update real-time if items are removed or added!
        public MainMenu()
        {
            buildTemplate(); //builds the menu viewcell

            menuContainer = new ObservableCollection<MenuModel>(); //Contains menu items as an Observable Collection, meaning they can be adjusted during runtime and updated
            ListView menuView = new ListView();
            menuView.ItemsSource = menuContainer;
            menuView.ItemTemplate = MenuTemplate; //Applies the item template to the menu list view

            //Add the Menu Items to the list, using the model defined in the MenuModel class.
            menuContainer.Add(new MenuModel("Games", "DGDG member games", false, "image.jpg", "Games Link"));
            menuContainer.Add(new MenuModel("Social", "Connect with other members", false, "image.jpg", "Social Link"));
            menuContainer.Add(new MenuModel("Events", "Get details on upcoming events", false, "image.jpg", "Events Link"));
            menuContainer.Add(new MenuModel("News", "News worth reading", false, "image.jpg", "News Link"));
            menuContainer.Add(new MenuModel("Resources", "DGDG curated resources", false, "image.jpg", "Resources Link"));



            //Create the StackLayout that contains the Menu
            var MenuViewStack = new StackLayout { 
                Padding = new Thickness(0, 20, 0, 0),
                };
            MenuViewStack.Children.Add(menuView); //Add the menu list view to the Menu View Stack
            
            Content = MenuViewStack; //Add the StackLayout onto the view content

            /* Test Button! 
            Button testButton = new Button();
            testButton.Text = "This is the test button";
            MenuViewStack.Children.Add(testButton);
            testButton.Clicked += (sender, e) => DoSomething(); */



            InitializeComponent();
        }

        /*void DoSomething()
        {
            DisplayAlert("Alert", "Toggle 1 value: " + menuContainer[1].isABool, "OK");     
        }*/

        void buildTemplate()
        {
            /* This method builds the custom list viewcell template that is used by the menu
             * The functionality should be moved to an external class such that it can be implemented into other ContentPages*/
            MenuTemplate = new DataTemplate(() => {
                var grid = new Grid() { //The grid defines the layout of the elements added below this definition
                    RowSpacing = 2,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                    {
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
                //var switcher = new Switch { };
                //var linkString = "";

                nameLabel.SetBinding(Label.TextProperty, "name");
                commentLabel.SetBinding(Label.TextProperty, "comment");
                imageLabel.SetBinding(Label.TextProperty, "image");
                //switcher.SetBinding(Switch.IsToggledProperty, "isABool");

                //Set the position of each element in the grid
                grid.Children.Add(nameLabel, 1, 0);
                grid.Children.Add(commentLabel, 1, 1);
                grid.Children.Add(imageLabel, 0, 0);
                //grid.Children.Add(switcher, 1, 0);

                var tapGesRec = new TapGestureRecognizer();
                tapGesRec.Tapped += (s, e) => {

                };

                nameLabel.GestureRecognizers.Add(tapGesRec);

                return new ViewCell { View = grid };
            });
        }
    }

    
}