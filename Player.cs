using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoboMines
{
    public class Player
    {
        public Board.cellvalue Piece { get; private set; }
        public int Score { get; set; }
        public bool HasMoved { get;  set; }
       
        public Player(Board.cellvalue piece)
        {
            Piece = piece;
        }
      
    }
}
