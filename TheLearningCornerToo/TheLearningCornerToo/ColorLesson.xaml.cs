﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;
using Microsoft.Samples.Kinect.SpeechBasics;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;

namespace TheLearningCornerToo
{
    /// <summary>
    /// Interaction logic for ColorLesson.xaml
    /// </summary>
    public partial class ColorLesson : Window
    {   
        private readonly SolidColorBrush _blackbSolidColorBrush = new SolidColorBrush(Colors.Black);
        private readonly SolidColorBrush _brownSolidColorBrush = new SolidColorBrush(Colors.SaddleBrown);
        private readonly SolidColorBrush _redSolidColorBrush =   new SolidColorBrush(Colors.Red);
        private readonly SolidColorBrush _orangeSolidColorBrush = new SolidColorBrush(Colors.DarkOrange);
        private readonly SolidColorBrush _yellowSolidColorBrush = new SolidColorBrush(Colors.Yellow);
        private readonly SolidColorBrush _greenSolidColorBrush = new SolidColorBrush(Colors.Green);
        private readonly SolidColorBrush _blueSolidColorBrush = new SolidColorBrush(Colors.Blue);
        private readonly SolidColorBrush _purpleSolidColorBrush = new SolidColorBrush(Colors.Purple);
        private readonly SolidColorBrush _pinkSolidColorBrush = new SolidColorBrush(Colors.HotPink);
        private readonly SolidColorBrush _whiteSolidColorBrush = new SolidColorBrush(Colors.White);

        private BodyFrameReader _bodyReader = null;
        private IList<Body> _bodies = null;

       /// <summary>
        /// SoundPlayer for app audio
        /// </summary>
        private SoundPlayer Player { get; } = new SoundPlayer();
        
        /// <summary>
        /// Stream for 32b-16b conversion.
        /// </summary>
        private KinectAudioStream _convertStream = null;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine _speechEngine = null;

        /// <summary>
        /// Create an instance of your kinect sensor
        /// </summary>
        public KinectSensor CurrentSensor;
        
        public ColorLesson()
        {
            InitializeComponent();
            //show person's body & moving cursor
            KinectRegion.SetKinectRegion(this, KinectArea);

            App app = ((App)Application.Current);
            app.KinectRegion = KinectArea;
            app.KinectRegion.CursorSpriteSheetDefinition = new CursorSpriteSheetDefinition(new System.Uri("pack://application:,,,/Images/CursorSpriteSheetPurple.png"), 4, 20, 137, 137);
            Loaded += WindowLoaded;
        }

        /// <summary>
        /// Execute initialization tasks.
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
           LoadCrayons(sender, e);
            // Only one sensor is supported
           CurrentSensor = KinectSensor.GetDefault();

            if (CurrentSensor != null)
            {
                // open the sensor
                CurrentSensor.Open();

                // grab the audio stream
                IReadOnlyList<AudioBeam> audioBeamList = CurrentSensor.AudioSource.AudioBeams;
                System.IO.Stream audioStream = audioBeamList[0].OpenInputStream();

                // create the convert stream
                this._convertStream = new KinectAudioStream(audioStream);
                
                _bodyReader = CurrentSensor.BodyFrameSource.OpenReader();
                _bodyReader.FrameArrived += BodyReader_FrameArrived;

                _bodies = new Body[CurrentSensor.BodyFrameSource.BodyCount];
                
            }
            else
            {
               // on failure, set the status text
                this.StatusBarText.Text = Properties.Resources.NoKinectReady;
                return;
            }

            RecognizerInfo ri = TryGetKinectRecognizer();

