﻿using System.Threading;
using System.Windows;
using Microsoft.Kinect;
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

            KinectRegion.SetKinectRegion(this, KinectArea);
            
           //show person's body

            App app = ((App)Application.Current);
            app.KinectRegion = KinectArea;

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