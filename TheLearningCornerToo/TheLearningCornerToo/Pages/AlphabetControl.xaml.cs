using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
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

namespace TheLearningCornerToo.Pages
{
    /// <summary>
    /// Interaction logic for AlphabetControl.xaml
    /// </summary>
    public partial class AlphabetControl : UserControl
    {
        private SoundPlayer _player = new SoundPlayer();

        public AlphabetControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //play instructions
            _player.Stream = Properties.Resources.alphabet_instructions;
            {
                _player.Load();
                _player.Play();
            }
        }


        //public void DisplayRandomImage()
        //{
        //    Random random = new Random();
        //    int num = random.Next(26) + 1;

        //    //RandomImage.Source = new BitmapImage(new Uri("../Images/Alphabet/{num}"));
        //}

        //public void DisplayTheAlphabetButtons()
        //{


        //    for (int i = 1; i <= 26; i++)
        //    {
        //        var button = new Button();
        //        {
        //            button.Name = "Button" + i;
        //            button.Style = TryFindResource("RegularStyle") as Style;


        //        };

        //    }
        //}


        private void Image_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            SoundPlayer player = new SoundPlayer();
            var image = AlphabetGrid.Children.OfType<Image>();
            string theName = image.GetType().Name;

            switch (theName)
            {
                case "LetterA":
                    {
                        player.Stream = Properties.Resources.a_apple;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "LetterB":
                    {
                        player.Stream = Properties.Resources.b_ballons;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "LetterC":
                    {
                        player.Stream = Properties.Resources.c_car;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "LetterD":
                    {
                        player.Stream = Properties.Resources.d_ducks;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "LetterE":
                    {
                        player.Stream = Properties.Resources.e_elephant;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "LetterF":
                    {
                        player.Stream = Properties.Resources.f_frog;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "LetterG":
                    {
                        player.Stream = Properties.Resources.g_guitar;
                        {
                            player.Load();
                            player.Play();
                        }

                        break;
                    }
                case "ButtonH":
                    {
                        player.Stream = Properties.Resources.h_hat;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "ButtonI":
                    {
                        player.Stream = Properties.Resources.I_ice;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
                case "ButtonJ":
                    {
                        player.Stream = Properties.Resources.j_juice;
                        {
                            player.Load();
                            player.Play();
                        }
                        break;
                    }
            }
        }

    }
}
