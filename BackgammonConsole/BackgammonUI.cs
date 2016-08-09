using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BackgammonLogic;

namespace BackgammonConsole
{
    public class BackgammonUI
    {
        private GameManager _gameManager = new GameManager("Black Player","White Player");
        private StringBuilder _stringBuilder = new StringBuilder();
        private string[,] _boardArray = new string[36, 18];
        private bool _flagIllegalMove = false;
        private bool _flagIllegalInput = false;

        private void CopyCheckersToBoardArray()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;
            string checkerType = "X";

            #region point on the board
            for (i = 0; i < 36; i++)
            {

                for (j = 0; j < 16; j++)
                {
                    if (j == 7 || j == 8 || i == 17 || i == 18)
                    {
                        _boardArray[i, j] = "   ";
                    }

                    else
                    {
                        _boardArray[i, j] = " . ";

                    }
                }
            } 
            #endregion

            #region Border of the board
            for (i = 0; i < 36; i++)
            {

                for (j = 0; j < 16; j++)
                {
                    if (j == 0 || j == 15)
                    {
                        _boardArray[i, j] = " | ";
                    }
                     if (i == 1 || i == 34)
                    {
                        _boardArray[i, j] = " - ";
                        
                    }

                }
            }
            #endregion

            #region bottom numbers in board
            for (j = 14, k = 0; j > 0; j--)
            {
                if (j != 7 && j != 8 && k <= 9)
                {
                    _boardArray[35, j] = $" {k} ";
                    k++;
                }
            }
            _boardArray[35, 1] = " 11";
            _boardArray[35, 2] = " 10";
            _boardArray[35, 17] = " 25";
            #endregion

            #region upper numbers in the board
            for (j = 14, k = 23; j > 0; j--)
            {
                if (j != 7 && j != 8)
                {
                    _boardArray[0, j] = $" {k}";
                    k--;
                }
            }
            _boardArray[0, 17] = " 24";

            #endregion

            #region put the checkers in their place (the Bottom half of the board)
            for (i = 0, k = 0; i < _gameManager.GameBoard.Triangles.Length / 2; i++)
            {
                for (j = 0; j < _gameManager.GameBoard.Triangles[i].NumberOfCheckers; j++)
                {
                    if (_gameManager.GameBoard.Triangles[i].CheckerColor == CheckerColor.BLACK)
                    {
                        checkerType = " X ";
                    }

                    if (_gameManager.GameBoard.Triangles[i].CheckerColor == CheckerColor.WHITE)
                    {
                        checkerType = " O ";
                    }

                    if (i == 7)
                    {
                        k = 2;
                    }
                    if (i == 6)
                    {
                        k = 2;
                    }

                    _boardArray[33 - j, 14 - i - k] = checkerType;

                }

            }
            #endregion

            #region put the checkers in their place (the Upper half of the board)
            for (i = 12, k = 0; i < _gameManager.GameBoard.Triangles.Length; i++, m++)
            {
                for (j = 0; j < _gameManager.GameBoard.Triangles[i].NumberOfCheckers; j++)
                {
                    if (_gameManager.GameBoard.Triangles[i].CheckerColor == CheckerColor.BLACK)
                    {
                        checkerType = " X ";
                    }

                    if (_gameManager.GameBoard.Triangles[i].CheckerColor == CheckerColor.WHITE)
                    {
                        checkerType = " O ";
                    }

                    if (m == 6)
                    {
                        k = 2;
                    }
                    if (m == 7)
                    {
                        k = 2;
                    }


                    _boardArray[2 + j, 1 + m + k] = checkerType;

                }

            }
            #endregion

            #region put checker at bar
            _boardArray[9, 7] = $"X={_gameManager.GetNumberOfBlackCheckersAtBar()}";
            _boardArray[27, 7] = $"O={_gameManager.GetNumberOfWhiteCheckersAtBar()}";
            #endregion

            _boardArray[9, 17] = $"X={_gameManager.GetNumberOfBlackCheckerOutSide()}";
            _boardArray[27, 17] = $"O={_gameManager.GetNumberOfWhiteCheckerOutSide()}";
        }

