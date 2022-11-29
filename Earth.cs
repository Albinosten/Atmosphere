using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Atmosphere
{



    public class Earth 
    {
        private GraphicsDevice graphicsDevice;
        public Earth (GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }
        public PositionalTexture2D PositionalTexture2D {get;set;}
        public void CreateTexture()
        {
            var texture = new Texture2D(this.graphicsDevice, s_width,s_width);
            texture.SetData(EarthTextureCreator.Create(s_width));

            this.PositionalTexture2D = new PositionalTexture2D(texture);
    }

        public static int s_width => 100*2;
        public static int s_radius = s_width/2;
        // public static float s_speed = 0f;
        public static float s_speed = 0.0005f;
        // public static float s_speed = 0.0012f;
        private float elapsedMilliSeconds = 0;
        private float lastRotationValue = 0;
        public float Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
            elapsedMilliSeconds  +=  gameTime.ElapsedGameTime.Milliseconds;
            var pos = new Vector2(this.PositionalTexture2D.XPosCenter, this.PositionalTexture2D.YPosCenter);
            
            // spriteBatch.Draw(this.PositionalTexture2D.GetTexture()
            // , pos
            // , Color.White
            // );

            var angleOfEarthRotation = elapsedMilliSeconds * s_speed;
            spriteBatch.Draw(this.PositionalTexture2D.GetTexture()
            , pos
            , new Rectangle((int)0,(int)0, (int)this.PositionalTexture2D.Width, (int)this.PositionalTexture2D.Height )
            , Color.White
            , angleOfEarthRotation
            , new Vector2((int)this.PositionalTexture2D.Width/2, (int)this.PositionalTexture2D.Height/2 )
            , Vector2.One
            , SpriteEffects.None
            , 1
            );
            var deltaRotation = angleOfEarthRotation - this.lastRotationValue;
            this.lastRotationValue = angleOfEarthRotation;

            return deltaRotation;
        }



    }

}