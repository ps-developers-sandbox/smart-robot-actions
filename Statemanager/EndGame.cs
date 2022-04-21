using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoboMines
{
    class EndGame:IState
    {
        Board board;
        bool CalcComplete = false;
        public EndGame(Board board)
        {
            this.board = board;


        }
        public void Enter()
        {
            //show end boom squence
        }
        public void Execute() 
        {
            if (!CalcComplete)
            {
                int longestX, longestO;
                // find the longest chain
                longestX = LongestChain(Board.cellvalue.X);
                longestO = LongestChain(Board.cellvalue.O);

                //penalty for hitting the mine
                if (board.ActivePlayer.Piece == Board.cellvalue.X)
                { longestX -= 2; }
                else { longestO -= 2; }
                board.Players["X"].Score = longestX;

                board.Players["O"].Score = longestO;


                // get the winner
                if (longestX == longestO)
                {
                    //draw
                    board.Outcome = Board.outcome.draw;
                }

                if (longestX > longestO)
                {
                    // X wins+
                    board.Outcome = Board.outcome.xwin;
                }
                else
                {
                    // O wins
                    board.Outcome = Board.outcome.owin;
                }
                CalcComplete = true;
            }

        }
        public void Exit() 
        {
            //end game
            //show winner/ game over screen. 
            //restart
        }

        private int LongestChain(Board.cellvalue cellvalue)
        {
            int row = 0, col = 0, diag = 0,diagdown = 0, diagup = 0, temprow = 0, tempcol = 0, tempdiagdown = 0, tempdiagdup = 0;
           

            for (int posx = 0; posx < 10; posx++)
            {
                for (int posy = 0; posy < 10; posy++)
                {
                    temprow = PiecesInARow(posx, posy, 1, 0, cellvalue);
                    tempcol = PiecesInARow(posx, posy, 0, 1, cellvalue);
                    tempdiagdown = PiecesInARow(posx, posy, 1, 1, cellvalue);
                    tempdiagdup = PiecesInARow(posx, posy, 1, -1, cellvalue);

                    if (temprow > row)
                        row = temprow;
                    if (tempcol > col)
                        col = tempcol;
                    if (tempdiagdown > diagdown)
                        diagdown = tempdiagdown;
                    if (tempdiagdup > diagup)
                        diagup = tempdiagdup;
                    if (diagup > diagdown) { diag = diagup; } else { diag = diagdown; }
                }
            }
            if (row > col && row > diag)
            { return row; }
            else if (col > row && col > diag)
            { return col; }
            else if(diag > row && diag > col)
            { return diag; }
            else { return 0; }
        }

        private int PiecesInARow(int row, int col, int dx, int dy,  Board.cellvalue cellvalue)
        {
            int count = 0;

            while (true)
            {
               

                if (row > 10 || row < 0 || col > 10 || col < 0 || board.Cells[row,col] != cellvalue)
                {
                    break;
                }
                row += dx;
                col += dy;
                count++;
            }

            return count;
        }

    }



}
