using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;


namespace TheLearningCornerToo
{
    /// <summary>
    /// Interaction logic for ColorLesson.xaml
    /// </summary>
    public partial class ColorLesson : Window
    {
        public ColorLesson()
        {
            InitializeComponent();
            
            Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //show person's body & moving cursor
            KinectRegion.SetKinectRegion(this, KinectArea);

            App app = ((App)Application.Current);
            app.KinectRegion = KinectArea;
            app.KinectRegion.CursorSpriteSheetDefinition = new CursorSpriteSheetDefinition(new System.Uri("pack://application:,,,/Images/CursorSpriteSheetPurple.png"), 4, 20, 137, 137);
            // Use the default sensor
            this.KinectArea.KinectSensor = KinectSensor.GetDefault();

            ////add buttons to screen
            //for (int i = 1; i < 2; i++)
            //{
            //    var button = new Button();
            //    {
            //        Height = 100;
            //        Width = 100;
            //        Name = "Button" + i;
            //        Content = "Click Me";
            //        Background = new SolidColorBrush(Colors.DeepPink);
                    //switch (Name)
                    //{
                    //    case "Button1":
                    //    {
                    //        button.Content = new Image
                    //        {
                    //            Source = new BitmapImage(new System.Uri("pack://application:,,,/Images/Colors/color_black.png")),

                    //        };
                    //        break;
                    //    }
                    //    case "Button2":
                    //        {
                    //            button.Content = new Image
                    //            {
                    //                Source = new BitmapImage(new System.Uri("pack://application:,,,/Images/Colors/color_white.png")),

                    //            };
                    //            break;
                    //        }
                    //}

                //};

                //button.Click += (o, args) => MessageBox.Show("You clicked button #" + button.Name);
                //ScrollContent.Children.Add(button);
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
