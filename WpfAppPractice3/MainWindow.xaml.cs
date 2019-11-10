using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppPractice3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// need variable to be set for higher or lower guess. variable natural state = 0, higher = 1, lower = -1
    /// duplicated and changed variable names for random number generation for second roll
    /// add "if" to check if higher or lower guess, then do check if number1 is higher or lower than number
    /// second roll must return a win state bool to trigger win/lose message

    public partial class MainWindow : Window
    {
        int HighLow = 0, numberA=0;


        public MainWindow()
        {
            
            InitializeComponent();
            
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            numberA = Update();
             
        }

        public int Update()
        {
            Random r = new Random();
            int number=0;
           
                number = r.Next(1, 21);

                tblkNumber.Text = number.ToString();
            return number;
            
        }

        private void rdioHigher_Checked(object sender, RoutedEventArgs e)
        {
            if (rdioHigher.IsChecked == true) HighLow = 1;
        }

        private void rdioLower_Checked(object sender, RoutedEventArgs e)
        {
            if (rdioLower.IsChecked == true) HighLow = -1;
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            Update1();
        }

        public void Update1()
        {
            Random r1 = new Random();
            
                int number1 = r1.Next(1, 21);

                tblkNumber1.Text = number1.ToString();

            if(HighLow == 1 && number1 > numberA)
            {
                tblkWinState.Text = "Winner!";
            }
            if (HighLow == 1 && number1 < numberA)
            {
                tblkWinState.Text = "Loser!";
            }
            if (HighLow == -1 && number1 > numberA)
            {
                tblkWinState.Text = "Loser!";
            }
            if (HighLow == -1 && number1 < numberA)
            {
                tblkWinState.Text = "Winner!";
            }

        }
    }
}
