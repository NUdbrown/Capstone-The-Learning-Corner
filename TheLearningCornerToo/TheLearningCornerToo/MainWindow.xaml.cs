using System.Threading;
using System.Windows;
using Microsoft.Kinect;

namespace TheLearningCornerToo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly KinectSensor kinectSensor;

        public MainWindow()
        {
            //turning on the kinect on program start up.
            kinectSensor = KinectSensor.GetDefault();
            kinectSensor.Open();

            //sleeping to make the splash screen last longer
            Thread.Sleep(4000);

            //adding buttons here
            //try kinects scroller?


            InitializeComponent();
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