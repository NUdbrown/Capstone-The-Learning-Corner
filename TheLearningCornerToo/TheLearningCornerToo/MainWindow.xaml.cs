using System;
using System.Media;
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
        private SoundPlayer _player = new SoundPlayer();

        public SoundPlayer Player
        {
            get
            {
                return _player;
            }

            set
            {
                _player = value;
            }
        }

        public MainWindow()
        {
            //offical theme music     
            Player.Stream = Properties.Resources.Schooldays_intro;
            {
                Player.PlayLooping();
            }

            //sleeping to make the splash screen last longer 
            Thread.Sleep(4000);
            InitializeComponent();

            //turning on the kinect on program start up.
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
            Player.Stop();
            Close();
            colorlesson.Show();

        }


        private void AlphabetButton_Click(object sender, RoutedEventArgs e)
        {
            var alphabet = new AlphabetLesson();
            Player.Stop();
            Close();
            alphabet.Show();
        }

        private void WordButton_Click(object sender, RoutedEventArgs e)
        {
            var words = new WordsLesson();
            Player.Stop();
            Close();
            words.Show();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}