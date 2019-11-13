using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        //declare variables 
        string[,] board = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };  //declares the 2d array that will represent the board
        private readonly string[,] boardReset = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } }; //array to reset the board with 
        int winState = 0;                                               //variable for win state of the game: 0 = game in progress, 1 = winner , 2 = draw 
        int move;                                                       //variable holds the players choice of square        
        int row = 0;                                                    //variables for the index of row and columns of the board array corresponding to the players choice of square
        int column = 0;                                                 
        int player = 1;                                                 //sets player to player 1 by default for the start of the game, will track turns by adding 1 after each turn and then checking for a remainder 

        //main window initialised
        public MainWindow()
        {
            InitializeComponent();
            txtBlkGameInfo.Background = Brushes.White;
            txtBlkGameInfo.Text = "Let's play Noughts and Crosses. Player one will be X's and player 2 will play O's.";
        }

        //event handling functions
        private void txtBoxEnterMove_GotFocus(object sender, RoutedEventArgs e)//clears the text from the text block
        {
            txtBoxEnterMove.Text = "";
        }

        private void NmbrValidation(object sender, TextCompositionEventArgs e)//restricts characters entered into the text box to digits 1-9
        {
            Regex regex = new Regex("[^1-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)    //calls the PlayGame method when the move button is clicked
        {
            PlayGame();
        }

        private void txtBoxEnterMove_KeyDown(object sender, KeyEventArgs e)//calls the PlayGame method when the enter key is pressed in the textbox
        {
            if (e.Key == Key.Enter)
            {
                PlayGame();
            }
        }

        private void btnResetBoard_Click(object sender, RoutedEventArgs e)
        {
            board = BoardReset();                                       //calls method to reset the array holding the board
            winState = 0;                                               //reset all the variables and textbox and move focus back to the text box
            row = 0;
            column = 0;
            player = 1;
            txtBoxEnterMove.Text = "Choose your move";
            Keyboard.Focus(txtBoxEnterMove);
            DisplayBoard();                                             //call displayboard to show the reset board
            txtBlkGameInfo.Background = Brushes.White;                  //reset the buttons and text blocks to starting colours + display new welcome message
            btnResetBoard.Background = Brushes.White;
            txtBlkGameInfo.Text = "Playing again? Player one will be X's and player 2 will play O's.";                
        }

        //methods associated with the game itself
        private void PlayGame()                                         //method that runs the game
        { 
            move = 0;
            if (player % 2 != 0)                                        //if to check whose turn it is
            {
                txtBlkGameInfo.Background = Brushes.White;
                txtBlkGameInfo.Text = "Player 2, it's your turn to pick a square.";
            }
            else
            {
                txtBlkGameInfo.Background = Brushes.White;
                txtBlkGameInfo.Text = "Player 1, it's your turn to pick a square.";
            }
            if (txtBoxEnterMove.Text != "")                             //checks that the textbox has a value in it
            {
                move = int.Parse(txtBoxEnterMove.Text);                 //value enters the switch statement
                switch (move)                                           //switch for the move variable, allows for translation of the player input of a single int to be translated to a pair of array indices 
                {
                    case 1:                                             //position 1 = 0,0 in the array, position 2 is 0,1 etc. this carries on till position 9 on the board which is 2,2
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
                    if (player % 2 != 0)                                //if it's player 1's turn put an X in the array
                    {
                        board[row, column] = "X";                       //updates the board array on player 1 turns 
                        player++;                                       //adds 1 to the player variable at the end of a successful turn
                    }
                    else                                                //if it's player 2's turn put an O in the array
                    {
                        board[row, column] = "O";                       //updates the board array on player 2 turns 
                        player++;                                       //adds 1 to the player variable at the end of a successful turn
                    }
                }
                else                                                    //checks if a position is already occupied and asks them to try again if it is
                {
                    txtBlkGameInfo.Background = Brushes.Red;
                    txtBlkGameInfo.Text = $"Sorry, but position {move} is already occupied by an {board[row, column]}. Try another move.";
                }

                DisplayBoard();                                         //output the updated board 
                winState = CheckWinState(board);                        //calls the CheckWinState method to update the winState variable

                if (winState == 1)                                      //if winState = 1 then we have a winner
                {
                    txtBlkGameInfo.Background = Brushes.Gold;
                    txtBlkGameInfo.Text = $"Player {(player % 2) + 1} is the winner.";//, (player % 2) + 1; //winning player number is found by looking for a remainder again and adding 1 to correct the off by 1 error    
                    btnResetBoard.Background = Brushes.LawnGreen;
                    Keyboard.Focus(btnResetBoard);                      //sets keyboard focus on the reset board button if players are using the enter key to confirm moves
                }
                if (winState == 2)                                      //if winState = 2 the match is a draw
                {
                    txtBlkGameInfo.Background = Brushes.White;
                    txtBlkGameInfo.Text = "The game was a draw";
                    btnResetBoard.Background = Brushes.LawnGreen;
                    Keyboard.Focus(btnResetBoard);
                }
                if (winState == 3)                                      //if winState = 3 the match is a stalemate
                {
                    txtBlkGameInfo.Background = Brushes.White;
                    txtBlkGameInfo.Text = "The game has reached a stalemate.";
                    btnResetBoard.Background = Brushes.LawnGreen;
                    Keyboard.Focus(btnResetBoard);
                }
                else if (winState == 0)
                {
                    txtBoxEnterMove.Text = "";                          //clear text box ready for next move
                    Keyboard.Focus(txtBoxEnterMove);                    //sets focus back on the text box again so that players don't need to click on it each time
                }
            }
            else if (txtBoxEnterMove.Text == "")                        //in the case of no value being entered when button is clicked, prompts user to enter a valid one
            {
                txtBlkGameInfo.Background = Brushes.Red;
                txtBlkGameInfo.Text = "Please choose a valid move";
                txtBoxEnterMove.Text = "";                              //clear text box ready for next move
                Keyboard.Focus(txtBoxEnterMove);
            }      
        }

        private int CheckWinState(string[,] board)               //a method to check for a win state

        {
            int r1ContainsO = 0, r1ContainsX = 0, r2ContainsO = 0, r2ContainsX = 0, r3ContainsO = 0, r3ContainsX = 0;
            int c1ContainsO = 0, c1ContainsX = 0, c2ContainsO = 0, c2ContainsX = 0, c3ContainsO = 0, c3ContainsX = 0;
            int d1ContainsO = 0, d1ContainsX = 0, d2ContainsO = 0, d2ContainsX = 0;
            int r1Stalemate = 0, r2Stalemate = 0, r3Stalemate = 0, c1Stalemate = 0, c2Stalemate = 0, c3Stalemate = 0, d1Stalemate = 0, d2Stalemate = 0, stalemate = 0;
            
            if (board[0, 0] == "X" || board[0, 1] == "X" || board[0, 2] == "X")  //checks if any elements of row1 contain X
            {
                r1ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[0, 0] == "O" || board[0, 1] == "O" || board[0, 2] == "O")  //checks if any elements of a row contain O
            {
                r1ContainsO = 1;                                               //returns 1 if an O is found
            }
            if (r1ContainsO == 1 && r1ContainsX == 1)              //checks to see if both an X and an O are found in the row and returns 1 if they are
            {
                r1Stalemate = 1; stalemate++;
            }
            if (board[1, 0] == "X" || board[1, 1] == "X" || board[1, 2] == "X")  //checks if any elements of row2 contain X
            {
                r2ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[1, 0] == "O" || board[1, 1] == "O" || board[1, 2] == "O")  //winning condition for horizontal rows (is element a the same as element b and is b the same as c)
            {
                r2ContainsO = 1;                                               //returns 1 for a win
            }
            if (r2ContainsO == 1 && r2ContainsX == 1)               //checks to see if both an X and an O are found in the second row and returns 1 if they both are
            {
                r2Stalemate = 1; stalemate++;
            }
            if (board[2, 0] == "X" || board[2, 1] == "X" || board[2, 2] == "X")  //checks if any elements of row2 contain X
            {
                r3ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[2, 0] == "O" || board[2, 1] == "O" || board[2, 2] == "O")  //winning condition for horizontal rows (is element a the same as element b and is b the same as c)
            {
                r3ContainsO = 1;                                               //returns 1 for a win
            }
            if (r3ContainsO == 1 && r3ContainsX == 1)               //checks to see if both an X and an O are found in the second row and returns 1 if they both are
            {
                r3Stalemate = 1; stalemate++;
            }
            if (board[0, 0] == "X" || board[1, 0] == "X" || board[2, 0] == "X")  //checks if any elements of column1 contain X
            {
                c1ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[0, 0] == "O" || board[1, 0] == "O" || board[2, 0] == "O")  //checks if any elements of a column contain O
            {
                c1ContainsO = 1;                                               //returns 1 if an O is found
            }
            if (c1ContainsO == 1 && c1ContainsX == 1)               //checks to see if both an X and an O are found in the column and returns 1 if they are
            {
                c1Stalemate = 1; stalemate++;
            }
            if (board[0, 1] == "X" || board[1, 1] == "X" || board[2, 1] == "X")  //checks if any elements of column2 contain X
            {
                c2ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[0, 1] == "O" || board[1, 1] == "O" || board[2, 1] == "O")  //checks if any elements of column 2 contain O
            {
                c2ContainsO = 1;                                               //returns 1 if an O is found
            }
            if (c2ContainsO == 1 && c2ContainsX == 1)               //checks to see if both an X and an O are found in the column and returns 1 if they are
            {
                c2Stalemate = 1; stalemate++;
            }
            if (board[0, 2] == "X" || board[1, 2] == "X" || board[2, 2] == "X")  //checks if any elements of column2 contain X
            {
                c3ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[0, 2] == "O" || board[1, 2] == "O" || board[2, 2] == "O")  //checks if any elements of column 2 contain O
            {
                c2ContainsO = 1;                                               //returns 1 if an O is found
            }
            if (c3ContainsO == 1 && c3ContainsX == 1)               //checks to see if both an X and an O are found in the column and returns 1 if they are
            {
                c3Stalemate = 1; stalemate++;
            }
            if (board[0, 0] == "X" || board[1, 1] == "X" || board[2, 2] == "X")  //checks if any elements of diagonal1 contain X
            {
                d1ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[0, 0] == "O" || board[1, 1] == "O" || board[2, 2] == "O")  //checks if any elements of diagonal1 contain O
            {
                d1ContainsO = 1;                                               //returns 1 if an O is found
            }
            if (d1ContainsO == 1 && d1ContainsX == 1)               //checks to see if both an X and an O are found in the diagonal1 and returns 1 if they are
            {
                d1Stalemate = 1; stalemate++;
            }
            if (board[2, 0] == "X" || board[1, 1] == "X" || board[0, 2] == "X")  //checks if any elements of diagonal2 contain X
            {
                d2ContainsX = 1;                                               //returns 1 if an X is found
            }
            if (board[2, 0] == "O" || board[1, 1] == "O" || board[0, 2] == "O")  //checks if any elements of diagonal2 contain O
            {
                d2ContainsO = 1;                                               //returns 1 if an O is found
            }
            if (d2ContainsO == 1 && d2ContainsX == 1)               //checks to see if both an X and an O are found in the diagonal2 and returns 1 if they are
            {
                d2Stalemate = 1; stalemate++;
            }
            txtblkStalemate.Text = $"{stalemate}";
            if (stalemate > 7)
            {
                
                return 3; //return winstate of 3  -- add new winstate == 3 where you output to text box that stalemate might be reached and highlight the reset board button
            }

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
       
        private void DisplayBoard()                                     //method to display the game board stored in the board array
        {
            txtBlk1.Text = board[0, 0];
            txtBlk2.Text = board[0, 1];
            txtBlk3.Text = board[0, 2];
            txtBlk4.Text = board[1, 0];
            txtBlk5.Text = board[1, 1];
            txtBlk6.Text = board[1, 2];
            txtBlk7.Text = board[2, 0];
            txtBlk8.Text = board[2, 1];
            txtBlk9.Text = board[2, 2];
        }

        private string[,] BoardReset()                                  //method to clone the boardReset array, (stops the board reset function only working once)
        {
            return (string[,])boardReset.Clone();                       //clones the array to stop the original being altered
        }
    }
}
