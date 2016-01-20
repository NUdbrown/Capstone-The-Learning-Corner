﻿using System;
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
    /// Interaction logic for AlphabetLesson.xaml
    /// </summary>
    public partial class AlphabetLesson : Window
    {
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
    }
}