using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HoboMines
{
   abstract class State: IState
    {
        public Board board;
        public Player player;
        public InputHelper state;
        public virtual void Enter() { }
        public virtual void Execute()
        {
            board.drawmine = false;
                board.drawpeek = false;
                //MouseState state = Mouse.GetState();
                state.Update();

                int posx = (int)(state.MousePosition.X - board.Postition.X) / 64;
                int posy = (int)(state.MousePosition.Y - board.Postition.Y) / 64;
                posx = MathHelper.Clamp(posx, 0, 9);
                posy = MathHelper.Clamp(posy, 0, 9);
                Board.cellvalue cellvalue = board.Cells[posx, posy];

                if (state.IsNewPress(MouseButtons.LeftButton))
                {
                    if (cellvalue == Board.cellvalue.empty && !player.HasMoved)
                    {
                        board.Cells[posx, posy] = player.Piece;
                        player.HasMoved = true;
                    }
                    if (cellvalue == Board.cellvalue.mine && !player.HasMoved)
                    {
                        board.Mine(posx, posy);
                        board.explodemine = true;
                    }
                }


                if (state.IsCurPress(MouseButtons.RightButton))
                {
                    if (cellvalue == Board.cellvalue.mine && !player.HasMoved)
                    {
                        //bombpeek
                        //show bomb png in cell
                        board.Mine(posx, posy);
                        board.explodemine = true;


                    }
                    if (cellvalue == Board.cellvalue.empty && !player.HasMoved)
                    {
                        board.Peek(posx, posy);

                    }
                }
                if (state.IsOldPress(MouseButtons.RightButton))
                {
                    player.HasMoved = true;
                }

            }

        public virtual void Exit()
        {
            player.HasMoved = false;
        }
    }
}
