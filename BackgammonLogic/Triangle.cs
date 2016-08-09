using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonLogic
{
    public class Triangle
    {

        public int NumberOfCheckers { get; set; } 
        public CheckerColor CheckerColor { get; set; }

        public Triangle()
        {
            NumberOfCheckers = 0;
            CheckerColor=CheckerColor.NONE;
        }
        public Triangle(byte numberOfCheckers, CheckerColor checkerColor)
        {

            NumberOfCheckers = numberOfCheckers;
            CheckerColor = checkerColor;
            
        }
    }
}