        private void PrintBoard()
        {
            
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;


            CopyCheckersToBoardArray();

            for (int i = 0; i < 36; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < 18; j++)
                {

                    Console.Write($"{_boardArray[i, j]}");
                }
                Console.WriteLine();
            }
        }
        private void PrintTurn()
        {
            if (_gameManager.BlackPlayer.IstMyTurn)
            {
                Console.WriteLine("\tPlayer X's Turn");
            }
            else
            {
                Console.WriteLine("\tPlayer O's Turn");
            }
        }
        private void PrintDiceRolls()
        {

            Console.WriteLine();
            Console.Write($"\tDice Rolls: {_gameManager.GameDice.ToString()}");
            if (_gameManager.GameDice.RolledDouble)
            {
                Console.Write($"  {_gameManager.GameDice.ToString()}");
            }
        }
        private void PrintPlayerHasNotAvailbleMoves()
        {
            Console.WriteLine();
            Console.WriteLine(_gameManager.BlackPlayer.IstMyTurn
                ? "\tX has not availble moves"
                : "\tO has not availble moves");
        }
        private void PrintWinner()
        {
            string winner = _gameManager.GetTheWinner().ToString();
            Console.Clear();
            _stringBuilder.Append("\n\n\n");
            _stringBuilder.Append("\tTHE WINNER IS:");
            _stringBuilder.Append($"\t{winner}");
            Console.WriteLine(_stringBuilder);
        }
        private void PrintForRefresh()
        {
           

            Console.Clear();
            PrintBoard();

            Console.WriteLine();
            Console.WriteLine();
            PrintTurn();
            PrintDiceRolls();
            Console.ForegroundColor = ConsoleColor.Red;

            if (_flagIllegalMove)
            {
                Console.WriteLine(" Illegal move");
            }

            if (_flagIllegalInput)
            {
                Console.WriteLine(" Illegal input");
            }
            Console.ForegroundColor = ConsoleColor.White;

            _flagIllegalMove = false;
            _flagIllegalInput = false;

           
        }

        private void HandleMovesFromBar()
        {
            
            int indexDestination = -1;
            bool firstParseResult = false;
            _flagIllegalInput = false;
            _flagIllegalMove = false;

            while (_gameManager.GetNumberOfCheckersAtBar() != 0 && (_gameManager.PlayerHasAvailbleMoves() || _gameManager.PlayerHasAvailbleMovesToEatFromBar()) && _gameManager.MoveLeft > 0)
            {
                Console.WriteLine();
                Console.WriteLine("\tplease insert move (destination index ): ");

                firstParseResult = int.TryParse(Console.ReadLine(), out indexDestination);

                if (firstParseResult)
                {
                    if (_gameManager.IsLegalRegularMoveFromBar( indexDestination) ||
                        _gameManager.IsLegalEatMoveFromBar( indexDestination))
                    {
                        _gameManager.SetLegalMoveFromBar( indexDestination);
                    }
                    else
                    {
                        _flagIllegalMove = true;
                    }

                }
                else
                {
                    _flagIllegalInput = true;
                }


                PrintForRefresh();
            }
        }
        private void HandleRegularMoves()
        {
            int indexSource = -1;
            int indexDestination = -1;
            bool firstPareResult = true;
            bool secondParseResult = true;
            _flagIllegalInput = false;
            _flagIllegalMove = false;

            while (_gameManager.GetNumberOfCheckersAtBar() ==0 && (_gameManager.PlayerHasAvailbleMoves() || _gameManager.PlayerHasAvailbleMovesToEat()) 
                && _gameManager.MoveLeft > 0 && !_gameManager.PlayerCanTakeOutCheckers()) 
            {
                Console.WriteLine();
                Console.WriteLine("\tplease insert move (source index then destination index ): ");

                    firstPareResult = int.TryParse(Console.ReadLine(), out indexSource);
                    secondParseResult = int.TryParse(Console.ReadLine(), out indexDestination);

                if (firstPareResult && secondParseResult )
                {
                    if (_gameManager.IsLegalRegularMove(indexSource, indexDestination) ||
                        _gameManager.IsLegalEatMove(indexSource, indexDestination))
                    {
                        _gameManager.SetLegalMove(indexSource, indexDestination);
                    }
                    else
                    {
                        _flagIllegalMove = true;
                    }

                }
                else
                {
                    _flagIllegalInput = true;
                }


                PrintForRefresh();

            }



        }
        private void HandleTakeOutCheckersFromHome()
        {
            int indexSource = -1;
            int indexDestination = -1;
            bool firstPareResult = true;
            bool secondParseResult = true;
            _flagIllegalInput = false;
            _flagIllegalMove = false;

            while (!_gameManager.IsGameOver&&_gameManager.GetNumberOfCheckersAtBar() == 0 &&
                _gameManager.PlayerCanTakeOutCheckers()  && _gameManager.MoveLeft > 0)
            {
                Console.WriteLine();
                Console.WriteLine("\tplease insert move (source index then destination index ): ");

                firstPareResult = int.TryParse(Console.ReadLine(), out indexSource);
                secondParseResult = int.TryParse(Console.ReadLine(), out indexDestination);

                if (firstPareResult && secondParseResult)
                {
                    if (_gameManager.IsLegalRegularMove(indexSource, indexDestination) ||
                        _gameManager.IsLegalEatMove(indexSource, indexDestination))
                    {
                        _gameManager.SetLegalMove(indexSource, indexDestination);
                    }
                    else if( _gameManager.PlayerHasAvailbleMovesToTakeOutCheckers() &&_gameManager.IsLegalTakeOutCheckerMove(indexSource,indexDestination))
                    {
                        _gameManager.SetLegalTakeOutCheckerMove(indexSource);
                    }
                    else
                    {
                        _flagIllegalMove = true;
                    }

                }
                else
                {
                    _flagIllegalInput = true;
                }


                PrintForRefresh();

            }
        }


        public void Start()
        {
          
            while (!_gameManager.IsGameOver)
            {
                Console.Clear();
                Console.SetWindowSize(Console.LargestWindowWidth/2, Console.LargestWindowHeight);
                
                PrintBoard();

                Console.WriteLine();
                Console.WriteLine();

                PrintTurn();
                _gameManager.GetDiceRolls();
                PrintDiceRolls();
                _gameManager.UpdateMoveLeft();

             
               HandleMovesFromBar();
           
               HandleRegularMoves();

               HandleTakeOutCheckersFromHome();

                if ( _gameManager.MoveLeft > 0)
                {

                  PrintPlayerHasNotAvailbleMoves();

                  Thread.Sleep(7000);
                   
                }

                _gameManager.SwapTurns();
            }

            PrintWinner();
            Thread.Sleep(10000);
           
        }


    }
}
