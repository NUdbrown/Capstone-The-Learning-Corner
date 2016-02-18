using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public AlphabetControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
           //DisplayRandomImage();
        }


        public void DisplayRandomImage()
        {
            Random random = new Random();
            int num = random.Next(26) + 1;
            
            //RandomImage.Source = new BitmapImage(new Uri("../Images/Alphabet/{num}"));
        }

        public void DisplayTheAlphabet()
        {
            
        }
    }
}
