using System.Media;
using System.Windows;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;

namespace TheLearningCornerToo
{
    /// <summary>
    /// Interaction logic for AlphabetLesson.xaml
    /// </summary>
    public partial class AlphabetLesson : Window
    {
        private SoundPlayer Player { get; } = new SoundPlayer();    

        public AlphabetLesson()
        {
            InitializeComponent();
            Loaded += OnLoad;
        }


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            KinectRegion.SetKinectRegion(this, KinectArea);
            App app = (App)Application.Current;
            app.KinectRegion = KinectArea;
            app.KinectRegion.CursorSpriteSheetDefinition = new CursorSpriteSheetDefinition(new System.Uri("pack://application:,,,/Images/CursorSpriteSheetPurple.png"), 4, 20, 137, 137);
            this.KinectArea.KinectSensor = KinectSensor.GetDefault();

            

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            Close();
        }

        private void InstructionButton_OnClick(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.alphabet_instructions;
            {
                Player.LoadAsync();
            }
        }
    }
}
