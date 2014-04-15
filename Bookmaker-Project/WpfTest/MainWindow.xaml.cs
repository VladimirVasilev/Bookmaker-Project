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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class PersonThrowException
    {
        private int age;

        public int Age
        {
            get { return age; }
            set
            {

                if (value < 0 || value > 150)
                {
                    throw new ArgumentException("Age must not be less than 0 or greater than 150.");
                }
                age = value;
            }
        }
    }
}
