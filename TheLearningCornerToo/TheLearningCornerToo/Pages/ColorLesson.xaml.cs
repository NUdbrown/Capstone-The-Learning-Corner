using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;
using TheLearningCornerToo.Pages;


namespace TheLearningCornerToo
{
    /// <summary>
    /// Interaction logic for ColorLesson.xaml
    /// </summary>
    public partial class ColorLesson : Window
    {
        public SoundPlayer Player { get; set; } = new SoundPlayer();

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


            //add buttons to screen
            for (int i = 1; i <= 10; i++)
            {
                var button = new Button();
                {
                    button.Height = 200;
                    button.Width = 200;
                    button.Focusable = false;

                    button.BorderThickness = new Thickness(0,0,0,0);
                    button.Name = "Button" + i;
                    
                    //button.Content = "Click Me";
                    //Background = new SolidColorBrush(Colors.DeepPink);
                    switch (button.Name)
                    {
                        case "Button1":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_black.png")));
                                break;
                            }
                        case "Button2":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_brown.png")));
                                break;
                            }
                        case "Button3":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_red.png")));
                                break;
                            }
                        case "Button4":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_orange.png")));
                                break;
                            }
                        case "Button5":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_yellow.png")));
                                break;
                            }
                        case "Button6":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_green.png")));
                                break;
                            }
                        case "Button7":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_blue.png")));
                                break;
                            }
                        case "Button8":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_purple.png")));
                                break;
                            }
                        case "Button9":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_pink.png")));
                                break;
                            }
                        case "Button10":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_white.png")));
                                break;
                            }
                    }
                };
               button.Click += (sender1, routedEventArgs) => ButtonOnClick(button, sender1, routedEventArgs);
               ScrollContent.Children.Add(button);

            }
            //give instructions once page is shown
            Player.Stream = Properties.Resources.color_instructions;
            {
                Player.Load();
                Player.Play();

            }

        }

        private void ButtonOnClick(Button thebutton, object sender, RoutedEventArgs routedEventArgs)
        {          
            string name = thebutton.Name;
            switch (name)
            {
                case "Button1":
                    {
                        Player.Stream = Properties.Resources.black;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button2":
                    {
                        Player.Stream = Properties.Resources.brown;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button3":
                    {
                        Player.Stream = Properties.Resources.red;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button4":
                    {
                        Player.Stream = Properties.Resources.orange;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button5":
                    {
                        Player.Stream = Properties.Resources.yellow;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button6":
                    {
                        Player.Stream = Properties.Resources.green;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button7":
                    {
                        Player.Stream = Properties.Resources.blue;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button8":
                    {
                        Player.Stream = Properties.Resources.purple;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button9":
                    {
                        Player.Stream = Properties.Resources.pink;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button10":
                    {
                        Player.Stream = Properties.Resources.white;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
            }
        }

        private void Instructions_OnClick(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.color_instructions;
            {
                Player.Load();
                Player.Play();
            }
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