            if (null != ri)
            {
                this._speechEngine = new SpeechRecognitionEngine(ri.Id);

                // Create a grammar from grammar definition XML file.
                using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.SpeechGrammar)))
                {
                    var g = new Grammar(memoryStream);
                    this._speechEngine.LoadGrammar(g);
                }

                this._speechEngine.SpeechRecognized += SpeechRecognized;
                this._speechEngine.SpeechRecognitionRejected += SpeechRejected;

                // let the convertStream know speech is going active
                this._convertStream.SpeechActive = true;

                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
               // _speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

                this._speechEngine.SetInputToAudioStream(
                    this._convertStream, new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                this._speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                this.StatusBarText.FontSize = 45;
                this.StatusBarText.Text = Properties.Resources.NoSpeechRecognizer;
            }
        }

        //Get the speech recognizer (SR)
        private static RecognizerInfo TryGetKinectRecognizer()
        {
            IEnumerable<RecognizerInfo> recognizers;

            // This is required to catch the case when an expected recognizer is not installed.
            // By default - the x86 Speech Runtime is always expected. 
            try
            {
                recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            }
            catch (COMException)
            {
                return null;
            }

            foreach (RecognizerInfo recognizer in recognizers)
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }

            return null;
        }


        private void LoadCrayons(object sender, RoutedEventArgs e)
        {        
            //add buttons to screen
            for (int i = 1; i <= 10; i++)
            {
                var button = new Button();
                {
                   button.Name = "Button" + i;
                   button.RenderTransform = new RotateTransform(-60,0.5,0.5);
                   
                    switch (button.Name)
                    {
                        case "Button1":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_black.png")));                              
                                break;
                            }
                        case "Button2":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_brown.png")));
                                break;
                            }
                        case "Button3":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_red.png")));
                                break;
                            }
                        case "Button4":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_orange.png")));
                                break;
                            }
                        case "Button5":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_yellow.png")));
                                break;
                            }
                        case "Button6":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_green.png")));
                                break;
                            }
                        case "Button7":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_blue.png")));
                                break;
                            }
                        case "Button8":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_purple.png")));
                                break;
                            }
                        case "Button9":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_pink.png")));
                                break;
                            }
                        case "Button10":
                            {
                                button.Background =
                                    new ImageBrush(
                                        new BitmapImage(
                                            new System.Uri("pack://application:,,,/Images/Colors/color_white.png")));
                                break;
                            }
                    }
                };
                button.Style = TryFindResource("ColorButtonStyle") as Style;
                button.Click += (sender1, routedEventArgs) => Crayon_OnClick(button, sender1, routedEventArgs);
                ScrollContent.Children.Add(button);
            }
            //give instructions once page is shown
            Player.Stream = Properties.Resources.color_instructions;
            {
                Player.Load();
                Player.Play();

            }

        }       

        private void Crayon_OnClick(Button thebutton, object sender, RoutedEventArgs routedEventArgs)
        {          
            var name = thebutton.Name;
            switch (name)
            {
                case "Button1":
                    {
                        Player.Stream = Properties.Resources.black;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button2":
                    {
                        Player.Stream = Properties.Resources.brown;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button3":
                    {
                        Player.Stream = Properties.Resources.red;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button4":
                    {
                        Player.Stream = Properties.Resources.orange;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button5":
                    {
                        Player.Stream = Properties.Resources.yellow;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button6":
                    {
                        Player.Stream = Properties.Resources.green;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button7":
                    {
                        Player.Stream = Properties.Resources.blue;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button8":
                    {
                        Player.Stream = Properties.Resources.purple;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button9":
                    {
                        Player.Stream = Properties.Resources.pink;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
                case "Button10":
                    {
                        Player.Stream = Properties.Resources.white;
                        {
                            Player.Load();
                            Player.Play();
                        }
                        break;
                    }
            }
        }

        private void Instructions_OnClick(object sender, RoutedEventArgs e)
        {
            Player.Stream = Properties.Resources.color_instructions;
            {
                Player.Load();
                Player.Play();
            }
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != this._convertStream)
            {
                this._convertStream.SpeechActive = false;
            }

            if (null != this._speechEngine)
            {
                this._speechEngine.SpeechRecognized -= this.SpeechRecognized;
                this._speechEngine.SpeechRecognitionRejected -= this.SpeechRejected;
                this._speechEngine.RecognizeAsyncStop();
            }

            _bodyReader?.Dispose();

            if (null != CurrentSensor)
            {
                CurrentSensor.Close();
                CurrentSensor = null;
            }

            Application.Current.Shutdown();
        }


        /// <summary>
        /// Handler for recognized speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence = we treat speech as if it hadn't been heard
            const double confidenceThreshold = 0.5;

            this.ClearRecognitionHighlights();

            if (e.Result.Confidence >= confidenceThreshold)
            {
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "BLACK":
                        ColoredCircle.Fill = _blackbSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _blackbSolidColorBrush;
                        break;

                    case "BROWN":
                        ColoredCircle.Fill = _brownSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _brownSolidColorBrush;

                        break;

                    case "RED":
                        ColoredCircle.Fill = _redSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _redSolidColorBrush;

                        break;

                    case "ORANGE":
                        ColoredCircle.Fill = _orangeSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _orangeSolidColorBrush;

                        break;

                    case "YELLOW":
                        ColoredCircle.Fill = _yellowSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _yellowSolidColorBrush;
                        break;

                    case "GREEN":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.Green);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _greenSolidColorBrush;
                        break;

                    case "BLUE":
                        ColoredCircle.Fill = _blueSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _blueSolidColorBrush;

                        break;

                    case "PURPLE":
                        ColoredCircle.Fill =_purpleSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _purpleSolidColorBrush;

                        break;

                    case "PINK":
                        ColoredCircle.Fill = _pinkSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _pinkSolidColorBrush;
                        break;

                    case "WHITE":
                        ColoredCircle.Fill = _whiteSolidColorBrush;
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.YellowGreen);
                        PaintLine.Stroke = _whiteSolidColorBrush;
                        break;
                }
            }
        }

        private void ClearRecognitionHighlights()
        {
            ColoredCircle.Fill = new SolidColorBrush(Colors.Transparent);
            ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
        }

        /// <summary>
        /// Handler for rejected speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            this.StatusBarText.Text = Properties.Resources.DidNotUnderstand;
            //add new voice-over that tells the child to repeat the color because the software didn't understand

        }


        private void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    frame.GetAndRefreshBodyData(_bodies);

                    Body body = _bodies.Where(b => b.IsTracked).FirstOrDefault();


                    if (body != null)
                    {
                        Joint handRight = body.Joints[JointType.HandRight];
                        //Joint handLeft = body.Joints[JointType.HandLeft];

                        if (handRight.TrackingState != TrackingState.NotTracked)
                        {
                            CameraSpacePoint handRightPosition = handRight.Position;
                            ColorSpacePoint handRightPoint = CurrentSensor.CoordinateMapper.MapCameraPointToColorSpace(handRightPosition);

                            float x = handRightPoint.X;
                            float y = handRightPoint.Y;
                            
                            if (!float.IsInfinity(x) && !float.IsInfinity(y))
                            {
                                // DRAW!
                                PaintLine.Points.Add(new Point { X = x, Y = y });

                                Canvas.SetLeft(ThePaintBrush, x - ThePaintBrush.Width / 2.0);
                                Canvas.SetTop(ThePaintBrush, y - ThePaintBrush.Height);
                            }
                        }
                    }

                }
            }
        }

        //private void EraserButton_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var body = _bodies.FirstOrDefault(b => b.IsTracked);

        //    if (body != null)
        //    {
        //        Joint handRight = body.Joints[JointType.HandRight];

        //        if (handRight.TrackingState != TrackingState.NotTracked)
        //        {
        //            CameraSpacePoint handRightPosition = handRight.Position;
        //            ColorSpacePoint handRightPoint = CurrentSensor.CoordinateMapper.MapCameraPointToColorSpace(handRightPosition);

        //            float x = handRightPoint.X;
        //            float y = handRightPoint.Y;

        //            if (!float.IsInfinity(x) && !float.IsInfinity(y))
        //            {
        //                // DRAW!
                        

        //                //Canvas.SetLeft(brush, x - brush.Width / 2.0);
        //                //Canvas.SetTop(brush, y - brush.Height);
        //            }
        //        }
        //    }

        //}
    }


}
