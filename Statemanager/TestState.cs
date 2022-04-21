using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HoboMines
{
     class TestState:IState    {
        Board board;
     

        public TestState(Board board) { this.board = board; }
         public void Enter()
        {
            
            Debug.Print("entering test state");
        }

         public void Execute()
        {
            Debug.Print("updating test state");
        }

         public void Exit()
        {
            Debug.Print("exiting test state");
        }
    }
}
