using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Media;
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
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Ink;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;
using Microsoft.Samples.Kinect.SpeechBasics;
using TheLearningCornerToo.Pages;


namespace TheLearningCornerToo
{
    /// <summary>
    /// Interaction logic for ColorLesson.xaml
    /// </summary>
    public partial class ColorLesson : Window
    {
       
        /// <summary>
        /// 
        /// </summary>
        private SoundPlayer Player { get; } = new SoundPlayer();
        private readonly Random _random = new Random(10);
        private readonly List<SolidColorBrush> _colorBrushes = new List<SolidColorBrush>()
        {
            new SolidColorBrush(Colors.Black),
            new SolidColorBrush(Colors.SaddleBrown),
            new SolidColorBrush(Colors.Red),
            new SolidColorBrush(Colors.DarkOrange),
            new SolidColorBrush(Colors.Yellow),
            new SolidColorBrush(Colors.Green),
            new SolidColorBrush(Colors.Blue),
            new SolidColorBrush(Colors.Purple),
            new SolidColorBrush(Colors.HotPink),
            new SolidColorBrush(Colors.White),           
        };
        
        /// <summary>
        /// Stream for 32b-16b conversion.
        /// </summary>
        private KinectAudioStream convertStream = null;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine speechEngine = null;

        /// <summary>
        /// Create an instance of your kinect sensor
        /// </summary>
        public KinectSensor CurrentSensor;

        //and the speech recognition engine (SRE)
        private SpeechRecognitionEngine speechRecognizer;


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
                this.convertStream = new KinectAudioStream(audioStream);
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
                this.speechEngine = new SpeechRecognitionEngine(ri.Id);

                // Create a grammar from grammar definition XML file.
                using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.SpeechGrammar)))
                {
                    var g = new Grammar(memoryStream);
                    this.speechEngine.LoadGrammar(g);
                }

                this.speechEngine.SpeechRecognized += SpeechRecognized;
                this.speechEngine.SpeechRecognitionRejected += SpeechRejected;

                // let the convertStream know speech is going active
                this.convertStream.SpeechActive = true;

                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
                ////speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

                this.speechEngine.SetInputToAudioStream(
                    this.convertStream, new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                this.speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
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

        //returns one of the colors
        private SolidColorBrush PickRandomColor()
        {
            //int picked = 0;
            //int theRandom = random.Next(_colorBrushes.Count);
            //Int32.TryParse(theRandom.ToString(), out picked);
            //return _colorBrushes.IndexOf(picked);
            return _colorBrushes[_random.Next(_colorBrushes.Count)];
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
            if (null != this.convertStream)
            {
                this.convertStream.SpeechActive = false;
            }

            if (null != this.speechEngine)
            {
                this.speechEngine.SpeechRecognized -= this.SpeechRecognized;
                this.speechEngine.SpeechRecognitionRejected -= this.SpeechRejected;
                this.speechEngine.RecognizeAsyncStop();
            }

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
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double confidenceThreshold = 0.5;

            this.ClearRecognitionHighlights();

            if (e.Result.Confidence >= confidenceThreshold)
            {
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "BLACK":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.Black);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.Black).Color;
                        break;

                    case "BROWN":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.SaddleBrown);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.SaddleBrown).Color;

                        break;

                    case "RED":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.Red);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.Red).Color;

                        break;

                    case "ORANGE":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.DarkOrange);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.DarkOrange).Color;

                        break;

                    case "YELLOW":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.Yellow);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.Yellow).Color;
                        break;

                    case "GREEN":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.Green);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.Green).Color;
                        break;

                    case "BLUE":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.Blue);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.Blue).Color;

                        break;

                    case "PURPLE":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.Purple);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.Purple).Color;

                        break;

                    case "PINK":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.HotPink);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.HotPink).Color;
                        break;

                    case "WHITE":
                        ColoredCircle.Fill = new SolidColorBrush(Colors.White);
                        ColoredCircle.Stroke = new SolidColorBrush(Colors.DimGray);
                        InkCanvas.DefaultDrawingAttributes.Color = new SolidColorBrush(Colors.White).Color;
                        break;
                }
            }
        }

        private void ClearRecognitionHighlights()
        {
            ColoredCircle.Fill = new SolidColorBrush(Colors.Transparent);
            ColoredCircle.Stroke = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Handler for rejected speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            this.StatusBarText.Text = Properties.Resources.DidNotUnderstand;

        }

        private void DrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
            InkCanvas.UseCustomCursor = true;
           

        }

        private void EraserButton_OnClick(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            InkCanvas.UseCustomCursor = false;
            InkCanvas.EraserShape = new RectangleStylusShape(10, 10);

        }
    }


}
