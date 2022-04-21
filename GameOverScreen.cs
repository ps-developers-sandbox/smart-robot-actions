using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace HoboMines
{
    class GameOverScreen: DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        SpriteFont _arial;
        public GameOverScreen(Game game) : base(game)
        {

        }
        public void Load(GraphicsDeviceManager graphics)
        {
            _arial = Game.Content.Load<SpriteFont>("Arial");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

        }
        public void Draw(Board board)
        {
            Board.outcome outcome = board.Outcome;
            spriteBatch.Begin();
         
            switch (outcome)
            {
                case Board.outcome.draw:
                    spriteBatch.DrawString(_arial, "Its a draw", new Vector2(10, 10), Color.Black);
                    break;
                case Board.outcome.xwin:
                   
                   
                    spriteBatch.DrawString(_arial, String.Format("X wins x= {0}  o={1}", board.Players["X"].Score, board.Players["O"].Score), new Vector2(10, 10), Color.Black);
                    break;
                case Board.outcome.owin:
                    spriteBatch.DrawString(_arial, String.Format("O wins x= {0}  o={1}", board.Players["X"].Score, board.Players["O"].Score), new Vector2(10, 10), Color.Black);
                    break;
                default:
                    break;
            }
            spriteBatch.DrawString(_arial, "Game Over!", new Vector2(10, 30), Color.Black);
            spriteBatch.End();
        }
        }
}
