using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TheLearningCornerToo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor kinectSensor = null;

        public MainWindow()
        {
            //turning on the kinect on program start up.
            kinectSensor = KinectSensor.GetDefault();
            kinectSensor.Open();

            //sleeping to make the splash screen last longer
            Thread.Sleep(4000);

            //adding buttons here
            

            InitializeComponent();
           
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AlphabetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WordButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
