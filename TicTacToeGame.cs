using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    internal class TicTacToeGame
    {
        private string[,] board = {
            {"1","2","3"},
            {"4","5","6"},
            {"7","8","9"}
        };

        private int playerNo = 1;
        private int turn = 0;

        public TicTacToeGame()
        {
 
        }

        public void Start()
        {
            bool isPlaying = true;
            
            
            while (isPlaying)
            {
                DisplayBoard();
                Console.WriteLine();

                // get player selected field
                int selected = getValidPlayerInput();
                MarkSelectedField(selected);
                turn++;

                // check for winner
                bool haveWinner = CheckWinner();
                if (haveWinner)
                {
                    // announce winner
                    EndGame(true);
                    // reset the game
                    reset();
                    continue;
                }

                // check if all field are entered
                if (turn == 9)
                {
                    // announce draw
                    EndGame(false);
                    // reset the game
                    reset();
                    continue;
                }

                // change player
                playerNo = playerNo == 1 ? 2 : 1;
                
                // clear the console
                Console.Clear();
            }
            
        }

        private void DisplayBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.WriteLine("     |     |     ");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write("  {0}  ", board[i,j]);
                    if (j != 2)
                    {
                        Console.Write("|");
                    } else
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("     |     |     ");
                if (i != 2)
                {
                    Console.WriteLine("-----------------");
                }
            }
        }

        private int getValidPlayerInput()
        {
            bool isValid = false;
            int input = 0;

            while (!isValid)
            {
                Console.Write("Player {0}: Choose your field! ", playerNo);
                input = GetPlayerInputNum();
                if (CheckDuplicatedFieldSelection(input))
                {
                    Console.WriteLine("This field has been selected, try again.");
                    continue;
                } else
                {
                    break;
                }
                
            }
            return input;
        }

        // get and return a integer from 1 - 9 
        private int GetPlayerInputNum()
        {
            String inputStr = Console.ReadLine();
            int inputNum;
            while (!int.TryParse(inputStr, out inputNum) || inputNum < 1 || inputNum > 9) 
            {
                Console.Write("Invalid input! Please enter 1-9 only: ");
                inputStr = Console.ReadLine();
            }

            return inputNum;
        }

        // return true if duplicate
        // false otherwise
        private bool CheckDuplicatedFieldSelection(int input)
        {
            int row = input <= 3 ? 0 : input <= 6 ? 1 : 2;

            int col = (input - 1) % 3;

            if (board[row, col].Equals("X") || board[row, col].Equals("O"))
            {
                return true;
            }

            return false;
        }

        private void MarkSelectedField(int selectedField)
        {
            int row = selectedField <= 3 ? 0 : selectedField <= 6 ? 1 : 2;
            int col = (selectedField - 1) % 3;

            string mark = playerNo == 1 ? "X" : "O";

            board[row, col] = mark;
        }

        private bool CheckWinner()
        {
            // check horizontal and vertical winner combos
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }

                if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    return true;
                }
            }

            // check for diagonal winner combos
            if (board[0,0] == board[1,1] && board[1,1] == board[2,2])
            {
                return true;
            }

            if (board[0,2] == board[1,1] && board[1,1] == board[2,0])
            {
                return true;
            }

            return false;
        }

        private void reset()
        {
            board = new string[,] {
                { "1","2","3"},
                { "4","5","6"},
                { "7","8","9"}
            };

            playerNo = 1;
            turn = 0;

            Console.Clear();
        }

        private void EndGame(bool hasWinner)
        {
            Console.Clear();
            DisplayBoard();
            if (hasWinner)
            {
                Console.WriteLine("Player {0} has won!", playerNo);
            } else
            {
                Console.WriteLine("We have a draw!");
            }
            
            Console.Write("Press any key to reset the game ");
            Console.ReadKey();
        }

    }
}
