using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;

namespace TheLearningCornerToo.Pages
{
    /// <summary>
    /// Interaction logic for ColorControl.xaml
    /// </summary>
    public partial class ColorControl : UserControl
    {
        public SoundPlayer Player { get; set; } = new SoundPlayer();

        public ColorControl()
        {
            ////show moving cursor
            //KinectRegion.SetKinectRegion(this, KinectRegion);

            //App app = ((App)Application.Current);
            //app.KinectRegion = KinectRegion;
            //app.KinectRegion.CursorSpriteSheetDefinition = new CursorSpriteSheetDefinition(new System.Uri("pack://application:,,,/Images/CursorSpriteSheetPurple.png"), 4, 20, 137, 137);
            //// Use the default sensor
            //this.KinectRegion.KinectSensor = KinectSensor.GetDefault();
           
            InitializeComponent();
        }

        private void BlackberryButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.blackberries;
            {
                Player.Load();
                Player.Play();
            }
            
            DoAnimation(BlackberryButton);
            
        }

        private void KiwiButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.kiwis;
            {
                Player.Load();
                Player.Play();
            }
            DoAnimation(KiwiButton);
        }

        private void AppleButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.apples;
            {
                Player.Load();
                Player.Play();
            }
            DoAnimation(AppleButton);
        }

        private void OrangeButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.oranges;
            {
                Player.Load();
                Player.Play();
            }
        }
        private void BananaButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.bananas;
            {
                Player.Load();
                Player.Play();
            }
        }

        private void PearButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.pears;
            {
                Player.Load();
                Player.Play();
            }
        }

        private void BlueberryButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.blueberries;
            {
                Player.Load();
                Player.Play();
            }
        }

        private void GrapeButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.grapes;
            {
                Player.Load();
                Player.Play();
            }
        }

        private void CupcakeButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.cupcake;
            {
                Player.Load();
                Player.Play();
            }
        }

        private void MilkButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.milk;
            {
                Player.Load();
                Player.Play();
            }
        }

        private void DoAnimation(Button button)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = new Duration(TimeSpan.FromSeconds(3));
            RotateTransform rt = new RotateTransform();
            button.RenderTransform = rt;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }
    }
}
