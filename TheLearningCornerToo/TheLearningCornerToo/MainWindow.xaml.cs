using System;
using System.Threading;
using System.Windows;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;

namespace TheLearningCornerToo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //sleeping to make the splash screen last longer
            Thread.Sleep(4000);
            InitializeComponent();

            //turning on the kinect on program start up.
            //KinectSensor kinectSensor = KinectSensor.GetDefault();
            //kinectSensor.Open();

            //show person's body, create kinect movement area via xaml & cs
            KinectRegion.SetKinectRegion(this, KinectArea);       
           
            App app = ((App)Application.Current);
            app.KinectRegion = KinectArea;
            //changing cursor
            app.KinectRegion.CursorSpriteSheetDefinition = new CursorSpriteSheetDefinition(new System.Uri("pack://application:,,,/Images/CursorSpriteSheetPurple.png"), 4, 20, 137, 137);
            // Use the default sensor
            this.KinectArea.KinectSensor = KinectSensor.GetDefault();
            

            
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            var colorlesson = new ColorLesson();
            colorlesson.Show();
            Close();
        }


        private void AlphabetButton_Click(object sender, RoutedEventArgs e)
        {
            var alphabet = new AlphabetLesson();
            alphabet.Show();
            Close();
        }

        private void WordButton_Click(object sender, RoutedEventArgs e)
        {
            var words = new WordsLesson();
            words.Show();
            Close();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}