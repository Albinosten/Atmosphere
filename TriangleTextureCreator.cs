using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
namespace Atmosphere
{
    public static class TriangleTextureCreator
    {
        public static Color[] Create(int height, int width, Color color)
        {
            float tanV = (float)width/(float)height;

            var y = 0;
            Color[] data = new Color[height*width];
            for(int i = 0; i < height*width; i++)
            {
                var x =  i%width;
                if(x == width-1)
                {
                    y++;
                }
                if(GetTanXY(x,y) < tanV)
                {
                    data[i] = color;
                }
                else 
                {
                    //data[i] = Color.Black;
                }
            }
            return data;
        }
        private static float GetTanXY(int x, int y)
        {
            //x = Math.Max(1,x);
            if (y == 0)
            {
                y =1;
            }
            
            return (float)x/(float)y;
        }
    }
}