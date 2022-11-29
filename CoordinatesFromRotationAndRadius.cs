using System;
using Microsoft.Xna.Framework;

namespace Atmosphere
{
    public class CoordinatesFromRotationAndRadius 
    {
        public static Vector2 GetPosition(float rotationinRadian, float radius)
        {
            float x = (float)Math.Sin(rotationinRadian) * radius;
            float y = (float)Math.Cos(rotationinRadian) * radius;
            return new Vector2(x,-y);
        }
    }
}