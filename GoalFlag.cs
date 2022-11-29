using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atmosphere
{
    public class GoalFlag
    {
        public static int s_width => 20;
        public static int s_height => 50;
        public float XPos {get;set;}
        public float YPos {get;set;}

        private readonly GraphicsDevice graphicsDevice;
        public PositionalTexture2D Flag {get;set;}
        public PositionalTexture2D Pole {get;set;}
        public GoalFlag(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void CreateTexture()
        {
            var flagHeight = s_height /5;
            var flagWidth = s_width;

            var flagTexture = new Texture2D(this.graphicsDevice, flagWidth,flagHeight);
            flagTexture.SetData(TriangleTextureCreator.Create(flagHeight, flagWidth, Color.Chocolate));
            
            var poleHeight = s_height;
            var poleWidth = s_width/5;
            var poleTexture = new Texture2D(this.graphicsDevice,poleHeight,poleWidth);
            poleTexture.SetData(RectangleTextureCreator.GetColorsWithBorders(poleWidth,poleHeight, Color.White,null));
            
            this.Flag = new PositionalTexture2D(flagTexture)
            {
                XPos = this.XPos,
                YPos = this.YPos,
            };
            this.Pole = new PositionalTexture2D(poleTexture)
            {
                XPos = this.XPos,
                YPos = this.YPos,
            };
        }

        private float elapsedMilliSeconds = 0;
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
            elapsedMilliSeconds += gameTime.ElapsedGameTime.Milliseconds;

            // var flagPos = new Vector2(this.Flag.XPos, this.Flag.YPos+this.Pole.Height);
            // spriteBatch.Draw(this.Flag.GetTexture()
            // , flagPos
            // , new Rectangle(0,0, (int)this.Flag.Width, (int)this.Flag.Height )
            // , Color.White
            // , elapsedMilliSeconds * Earth.s_speed
            // , new Vector2((int)this.Flag.Width+Earth.s_radius, (int)this.Flag.Height/2)
            // , Vector2.One
            // , SpriteEffects.None
            // , 1
            // );

            var polePos = new Vector2(this.Pole.XPos, this.Pole.YPos);
            spriteBatch.Draw(this.Pole.GetTexture()
            , polePos
            , new Rectangle(0,0, (int)this.Pole.Width, (int)this.Pole.Height )
            , Color.White
            , elapsedMilliSeconds * Earth.s_speed 
            , new Vector2((int)this.Pole.Width+Earth.s_radius, (int)this.Pole.Height/2)
            , Vector2.One
            , SpriteEffects.None
            , 1
            );
        }
    }
}