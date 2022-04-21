using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;



namespace HoboMines
{
    class Xturn:State
    {
        

        public Xturn(Board board)
        {
            this.board = board;
         

        }
        public override void Enter()
        {
            player = board.Players["X"];
            state = new InputHelper();
            board.ActivePlayer = player;
        }

      
      
    }

    }

