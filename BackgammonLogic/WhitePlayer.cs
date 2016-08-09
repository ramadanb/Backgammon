using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonLogic
{
    public class WhitePlayer:Player
    {

        public WhitePlayer(string name, CheckerColor playerColor) : base(name, playerColor)
        {
        }

        /// <summary>
        ///  all availble moves of the white checker 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="dice"></param>
        /// <returns>list of keyValuePair when the key is the index of the source traingle 
        /// and the value is the index of the destination triangle
        ///  </returns>
        /// 
        public override IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMoves(Board board, Dice dice)
        {

            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            int i;
            List<KeyValuePair<int, int>> moveList = new List<KeyValuePair<int, int>>();


            if (board.GameBar.NumberOfWhiteCheckers == 0) //no checkers that needs to return first
            {
                for (i = 0; i < board.Triangles.Length; i++)
                {
                    if (!dice.RolledDouble) //there is no double rolled
                    {
                        if (dice.FirstCube != -1 && IsLegalMoveSource(board, 23-i) && IsLegalMoveDestination(board, 23-i, 23-i-dice.FirstCube))
                        {
                            moveList.Add(new KeyValuePair<int, int>(23-i, 23-i-dice.FirstCube));
                        }
                    }

                    if (dice.SecondCube != -1 && IsLegalMoveSource(board, 23-i) && IsLegalMoveDestination(board, 23-i, 23 - i - dice.FirstCube))
                    {
                        moveList.Add(new KeyValuePair<int, int>(23-i, 23-i-dice.SecondCube));
                    }

                }
            }
            else
            {
                moveList = (List<KeyValuePair<int, int>>)GetAllAvailbleMovesFromBar(board, dice);

            }

            return moveList;
        }

        public override IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMovesFromBar(Board board, Dice dice)
        {

            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            List<KeyValuePair<int, int>> moveList = new List<KeyValuePair<int, int>>();

            if (!dice.RolledDouble)
            {
                if (dice.FirstCube != -1 && IsLegalMoveDestination(board, 23, 23-dice.FirstCube+1))
                {
                    moveList.Add(new KeyValuePair<int, int>(1, 23-dice.FirstCube + 1));
                }

            }

            if (dice.SecondCube != -1 && IsLegalMoveDestination(board,23, 23 - dice.SecondCube + 1))
            {

                moveList.Add(new KeyValuePair<int, int>(2, 23-dice.SecondCube + 1));
            }



            return moveList;
        }

        public override IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMovesEat(Board board, Dice dice)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            if (board.GameBar.NumberOfBlackCheckers == 0)
            {
                List<KeyValuePair<int, int>> moveList = new List<KeyValuePair<int, int>>();

                for (int i = 0; i < board.Triangles.Length; i++)
                {
                    if (!dice.RolledDouble)
                    {
                        if (dice.FirstCube != -1 && IsLegalMoveSource(board, 23-i) && IsLegalMoveForEat(board, 23-i,  23-i-dice.FirstCube))
                        {
                            moveList.Add(new KeyValuePair<int, int>(23-i, 23-i- dice.FirstCube));

                        }
                    }

                    if (dice.SecondCube != -1 && IsLegalMoveSource(board, 23-i) && IsLegalMoveForEat(board, 23-i, 23-i-dice.SecondCube))
                    {
                        moveList.Add(new KeyValuePair<int, int>(23-i, 23-i - dice.SecondCube));

                    }
                }
                return moveList;
            }
            else
            {
                return GetAllAvailbleMovesEatFromBar(board, dice);
            }


        }

        public override IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMovesEatFromBar(Board board, Dice dice)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            List<KeyValuePair<int, int>> moveList = new List<KeyValuePair<int, int>>();

            if (!dice.RolledDouble)
            {
                if (dice.FirstCube != -1 && IsLegalMoveForEat(board, 23,23- dice.FirstCube +1))
                {
                    moveList.Add(new KeyValuePair<int, int>(1, dice.FirstCube - 1));
                }
            }

            if (dice.SecondCube != -1 && IsLegalMoveForEat(board, 23, 23-dice.SecondCube + 1))
            {
                moveList.Add(new KeyValuePair<int, int>(2, dice.SecondCube - 1));
            }
            return moveList;
        }

        public override IEnumerable<KeyValuePair<int, int>> GetAllAvailbleTakeOutMoves(Board board, Dice dice)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            List<KeyValuePair<int, int>> moveList = new List<KeyValuePair<int, int>>();

            for (int i = 0; i <= 5; i++)
            {

                if (!dice.RolledDouble)
                {
                    if (dice.FirstCube != -1 && IsLegalMoveSource(board, i) && IsLegalMoveToTakeOut(i, dice.FirstCube))
                    {
                        moveList.Add(new KeyValuePair<int, int>(i, dice.FirstCube));
                    }
                }

                if (dice.SecondCube != -1 && IsLegalMoveSource(board, i) && IsLegalMoveToTakeOut(i, dice.SecondCube))
                {
                    moveList.Add(new KeyValuePair<int, int>(i, dice.SecondCube));
                }
            }

            return moveList;
        }

        public override bool IsLegalMoveForEat(Board board, int fromIndex, int toIndex)
        {
            if (toIndex > 23 || toIndex < 0 || fromIndex > 23 || fromIndex < 0)
            {
                return false;
            }
            if ( (board.Triangles[toIndex].CheckerColor == CheckerColor.BLACK &&
                    board.Triangles[toIndex].NumberOfCheckers == 1))
            {
                return true;
            }

            return false;
        }
        public override bool IsLegalMoveSource(Board board, int fromIndex)
        {
            return (board.Triangles[fromIndex].CheckerColor == CheckerColor.WHITE &&
                board.Triangles[fromIndex].NumberOfCheckers != 0);
        }
        public override bool IsLegalMoveDestination(Board board, int fromIndex, int toIndex)
        {
            if (toIndex > 23 || toIndex < 0 || fromIndex > 23 || fromIndex < 0)
            {
                return false;
            }
            if (( board.Triangles[toIndex].CheckerColor == CheckerColor.WHITE ||
                board.Triangles[toIndex].NumberOfCheckers == 0))
            {
                return true;
            }

            return false;
        }
        public override bool IsLegalMoveToTakeOut(int fromIndex, int rolledNumber)
        {
            if (fromIndex - rolledNumber >= 0)
                return false;

            return true;
        }

        public override bool CanTakeOutCheckers(Board board)
        {
            int numberOfCheckerOutSideHome = board.GameBar.NumberOfWhiteCheckers; 

            for (int i = 6; i <= 23; i++)
            {
                if (IsLegalMoveSource(board, i))
                {
                    numberOfCheckerOutSideHome += board.Triangles[i].NumberOfCheckers;
                }
            }

            if (numberOfCheckerOutSideHome > 0)
                return false;

            return true;
        }
        public override void UpdateCheckersAtHome(Board board)
        {
            CheckersAtHome = 0;

            for (int i = 18; i <= 23; i++)
            {
                if (board.Triangles[i].CheckerColor == CheckerColor.WHITE)
                {
                    CheckersAtHome += board.Triangles[i].NumberOfCheckers;
                }
            }
        }
    }
}

