using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonLogic
{
    public class Bar
    {

        public int NumberOfWhiteCheckers { get; private set; }
        public int NumberOfBlackCheckers { get; private set; }

        public Bar()
        {
            
        }

        public void AddWhiteCheckerToBar()
        {
            NumberOfWhiteCheckers++;
        }
        public void RemoveWhiteCheckerToBar()
        {
            NumberOfWhiteCheckers--;
        }

        public void AddBlackCheckerToBar()
        {
            NumberOfBlackCheckers++;
        }
        public void RemoveBlackCheckerToBar()
        {
            NumberOfBlackCheckers--;
        }

        public void RemoveBlackCheckerFromBar()
        {
            NumberOfBlackCheckers--;
        }
        public void RemoveWhiteCheckerFromBar()
        {
            NumberOfWhiteCheckers--;
        }
    }
}
