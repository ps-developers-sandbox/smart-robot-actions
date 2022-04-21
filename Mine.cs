using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoboMines
{
    class Mine
    {
        public Vector2 Position { get; set; }

        public Mine(Vector2 postition)
        {
            Position = postition;
        }
    }
}
