using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace HoboMines
{
    class Oturn:State
    {
      

        public Oturn(Board board)
        {
            this.board = board;
        }
        public override void Enter()
        { 
            player = board.Players["O"];
           state = new InputHelper();
            board.ActivePlayer = player;           
        }
        

    }
}
