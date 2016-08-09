using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonLogic
{
    public abstract class Player
    {
        public string Name { get; private set; }
        public CheckerColor PlayerColor { get; private set; }
        public bool IstMyTurn { get; set; } = false;
        public int CheckersAtHome { get; set; }
        public int Pip { get; set; } = 167;
        public int NumberOfCheckerOutSide { get; set; }

        protected Player(string name, CheckerColor playerColor)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            PlayerColor = playerColor;

        }

        public bool HasAvailbleMoves(Board board, Dice dice)
        {

            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            return GetAllAvailbleMoves(board, dice).ToList().Count > 0 ? true : false;
        }
        public bool HasAvailbleMovesToTakeOut(Board board, Dice dice)
        {

            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            return GetAllAvailbleTakeOutMoves(board, dice).ToList().Count > 0 ? true : false;
        }
        public bool HasAvailbleMovesToEat(Board board, Dice dice)
        {

            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            return GetAllAvailbleMovesEat(board, dice).ToList().Count > 0 ? true : false;
        }
        public bool HasAvailbleMovesToEatFromBar(Board board, Dice dice)
        {

            if (board == null)
                throw new ArgumentNullException(nameof(board));

            if (dice == null)
                throw new ArgumentNullException(nameof(dice));

            return GetAllAvailbleMovesEatFromBar(board, dice).ToList().Count > 0 ? true : false;
        }

        public abstract IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMoves(Board board, Dice dice);
        public abstract IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMovesFromBar(Board board, Dice dice);
        public abstract IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMovesEat(Board board, Dice dice);
        public abstract IEnumerable<KeyValuePair<int, int>> GetAllAvailbleMovesEatFromBar(Board board, Dice dice);
        public abstract IEnumerable<KeyValuePair<int, int>> GetAllAvailbleTakeOutMoves(Board board, Dice dice);


        public abstract bool IsLegalMoveForEat(Board board, int fromIndex, int toIndex);
        public abstract bool IsLegalMoveSource(Board board, int fromIndex);
        public abstract bool IsLegalMoveDestination(Board board, int fromIndex, int toIndex);
        public abstract bool IsLegalMoveToTakeOut(int fromIndex, int rolledNumber);

        public abstract bool CanTakeOutCheckers(Board board);
        public abstract void UpdateCheckersAtHome(Board board);
     
    }
}
