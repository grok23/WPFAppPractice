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

namespace OddEven
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

        private void txtBoxNumberIn_GotFocus(object sender, RoutedEventArgs e)  //blanks textbox on focus
        {
            txtBoxNumberIn.Text = "";
        }

        private void OddButton_Click(object sender, RoutedEventArgs e)      //checks if number is odd or even by seeing if there is a remainder after dviding by 2
        {
            int number;                                                     //variable for number if string converts to int successfully
            bool ifNumber = int.TryParse(txtBoxNumberIn.Text, out number);  //bool ifNumber is used to hold result of a try parse check on the text box contents

            if (ifNumber == true)                                           //checks bool ifNumber in case of result true 
            {
                if (number % 2 == 0)                                        //checks odd or even and outputs appropriate answer
                {
                    TxtBlkAnswer.Background = Brushes.Green;
                    TxtBlkAnswer.Foreground = Brushes.White;
                    TxtBlkAnswer.Text = "Number is Even";
                }
                else
                {
                    TxtBlkAnswer.Background = Brushes.LightBlue;
                    TxtBlkAnswer.Foreground = Brushes.White;
                    TxtBlkAnswer.Text = "Number is Odd";
                }
            }
            else                                                            //in case of false displays invalid entry message
            {
                TxtBlkAnswer.Background = Brushes.Red;
                TxtBlkAnswer.Foreground = Brushes.Black;
                TxtBlkAnswer.Text = "Invalid Entry";
            }
        }
    }
}
