using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonLogic
{
    public interface IGameManager
    {
         Player     BlackPlayer     { get;}
         Player     WhitePlayer     { get;}
         Board      GameBoard       { get;}
         Dice       GameDice        { get;}
         bool       IsGameOver      { get;}
         int        MoveLeft        { get;}

        int GetNumberOfCheckersAtHome();
        int GetNumberOfCheckersAtBar();
        int GetNumberOfBlackCheckersAtBar();
        int GetNumberOfWhiteCheckersAtBar();
        int GetNumberOfBlackCheckerOutSide();
        int GetNumberOfWhiteCheckerOutSide();

        void GetDiceRolls();
        CheckerColor GetTheWinner();

        bool PlayerHasAvailbleMoves();
        bool PlayerHasAvailbleMovesToTakeOutCheckers();
        bool PlayerHasAvailbleMovesToEat();
        bool PlayerHasAvailbleMovesToEatFromBar();
        bool PlayerCanTakeOutCheckers();

        void UpdateMoveLeft();
        void SwapTurns();

        bool IsLegalRegularMove(int indexSource, int indexDestination);
        bool IsLegalRegularMoveFromBar(int indexDestination);
        bool IsLegalEatMove(int indexSource, int indexDestination);
        bool IsLegalEatMoveFromBar(int indexDestination);
        bool IsLegalTakeOutCheckerMove(int indexSource, int indexDestination);

        void SetLegalMove(int indexSource, int indexDestination);
        void SetLegalMoveFromBar(int indexDestination);
        void SetLegalTakeOutCheckerMove(int indexSource);




    }
}
