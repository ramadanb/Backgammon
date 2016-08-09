using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonLogic
{
    public class Board
    {
        private Triangle[] _triangles;

        public Bar GameBar { get; private set; }
        public Triangle[] Triangles => _triangles;

        public Board()
        {
            _triangles = new Triangle[24];
            GameBar = new Bar();
            InitializeBoard();
          

        }

        private void InitializeBoard1() //for testing of taking out checkers
        {
            //initialize white checkers
            _triangles[0] = new Triangle(1, CheckerColor.WHITE);
            _triangles[1] = new Triangle(2, CheckerColor.WHITE);
            _triangles[2] = new Triangle(3, CheckerColor.WHITE);
            _triangles[3] = new Triangle(3, CheckerColor.WHITE);
            _triangles[4] = new Triangle(3, CheckerColor.WHITE);
            _triangles[5] = new Triangle(3, CheckerColor.WHITE);

            //initialize black checkers
            _triangles[23] = new Triangle(1, CheckerColor.BLACK);
            _triangles[22] = new Triangle(2, CheckerColor.BLACK);
            _triangles[21] = new Triangle(3, CheckerColor.BLACK);
            _triangles[20] = new Triangle(3, CheckerColor.BLACK);
            _triangles[19] = new Triangle(3, CheckerColor.BLACK);
            _triangles[18] = new Triangle(3, CheckerColor.BLACK);

            for (int i = 6; i <= 17; i++)
            {

                    _triangles[i] = new Triangle();
                
            }
        }

        private void InitializeBoard()
        {
            //initialize white checkers
            _triangles[5] = new Triangle(5, CheckerColor.WHITE);
            _triangles[7] = new Triangle(3, CheckerColor.WHITE);
            _triangles[12] = new Triangle(5, CheckerColor.WHITE);
            _triangles[23] = new Triangle(2, CheckerColor.WHITE);

            //initialize black checkers
            _triangles[0] = new Triangle(2, CheckerColor.BLACK);
            _triangles[11] = new Triangle(5, CheckerColor.BLACK);
            _triangles[16] = new Triangle(3, CheckerColor.BLACK);
            _triangles[18] = new Triangle(5, CheckerColor.BLACK);

            for (int i = 0; i < 24; i++)
            {
                if (i != 0 && i != 5 && i != 7 && i != 11 && i != 12 && i != 16 && i != 18 && i != 23)
                {
                    _triangles[i] = new Triangle();
                }
            }

        }

        public void AddCheckerToTriangle(int triangleNumber, CheckerColor checkerColor)
        {
            if (_triangles[triangleNumber].CheckerColor == checkerColor ||
                _triangles[triangleNumber].NumberOfCheckers == 0)
            {
                _triangles[triangleNumber].NumberOfCheckers++;
            }
        }
        public void RemoveCheckerFromTriangle(int triangleNumber, CheckerColor checkerColor)
        {
            if (_triangles[triangleNumber].CheckerColor == checkerColor &&
                _triangles[triangleNumber].NumberOfCheckers != 0)
            {
                _triangles[triangleNumber].NumberOfCheckers--;
            }
            if (_triangles[triangleNumber].NumberOfCheckers == 0)
            {
                _triangles[triangleNumber].CheckerColor = CheckerColor.NONE;
            }
        }
    }
}
