using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonLogic
{
    public class GameManager 
    {
        public BlackPlayer BlackPlayer { get; private set; }
        public WhitePlayer WhitePlayer { get; private set; }


        public Board GameBoard { get; private set; }
        public Dice GameDice { get; private set; }

        public bool IsGameOver { get; set; } = false;

        public int MoveLeft { get; set; }

        public GameManager(string blackName, string whiteName)
        {
            if (string.IsNullOrEmpty(blackName) || string.IsNullOrEmpty(whiteName))
            {
                throw new ArgumentNullException($"{nameof(blackName)} or {nameof(whiteName)}");

            }
            BlackPlayer = new BlackPlayer(blackName, CheckerColor.BLACK);
            WhitePlayer = new WhitePlayer(whiteName, CheckerColor.WHITE);

            GameDice = new Dice();
            GameBoard = new Board();

            BlackPlayer.IstMyTurn = true;
            WhitePlayer.IstMyTurn = false;

        }

        public int GetNumberOfCheckersAtHome()
        {
            if (BlackPlayer.IstMyTurn)
            {
                return BlackPlayer.CheckersAtHome;
            }

            return WhitePlayer.CheckersAtHome;
        }
        public int GetNumberOfCheckersAtBar()
        {
            if (BlackPlayer.IstMyTurn)
            {
                return GameBoard.GameBar.NumberOfBlackCheckers;
            }

            return GameBoard.GameBar.NumberOfWhiteCheckers;
        }
        public int GetNumberOfBlackCheckersAtBar()
        {
            return GameBoard.GameBar.NumberOfBlackCheckers;

        }
        public int GetNumberOfWhiteCheckersAtBar()
        {
            return GameBoard.GameBar.NumberOfWhiteCheckers;

        }
        public int GetNumberOfBlackCheckerOutSide()
        {

            return BlackPlayer.NumberOfCheckerOutSide;
        }
        public int GetNumberOfWhiteCheckerOutSide()
        {

            return WhitePlayer.NumberOfCheckerOutSide;
        }

        public void GetDiceRolls()
        {
            GameDice.RollDice();
        }
        public CheckerColor GetTheWinner()
        {
            if (BlackPlayer.NumberOfCheckerOutSide == 15)
            {
                return CheckerColor.BLACK;
            }
            if (WhitePlayer.NumberOfCheckerOutSide == 15)
            {
                return CheckerColor.WHITE;
            }
            return CheckerColor.NONE;
        }

        public bool PlayerHasAvailbleMoves()
        {
            if (BlackPlayer.IstMyTurn)
            {
                return BlackPlayer.HasAvailbleMoves(GameBoard, GameDice);
            }

            return WhitePlayer.HasAvailbleMoves(GameBoard, GameDice);
        }
        public bool PlayerHasAvailbleMovesToTakeOutCheckers()
        {
            if (BlackPlayer.IstMyTurn)
            {
                return BlackPlayer.HasAvailbleMovesToTakeOut(GameBoard, GameDice);
            }

            return WhitePlayer.HasAvailbleMovesToTakeOut(GameBoard, GameDice);
        }
        public bool PlayerHasAvailbleMovesToEat()
        {
            if (BlackPlayer.IstMyTurn)
            {
                return BlackPlayer.HasAvailbleMovesToEat(GameBoard, GameDice);
            }

            return WhitePlayer.HasAvailbleMovesToEat(GameBoard, GameDice);
        }
        public bool PlayerHasAvailbleMovesToEatFromBar()
        {
            if (BlackPlayer.IstMyTurn)
            {
                return BlackPlayer.HasAvailbleMovesToEatFromBar(GameBoard, GameDice);
            }

            return WhitePlayer.HasAvailbleMovesToEatFromBar(GameBoard, GameDice);
        }
        public bool PlayerCanTakeOutCheckers()
        {
            if (BlackPlayer.IstMyTurn)
            {
                return BlackPlayer.CanTakeOutCheckers(GameBoard);
            }

            return WhitePlayer.CanTakeOutCheckers(GameBoard);
        }

        public void ResetCubes(int cubeChosed)
        {

            if (GameDice.RolledDouble == false)
            {
                if (GameDice.FirstCube == cubeChosed)
                {
                    GameDice.ResetFirstCube();
                }

                if (GameDice.SecondCube == cubeChosed)
                {
                    GameDice.ResetSecondCube();
                }
            }
        }
        public void UpdateMoveLeft()
        {
            MoveLeft = GameDice.RolledDouble ? 4 : 2;
        }
        public void SwapTurns()
        {
            BlackPlayer.IstMyTurn = !BlackPlayer.IstMyTurn;
            WhitePlayer.IstMyTurn = !WhitePlayer.IstMyTurn;

        }

        public bool IsLegalRegularMove(int indexSource, int indexDestination)
        {
            int blackMove = indexDestination - indexSource;
            int whiteMove = indexSource - indexDestination;

            if (indexSource > 23 || indexSource < 0 || indexDestination > 23 || indexDestination < 0)
            {
                return false;
            }

            if (BlackPlayer.IstMyTurn)
            {
                if ((blackMove == GameDice.FirstCube || blackMove == GameDice.SecondCube) &&
                    BlackPlayer.IsLegalMoveDestination(GameBoard, indexSource, indexDestination) &&
                    BlackPlayer.IsLegalMoveSource(GameBoard, indexSource))
                {
                    return true;
                }

                return false;
            }

            if ((whiteMove == GameDice.FirstCube || whiteMove == GameDice.SecondCube) &&
                WhitePlayer.IsLegalMoveDestination(GameBoard, indexSource, indexDestination) &&
                WhitePlayer.IsLegalMoveSource(GameBoard, indexSource))
            {
                return true;
            }

            return false;

        }
        public bool IsLegalRegularMoveFromBar(int indexDestination)
        {
            int blackMove = indexDestination + 1;
            int whiteMove = 24 - indexDestination;

            if (!(indexDestination >= 0 && indexDestination <= 5) && !(indexDestination >= 18 && indexDestination <= 23))
            {
                return false;
            }

            if (BlackPlayer.IstMyTurn)
            {
                if ((blackMove == GameDice.FirstCube || blackMove == GameDice.SecondCube) &&
                    BlackPlayer.IsLegalMoveDestination(GameBoard, 0, indexDestination))
                {
                    return true;
                }

                return false;
            }

            if ((whiteMove == GameDice.FirstCube || whiteMove == GameDice.SecondCube) &&
                WhitePlayer.IsLegalMoveDestination(GameBoard, 0, indexDestination))
            {
                return true;
            }

            return false;

        }
        public bool IsLegalEatMove(int indexSource, int indexDestination)
        {
            int blackMove = indexDestination - indexSource;
            int whiteMove = indexSource - indexDestination;

            if (indexSource > 23 || indexSource < 0 || indexDestination > 23 || indexDestination < 0)
            {
                return false;
            }

            if (BlackPlayer.IstMyTurn)
            {
                if ((blackMove == GameDice.FirstCube || blackMove == GameDice.SecondCube) &&
                    BlackPlayer.IsLegalMoveForEat(GameBoard, indexSource, indexDestination) &&
                    BlackPlayer.IsLegalMoveSource(GameBoard, indexSource))
                {
                    return true;
                }

                return false;
            }

            if ((whiteMove == GameDice.FirstCube || whiteMove == GameDice.SecondCube) &&
                WhitePlayer.IsLegalMoveForEat(GameBoard, indexSource, indexDestination) &&
                WhitePlayer.IsLegalMoveSource(GameBoard, indexSource))
            {
                return true;
            }

            return false;
        }
        public bool IsLegalEatMoveFromBar(int indexDestination)
        {
            int blackMove = indexDestination + 1;
            int whiteMove = 24 - indexDestination;

            if (!(indexDestination >= 0 && indexDestination <= 5) && !(indexDestination >= 18 && indexDestination <= 23))
            {
                return false;
            }

            if (BlackPlayer.IstMyTurn)
            {
                if ((blackMove == GameDice.FirstCube || blackMove == GameDice.SecondCube) &&
                    BlackPlayer.IsLegalMoveForEat(GameBoard, 0, indexDestination))
                {
                    return true;
                }

                return false;
            }

            if ((whiteMove == GameDice.FirstCube || whiteMove == GameDice.SecondCube) &&
                WhitePlayer.IsLegalMoveForEat(GameBoard, 0, indexDestination))
            {
                return true;
            }

            return false;
        }
        public bool IsLegalTakeOutCheckerMove(int indexSource, int indexDestination)
        {
            int blackMove = indexDestination - indexSource;
            int whiteMove =  indexSource  + 1;

            if (indexSource > 23 || indexSource < 0 || indexDestination > 25 || indexDestination < 0)
            {
                return false;
            }



            if (BlackPlayer.IstMyTurn)
            {
                if ((blackMove <= GameDice.FirstCube || blackMove <= GameDice.SecondCube) &&
                    BlackPlayer.IsLegalMoveSource(GameBoard, indexSource) && indexDestination == 24)
                {
                    
                    return true;
                }

                return false;
            }

            if ((whiteMove <= GameDice.FirstCube || whiteMove <= GameDice.SecondCube) &&
                WhitePlayer.IsLegalMoveSource(GameBoard, indexSource) && indexDestination == 25)
            {
                return true;
            }

            return false;
        }


        public void SetLegalMove(int indexSource, int indexDestination)
        {
            if (BlackPlayer.IstMyTurn)
            {

                if (IsLegalEatMove(indexSource, indexDestination))
                {
                    GameBoard.RemoveCheckerFromTriangle(indexDestination, CheckerColor.WHITE);
                    GameBoard.GameBar.AddWhiteCheckerToBar();

                    GameBoard.RemoveCheckerFromTriangle(indexSource, CheckerColor.BLACK);
                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.BLACK;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.BLACK);

                }
                else
                {
                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.BLACK;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.BLACK);

                    GameBoard.RemoveCheckerFromTriangle(indexSource, CheckerColor.BLACK);

                }
            }
            else
            {
                if (IsLegalEatMove(indexSource, indexDestination))
                {
                    GameBoard.RemoveCheckerFromTriangle(indexDestination, CheckerColor.BLACK);
                    GameBoard.GameBar.AddBlackCheckerToBar();

                    GameBoard.RemoveCheckerFromTriangle(indexSource, CheckerColor.WHITE);
                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.WHITE;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.WHITE);
                }
                else
                {

                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.WHITE;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.WHITE);

                    GameBoard.RemoveCheckerFromTriangle(indexSource, CheckerColor.WHITE);
                }
            }

            MoveLeft--;
            ResetCubes(Math.Abs(indexDestination - indexSource));
        }
        public void SetLegalMoveFromBar(int indexDestination)
        {
            if (BlackPlayer.IstMyTurn)
            {

                if (IsLegalEatMoveFromBar(indexDestination))
                {
                    GameBoard.RemoveCheckerFromTriangle(indexDestination, CheckerColor.WHITE);
                    GameBoard.GameBar.AddWhiteCheckerToBar();

                    GameBoard.GameBar.RemoveBlackCheckerFromBar();
                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.BLACK;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.BLACK);

                }
                else
                {
                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.BLACK;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.BLACK);

                    GameBoard.GameBar.RemoveBlackCheckerFromBar();

                }
            }
            else //White Turn
            {
                if (IsLegalEatMoveFromBar(indexDestination))
                {
                    GameBoard.RemoveCheckerFromTriangle(indexDestination, CheckerColor.BLACK);
                    GameBoard.GameBar.AddBlackCheckerToBar();

                    GameBoard.GameBar.RemoveWhiteCheckerFromBar();
                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.WHITE;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.WHITE);
                }
                else
                {

                    GameBoard.Triangles[indexDestination].CheckerColor = CheckerColor.WHITE;
                    GameBoard.AddCheckerToTriangle(indexDestination, CheckerColor.WHITE);

                    GameBoard.GameBar.RemoveWhiteCheckerFromBar();
                }
            }

            MoveLeft--;
            ResetCubes(Math.Abs(24 - indexDestination));
            ResetCubes(Math.Abs(indexDestination + 1));
        }
        public void SetLegalTakeOutCheckerMove(int indexSource)
        {
            if (BlackPlayer.IstMyTurn)
            {
                GameBoard.RemoveCheckerFromTriangle(indexSource, CheckerColor.BLACK);
                BlackPlayer.NumberOfCheckerOutSide++;
                ResetCubeAfterTakingOutChecker((24 - indexSource));
            }
            else
            {
                GameBoard.RemoveCheckerFromTriangle(indexSource, CheckerColor.WHITE);
                WhitePlayer.NumberOfCheckerOutSide++;
                ResetCubeAfterTakingOutChecker(indexSource + 1);

            }
         
            MoveLeft--;
            CheckIsGameOver();
        }

        public void CheckIsGameOver()
        {
            if (BlackPlayer.NumberOfCheckerOutSide == 15 || WhitePlayer.NumberOfCheckerOutSide == 15)
            {
                IsGameOver = true;
                MoveLeft = 0;
            }
        }

        private void ResetCubeAfterTakingOutChecker(int moveLenght)
        {
            if (GameDice.FirstCube == moveLenght)
            {
                ResetCubes(GameDice.FirstCube);
            }
            else if (GameDice.SecondCube == moveLenght)
            {
                ResetCubes(GameDice.SecondCube);
            }
            else if (GameDice.FirstCube > moveLenght && GameDice.SecondCube < moveLenght)
            {
                ResetCubes(GameDice.FirstCube);
            }
            else if (GameDice.FirstCube < moveLenght && GameDice.SecondCube > moveLenght)
            {
                ResetCubes(GameDice.SecondCube);
            }
            else if (GameDice.FirstCube > moveLenght && GameDice.SecondCube > moveLenght)
            {
                ResetCubes(Math.Min(GameDice.FirstCube, GameDice.SecondCube));
            }
        }
    }
}
