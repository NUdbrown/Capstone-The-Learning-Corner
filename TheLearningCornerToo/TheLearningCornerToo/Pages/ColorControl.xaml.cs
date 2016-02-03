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
            InitializeComponent();
        }

        private void BlackButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.blackberries;
            //{
            //    Player.Load();
            //    Player.Play();
            DoRotationAnimation(BlackButton);
            //}


        }

        private void BrownButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.kiwis;
            //{
            //    Player.Load();
            //    Player.Play();
            DoRotationAnimation(TeddyButton);
            //}
        }

        private void RedButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.apples;
            //{
            //    Player.Load();
            //    Player.Play();
            DoRotationAnimation(AppleButton);
            //}
        }

        private void OrangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.oranges;
            //{
            //    Player.Load();
            //    Player.Play();
            DoRotationAnimation(PumpkinButton);
            //}
        }
        private void YellowButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.bananas;
            //{
            //    Player.Load();
            //    Player.Play();
            DoRotationAnimation(DuckiesButton);
            //}
        }

        private void GreenButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.pears;
            //{
            //    Player.Load();
            //    Player.Play();
            DoOppositeRotationAnimation(FroggyButton);
            //}
        }

        private void BlueButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.blueberries;
            //{
            //    Player.Load();
            //    Player.Play();
            DoOppositeRotationAnimation(BaloonsButton);
            //}
        }

        private void PurpleButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.grapes;
            //{
            //    Player.Load();
            //    Player.Play();
            DoOppositeRotationAnimation(OctopusButton);
            //}
        }

        private void PinkButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.cupcake;
            //{
            //    Player.Load();
            //    Player.Play();
            DoOppositeRotationAnimation(PiggyButton);
            //}
        }

        private void WhiteButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Player.Stream = Properties.Resources.milk;
            //{
            //    Player.Load();
            //    Player.Play();
            DoOppositeRotationAnimation(CloudsButton);
            //}
        }

        private void DoRotationAnimation(Button button)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = new Duration(TimeSpan.FromSeconds(3));
            RotateTransform rt = new RotateTransform();
            button.RenderTransform = rt;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }

        private void DoOppositeRotationAnimation(Button button)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 360;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromSeconds(4));
            RotateTransform rt = new RotateTransform();
            button.RenderTransform = rt;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }
    }
}
