using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MazeGeneration
{
    public class Dot
    {

        Texture2D image;
        Vector2 position;

        List<Dot> neighbors;

        public Dot(Texture2D image, Vector2 position)
        {

            this.image = image;
            this.position = position;

        }


        public void Draw(SpriteBatch sb)
        {

            sb.Draw(image, position, Color.White);

        }

    }
}
