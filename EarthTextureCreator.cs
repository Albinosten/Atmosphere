using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Atmosphere
{
    public static class EarthTextureCreator
    {
        public static Color[] Create(int width)
        {

            Color[] data = new Color[width*width];

            data = AddCircleWithColor(data, width, width/2, Color.Black);
            data = AddCircleWithColor(data, width, (width/2)-2, Color.Purple);
            
            //Add land
            data = AddCircleWithColor(data, width, (width/5), Color.Green, width/6, width/2);
            data = AddCircleWithColor(data, width, (width/4), Color.Green, -width/3);
            data = AddCircleWithColor(data, width, (width/6), Color.DarkSalmon,0,-width/4);

            return data;
        }
        public static Color[] AddCircleWithColor(Color[] data
            , int width
            , int circleRadius
            , Color color
            , int xOffset = 0
            , int yOffset = 0
            )
        {
            var y = 0;

            for(var i = 0; i< width*width;i++)
            {
                var x =  i%width;
                if(x == width-1)
                {
                    y++;
                }
                var valueWithOffset = GetValue(x-xOffset, y-yOffset, width);
                var valueWithoutOffset = GetValue(x,y, width);

                if(valueWithOffset > circleRadius*circleRadius || valueWithoutOffset > ((width/2)*(width/2)))
                {
                    //utanför cirkeln
                 //   data[i] = Color.Transparent;
                }
                else
                {
                    //här är i cirkeln
                    data[i] = color;

                }
                
            }
            return data;
        }
        private static int GetValue(int x,int y, int width)
        {
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            var deltax = Math.Abs(x-(width/2));
            var deltay = Math.Abs(y-(width/2));

            return deltax * deltax + deltay * deltay;
        }
    }
}