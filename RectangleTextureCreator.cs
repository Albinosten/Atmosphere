using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
namespace Atmosphere
{
    public static class RectangleTextureCreator
    {
        public static Color[] GetColorsWithBorders(int width, int height, Color color, Color? borderColor = null)
        {
            //Todo add parameter for thickness?
            Color[] data = new Color[width*height];
            for(int i=0; i < data.Length; ++i) 
            {
                var mod = i%width;
                if(mod < 2 || mod > width - 3 || i < width*2 || i > data.Length - (width*2))
                {
                    data[i] = borderColor ?? Color.Black;
                }
                else
                {
                    data[i] = color;
                }

                if(i < width*7 )
                {
                    data[i] = Color.DarkGreen;
                }
            }
            return data;
        }
    }
}