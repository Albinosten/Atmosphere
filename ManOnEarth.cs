using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Atmosphere
{
    public class ManOnEarth
    {
        public static int s_width => 44;
        public static int s_height => 80;
        private readonly GraphicsDevice graphicsDevice;
        public PositionalTexture2D PositionalTexture2D {get;set;}
        private Vector2 origo;
        public ManOnEarth(GraphicsDevice graphicsDevice, Vector2 origo)
        {
            this.TextureScaling = Vector2.One;
            this.graphicsDevice = graphicsDevice;
            this.origo = origo;
            this.timeSinceJumped = 10;
        }

        public void InitializePosition()
        {
            this.jumpingStatingPosition = this.origo.Y;
            this.PositionalTexture2D.XPos = this.origo.X;
            this.PositionalTexture2D.YPos = this.origo.Y;
            
        }
        public void CreateTexture()
        {
            var texture = new Texture2D(this.graphicsDevice, s_width,s_height);
            //ändra till en raket typ

            /*

             ^
            / \
            | |
            ---
           /   \

            */
            texture.SetData(RectangleTextureCreator.GetColorsWithBorders(s_height, s_width, Color.Chocolate));

            this.PositionalTexture2D = new PositionalTexture2D(texture);
        }


        private float timeSinceJumped;
        private float jumpingStatingPosition;
        private const float jumpingLockTime = 1.2f;

        private Vector2 TextureScaling;
        private float loadingTime=0;
        private const float  maxLoadingTime = 3;
        public void LoadingJump(GameTime gameTime)
        {
            this.loadingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            var loadingTimePercent = Math.Min(this.loadingTime,maxLoadingTime) / maxLoadingTime;

            this.jumpingPower = loadingTimePercent*maxJumpHeight;
            
            var x = 1 -(loadingTimePercent/3);
            var y = 1 -(loadingTimePercent/2);
            this.TextureScaling = new Vector2(x,y);
        }
        public void StartJump(GameTime gameTime)
        {
            this.TextureScaling = Vector2.One;
            timeSinceJumped += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(this.timeSinceJumped > jumpingLockTime)
            {
                this.jumpingStatingPosition = this.PositionalTexture2D.YPos;
                this.timeSinceJumped = 0;
                // this.jumpingPower=0;
                this.loadingTime = 0;
            }
        }
        private bool keyDownPreviousValue=false;
        public void InitializeJump(GameTime gameTime, bool keyDown)
        {
            if(keyDownPreviousValue && keyDown == false)//time to jump
            {
                this.StartJump(gameTime);
            }
            this.keyDownPreviousValue = keyDown;
            if(keyDown)
            {
                this.LoadingJump(gameTime);
            }

        }

        private const float maxJumpTimeForFunction = 2;
  
        public float totalJumpTime = 6;//används till att sätta hur långt hela hopp animationen ska ta.
        private float jumpingPower;
        private const float maxJumpHeight = 80*2;
        private float jumpingOffset;

        private float GetJumpingOffset(GameTime gameTime) //returns a value from 0 -> maxJumpingHeigh -> 0
        {
            this.timeSinceJumped += (float)gameTime.ElapsedGameTime.TotalSeconds * (1/(totalJumpTime/2));
            
            if(this.timeSinceJumped < maxJumpTimeForFunction)
            {
                //2x - 1x^2
                // t = 0 => y = 0;
                // t = 1 => y = 1;
                // t = 2 => y = 0;
                // t = 3 => y = -3

                var a =  2*this.timeSinceJumped - 1*this.timeSinceJumped*this.timeSinceJumped;   
                a = Math.Max((float)a, (float)-1); 
            
                return a*jumpingPower;
            }
            else
            {
                return 0;
            }
        }
        private float GetPosition(GameTime gameTime)
        {
            this.jumpingOffset = this.GetJumpingOffset(gameTime);
            var newPosition =  this.jumpingStatingPosition - this.jumpingOffset;
            
            if(newPosition < 0)//slå i taket
            {
                return Math.Max(this.jumpingStatingPosition - this.jumpingOffset, 0);
            }
            if(newPosition >this.origo.Y-Earth.s_radius)//slå i botten
            {
                return this.origo.Y-Earth.s_radius;
            }

            return newPosition;
        }

        public void Update(GameTime gameTime)
        {

            // this.PositionalTexture2D.YPos = this.GetPosition(gameTime);
        }

        private float GetSpinningDistanceOfEarth(float angleOfEarthRotation)
        {
            float partsOfAEntireLap = angleOfEarthRotation/2f;
            float totalCurcomferenceOfEarth = Earth.s_width * (float)Math.PI;

            return totalCurcomferenceOfEarth * partsOfAEntireLap;
        }
        private float GetRotationFromDistance(float spinningDistanceOfEarth, float radius)
        {
            var totalCurcomferenceOfMan = 2f*radius*(float)Math.PI;

            return  spinningDistanceOfEarth / totalCurcomferenceOfMan *2f;
        }
        private float totalRotation = 0;
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float angleOfEarthRotation)
        {
            var jumpOffset = this.GetJumpingOffset(gameTime);

            var distance = this.GetSpinningDistanceOfEarth(angleOfEarthRotation);
            var rotation =  this.GetRotationFromDistance(distance, Earth.s_radius+jumpOffset);
            totalRotation += rotation;

            var pos = CoordinatesFromRotationAndRadius
                .GetPosition(totalRotation, Earth.s_radius+jumpOffset)
                + this.origo
                ;

            var origin = new Vector2(this.PositionalTexture2D.Width/2, this.PositionalTexture2D.Height);
            
            spriteBatch.Draw(this.PositionalTexture2D.GetTexture()
            , pos
            , new Rectangle((int)0,(int)0, (int)this.PositionalTexture2D.Width, (int)this.PositionalTexture2D.Height )
            , Color.White
            , totalRotation
            , origin
            , this.TextureScaling
            , SpriteEffects.None
            , 1
            );
        }
    }
}