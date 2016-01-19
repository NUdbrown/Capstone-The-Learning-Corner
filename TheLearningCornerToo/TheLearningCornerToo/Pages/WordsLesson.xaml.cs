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
    /// Interaction logic for WordsLesson.xaml
    /// </summary>
    public partial class WordsLesson : Window
    {
        public WordsLesson()
        {
            KinectRegion.SetKinectRegion(this, KinectRegion);
            App app = (App) Application.Current;
            app.KinectRegion = KinectRegion;
            app.KinectRegion.CursorSpriteSheetDefinition = new CursorSpriteSheetDefinition(new System.Uri("pack://application:,,,/Images/CursorSpriteSheetPurple.png"), 4, 20, 137, 137);
            this.KinectRegion.KinectSensor = KinectSensor.GetDefault();
            InitializeComponent();
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
