using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HoboMines
{
   public class Board: DrawableGameComponent
    {
        public enum cellvalue {
        empty = 0,
        mine =1,
        X = 2,
        O=4
        }
        public enum outcome { draw,xwin,owin}
        public cellvalue[,] Cells { get; private set; }
        public Texture2D Texture { get; private set; }
        private Texture2D mineTexture;
        private Texture2D XTexture;
        private Texture2D OTexture;
        private Texture2D peekTexture;
        public Vector2 Postition { get; private set; }
       // private Vector2 minePos;
        private Vector2 peekPos;
        //public Player[] Players { get; set; }
        public Dictionary<string, Player> Players { get; set; }
        public Player ActivePlayer { get; set; }
        public outcome Outcome { get; set; }
        
        private SpriteBatch spriteBatch;
        public bool drawmine { get; set; }
        public bool drawpeek { get; set; }
        public bool explodemine { get; set; }
        private List<Mine> mines { get; set; }
       
      

        public Board(Game game) :base(game)
        {
            Cells = new cellvalue[11, 11];
            mines = new List<Mine>();
         
            Random rnd = new Random();
            int x, y,count;
            // randomize how many mines on the board (between 4 and 10)
            count = rnd.Next(4,10);
            for (int i = 0; i < count; i++)
            {
               
                x = rnd.Next(0, 10);
                y = rnd.Next(0, 10);
                // spreadout the mines a bit. no adjecent mines allowed
              while(GetColor(x,y) == Color.Red && Cells[x, y] != cellvalue.mine)
                {
                    x = rnd.Next(0, 10);
                    y = rnd.Next(0, 10);
                }
                Cells[x, y] = cellvalue.mine;
               

            }

          
        }

        public void Load(GraphicsDeviceManager graphics) 
        {

            mineTexture = Game.Content.Load<Texture2D>("mine");
            XTexture = Game.Content.Load<Texture2D>("X");
            OTexture = Game.Content.Load<Texture2D>("O");
            peekTexture = Game.Content.Load<Texture2D>("peek");
            Texture = Game.Content.Load<Texture2D>("board");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            Postition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - Texture.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2 - Texture.Height / 2);

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (Cells[x, y] == cellvalue.mine)
                    {
                        mines.Add(new Mine(new Vector2(Postition.X + (x * 64), Postition.Y + (y * 64))));
                    }
                }
            }




        }
        public void Mine(int posx, int posy)
        {
            drawmine = true;
            //minePos.X = Postition.X +( posx*64);
            //minePos.Y = Postition.Y + (posy * 64);
        }
        public void Peek(int posx, int posy) 
        {
            drawpeek = true;
            peekPos.X = Postition.X + (posx * 64);
            peekPos.Y = Postition.Y + (posy * 64);
        }
        private Color GetColor(int x, int y)
        {
            bool foundmine = false;

            //TODO  messy code. simplify or refactor to use an array of structs with the search pattern then do a foreach loop.  
            // check if mine is in the 1 square around the peeked position
            if (y > 0 && x <= 9)
            {
                if (Cells[x, y - 1] == cellvalue.mine) { foundmine = true; }
                if (Cells[x + 1, y - 1] == cellvalue.mine) { foundmine = true; }
            }

            if (x > 0 && y <= 9)
            {
                if (Cells[x - 1, y] == cellvalue.mine) { foundmine = true; }
                if (Cells[x - 1, y + 1] == cellvalue.mine) { foundmine = true; }
            }

            if (x > 0 && y > 0)
            {
                if (Cells[x - 1, y - 1] == cellvalue.mine) { foundmine = true; }
            }
            if (x <= 9 && y <= 9)
            {
                if (Cells[x, y + 1] == cellvalue.mine) { foundmine = true; }
                if (Cells[x + 1, y] == cellvalue.mine) { foundmine = true; }
                if (Cells[x + 1, y + 1] == cellvalue.mine) { foundmine = true; }
            }
            if (foundmine) { return Color.Red; }
            else
            {// check if mine is in the 2 square around the peeked position

                if (y > 1 && x <= 8)
                {
                    if (Cells[x, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 2, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 1, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 2, y - 1] == cellvalue.mine) { foundmine = true; }
                }

                if (x > 1 && y <= 8)
                {
                    if (Cells[x - 2, y] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y + 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y + 1] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 1, y + 2] == cellvalue.mine) { foundmine = true; }
                }

                if (x > 1 && y > 1)
                {
                    if (Cells[x - 1, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y - 1] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y - 2] == cellvalue.mine) { foundmine = true; }
                }

                if (x <= 9 && y <= 8)
                {
                    if (Cells[x, y + 2] == cellvalue.mine) { foundmine = true; }

                }
                if (x <= 8 && y <= 9)
                {
                    if (Cells[x + 2, y] == cellvalue.mine) { foundmine = true; }
                }

                if (x <= 8 && y <= 8)
                {
                    if (Cells[x + 2, y + 1] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 1, y + 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 2, y + 2] == cellvalue.mine) { foundmine = true; }
                }
                if (foundmine) { return Color.Yellow; }
                else { return Color.Green; }

            }
        }
        private Color GetColor(Vector2 pos)
        {
            // get the the cell position user clicked 
            int x = (int)((pos.X - Postition.X) / 64);
            int y = (int)((pos.Y - Postition.Y) / 64);
            bool foundmine = false;

            //TODO  messy code. simplify or refactor to use an array of structs with the search pattern then do a foreach loop.  
            // check if mine is in the 1 square around the peeked position
            if (y > 0 && x <= 9)
            {
                if (Cells[x, y - 1] == cellvalue.mine) { foundmine = true; }
                if (Cells[x + 1, y - 1] == cellvalue.mine) { foundmine = true; }
            }

            if (x > 0 && y <= 9)
            {
                if (Cells[x - 1, y] == cellvalue.mine) { foundmine = true; }
                if (Cells[x - 1, y + 1] == cellvalue.mine) { foundmine = true; }
            }

            if (x > 0 && y > 0)
            {
                if (Cells[x - 1, y - 1] == cellvalue.mine) { foundmine = true; }
            }
            if (x <= 9 && y <= 9)
            {
                if (Cells[x, y + 1] == cellvalue.mine) { foundmine = true; }
                if (Cells[x + 1, y] == cellvalue.mine) { foundmine = true; }
                if (Cells[x + 1, y + 1] == cellvalue.mine) { foundmine = true; }
            }
            if (foundmine) { return Color.Red; }
            else
            {// check if mine is in the 2 square around the peeked position

                if (y > 1 && x <= 8)
                {
                    if (Cells[x, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 2, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 1, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 2, y - 1] == cellvalue.mine) { foundmine = true; }
                }

                if (x > 1 && y <= 8)
                {
                    if (Cells[x - 2, y] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y + 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y + 1] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 1, y + 2] == cellvalue.mine) { foundmine = true; }
                }

                if (x > 1 && y > 1)
                {
                    if (Cells[x - 1, y - 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y - 1] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x - 2, y - 2] == cellvalue.mine) { foundmine = true; }
                }

                if (x <=9 && y <=8)
                {
                    if (Cells[x, y + 2] == cellvalue.mine) { foundmine = true; }

                }
                if (x <= 8 && y <= 9)
                {
                    if (Cells[x + 2, y] == cellvalue.mine) { foundmine = true; }
                }

                if (x <= 8 && y <= 8)
                {
                    if (Cells[x + 2, y + 1] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 1, y + 2] == cellvalue.mine) { foundmine = true; }
                    if (Cells[x + 2, y + 2] == cellvalue.mine) { foundmine = true; }
                }
                if (foundmine) { return Color.Yellow; }
                else { return Color.Green; }

            }
        }
        public void Draw ()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture,Postition,Color.White);
   

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                   
                    //Primitives2D.DrawRectangle(spriteBatch, new Rectangle(new Point((int)Postition.X + (x * 64), (int)Postition.Y + (y * 64)), new Point(64, 64)), Color.Black);
                    cellvalue cell = Cells[x, y];
                    switch (cell)
                    {
                        case cellvalue.empty:
                            break;
                        case cellvalue.mine:
                            
                            break;
                        case cellvalue.X:
                            spriteBatch.Draw(XTexture, new Vector2(Postition.X + (x*64) ,Postition.Y + (y*64)), Color.White);
                            break;
                        case cellvalue.O:
                            spriteBatch.Draw(OTexture, new Vector2(Postition.X + (x * 64), Postition.Y + (y * 64)), Color.White);

                            break;
                        default:
                            break;
                    }
                }

            }
            if (drawmine)
            {
                foreach (Mine mine in mines)
                {
                    spriteBatch.Draw(mineTexture, new Vector2(((mine.Position.X) / 64) * 64, ((mine.Position.Y) / 64) * 64), Color.White);
                }
            }

            if (drawpeek)
                spriteBatch.Draw(peekTexture, new Vector2(((peekPos.X/ 64) * 64)+1, (peekPos.Y / 64) * 64), GetColor(peekPos));

            spriteBatch.End();
           
        }


    }
}
