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
        private readonly SoundPlayer _player = new SoundPlayer();

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


        private void ImageA_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.a_apple;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageB_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.b_ballons;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageC_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.c_car;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageD_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.d_ducks;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageE_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.e_elephant;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageF_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.f_frog;
            {
                _player.Load();
                _player.Play();
            }
        }
        private void ImageG_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.g_guitar;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageH_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.h_hat;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageI_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.I_ice;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageJ_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.j_juice;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageK_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.k_kite;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageL_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.l_lion;
            {
                _player.Load();
                _player.Play();
            }
        }
        private void ImageM_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.m_monkey;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageN_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.n_nest;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageO_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.o_octopus;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageP_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.p_pig;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageQ_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.q_quarter;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageR_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.r_robot;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageS_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.s_sun;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageT_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.t_turtle;
            {
                _player.Load();
                _player.Play();
            }
        }
        private void ImageU_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.u_umbrella;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageV_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.v_vacuum;
            {
                _player.Load();
                _player.Play();
            }
        }
        private void ImageW_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.w_whale;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageX_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.x_xylophone;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageY_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.y_yarn;
            {
                _player.Load();
                _player.Play();
            }
        }

        private void ImageZ_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            _player.Stream = Properties.Resources.z_zebra;
            {
                _player.Load();
                _player.Play();
            }
        }
    }
}
