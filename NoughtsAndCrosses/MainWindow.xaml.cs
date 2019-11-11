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

namespace NoughtsAndCrosses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[,] board = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };  //declares the 2d array that will represent the board
        int winState = 0;                                               //variable for win state of the game: 0 = game in progress, 1 = winner , 2 = draw 
        int move;                                                       //the variable for the players choice of square
        int row = 0;                                                    //variable for the index of row  of the board array corresponding to the players choice of square
        int column = 0;                                                 //variable for the index of column of the board array corresponding to the players choice of square
        int player = 1;                                                 //sets player to player 1 by default for the start of the game, will track turns by adding 1 after each turn and then checking for a remainder 

        public MainWindow()
        {
            InitializeComponent();
            txtBlkGameInfo.Background = Brushes.White;
            txtBlkGameInfo.Text = "Let's play Noughts and Crosses. Player one will be X's and player 2 will play O's.";
        }
        private void txtBlkEnterMove_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBlkEnterMove.Text = "";
        }
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            if (player % 2 != 0)                                        //if to check whose turn it is, a remainder of 0  = player 2, else player 1
            {
                txtBlkGameInfo.Background = Brushes.White;
                txtBlkGameInfo.Text = "Player 2, it's your turn to pick a square.";
            }
            else
            {
                txtBlkGameInfo.Background = Brushes.White;
                txtBlkGameInfo.Text = "Player 1, it's your turn to pick a square.";
            }

            move = int.Parse(txtBlkEnterMove.Text);                     //takes user input for their move as a single int
            switch (move)                                               //switch for the move variable, allows for translation of the player input of a single int to be translated to a pair of array indices 
            {
                case 1:                                                 //position 1 = 0,0 in the array, position 2 is 0,1 etc. this carries on till position 9 on the board which is 2,2
                    row = 0; column = 0; break;
                case 2:
                    row = 0; column = 1; break;
                case 3:
                    row = 0; column = 2; break;
                case 4:
                    row = 1; column = 0; break;
                case 5:
                    row = 1; column = 1; break;
                case 6:
                    row = 1; column = 2; break;
                case 7:
                    row = 2; column = 0; break;
                case 8:
                    row = 2; column = 1; break;
                case 9:
                    row = 2; column = 2; break;
            }
            if (board[row, column] != "X" && board[row, column] != "O") //checks that the point on the board is clear  
            {
                if (player % 2 != 0)                                    //if it's player 1's turn put an X in the array
                {
                    board[row, column] = "X";                           //updates the board array on player 1 turns 
                    player++;                                           //adds 1 to the player variable at the end of a successful turn
                }
                else                                                    //if it's player 2's turn put an O in the array
                {
                    board[row, column] = "O";                           //updates the board array on player 2 turns 
                    player++;                                           //adds 1 to the player variable at the end of a successful turn
                }
            }
            else                                                        //checks if a position is already occupied and asks them to try again if it is
            {
                txtBlkGameInfo.Background = Brushes.Red;
                txtBlkGameInfo.Text = $"Sorry, but position {move} is already occupied by an {board[row, column]}. Try another move.";
            }
            //output the values for the board array to the text boxes
            txtBlk1.Text = board[0, 0];
            txtBlk2.Text = board[0, 1];
            txtBlk3.Text = board[0, 2];
            txtBlk4.Text = board[1, 0];
            txtBlk5.Text = board[1, 1];
            txtBlk6.Text = board[1, 2];
            txtBlk7.Text = board[2, 0];
            txtBlk8.Text = board[2, 1];
            txtBlk9.Text = board[2, 2];
                 
            winState = CheckWinState(board);                            //calls the CheckWinState method to update the winState variable

            if (winState == 1)                                          //if winState = 1 then we have a winner
            {
                txtBlkGameInfo.Background = Brushes.Gold;
                txtBlkGameInfo.Text = $"Player {(player % 2) + 1} is the winner.";//, (player % 2) + 1; //winning player number is found by looking for a remainder again and adding 1 to correct the off by 1 error
                
                //btnMove_ClickBoardReset(sender, e); trying to make a reset board function for end game (currently fails due to no input in text box breaking the switch function)
            }
            if (winState == 2)                                          //if winState = 2 the match is a draw
            {
                txtBlkGameInfo.Background = Brushes.White;
                txtBlkGameInfo.Text = "The game was a draw";
            }
            txtBlkEnterMove.Text = "";                                  //clear text box ready for next move
            Keyboard.Focus(txtBlkEnterMove);                            //sets focus back on the text box again so that players don't need to click on it each time
        }
        
        public static int CheckWinState(string[,] board)                //a method to check for a win state

        {
            if ((board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2]) || (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2]) || (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2])) //winning condition for horizontal rows (is element a the same as element b and is b the same as c)
            {
                return 1;                                               //returns 1 for a win
            }
            else if ((board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0]) || (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1]) || (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2])) //winning condition for columns (is element a the same as element b and is b the same as c)
            {
                return 1;                                               //returns 1 for a win
            }
            else if ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) || (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])) //winning condition for diagonals (is element a the same as element b and is b the same as c)
            {
                return 1;                                               //returns 1 for a win
            }
            else if (board[0, 0] != "1" && board[0, 1] != "2" && board[0, 2] != "3" && board[1, 0] != "4" && board[1, 1] != "5" && board[1, 2] != "6" && board[2, 0] != "7" && board[2, 1] != "8" && board[2, 2] != "9") //checking for a filled board without having met a win state, ie: a draw
            {
                return 2;                                               //returns 2 for a draw
            }
            else
            {
                return 0;                                               //if no win state is met and the match hasn't reached a stalemate yet, then return 0 as the game is still in progress
            }
        }
        private void btnMove_ClickBoardReset(object sender, RoutedEventArgs e)  //trying to build a board reset function into the game
        {
            InitializeComponent();
        }

        private void btnResetBoard_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }
    }
}
