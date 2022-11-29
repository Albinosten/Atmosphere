using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Atmosphere
{
    public class AtmosphereGame : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public AtmosphereGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private Earth earth;
        private ManOnEarth manOnEarth;
        private GoalFlag goalFlag;
        public Vector2 Origo {get;set;}
        public static int s_PreferredBackBufferHeight = 600;
        public static int s_PreferredBackBufferWidth = 1100;
        protected override void Initialize()

        {
            this.graphics.PreferredBackBufferHeight = s_PreferredBackBufferHeight;
            this.graphics.PreferredBackBufferWidth = s_PreferredBackBufferWidth;
            this.graphics.ApplyChanges();

            this.Origo = new Vector2(this.graphics.PreferredBackBufferWidth/2,s_PreferredBackBufferHeight/2);
            // TODO: Add your initialization logic here
            
            this.earth = new Earth(this.GraphicsDevice);
            this.earth.CreateTexture();
            this.earth.PositionalTexture2D.XPos = s_PreferredBackBufferWidth/2 - Earth.s_radius;
            this.earth.PositionalTexture2D.YPos = s_PreferredBackBufferHeight/2- Earth.s_radius;
            
            this.manOnEarth = new ManOnEarth(this.GraphicsDevice, this.Origo);
            this.manOnEarth.CreateTexture();
            this.manOnEarth.InitializePosition();
            
            this.goalFlag = new GoalFlag(this.GraphicsDevice);
            this.goalFlag.XPos = this.earth.PositionalTexture2D.XPosCenter;
            this.goalFlag.YPos = this.earth.PositionalTexture2D.YPosCenter;
            this.goalFlag.CreateTexture();
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            var keyBoardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if(keyBoardState.IsKeyDown(Keys.P))
            {
                // this.manOnEarth.
            }
            this.manOnEarth.InitializeJump(gameTime,keyBoardState.IsKeyDown(Keys.Space));

            this.manOnEarth.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();

            var boxTexture = new Texture2D(this.GraphicsDevice, 3,3);
            boxTexture.SetData(RectangleTextureCreator.GetColorsWithBorders(3,3,Color.Black));
            

            var angleOfEarthRotation =  this.earth.Draw(gameTime, this.spriteBatch);
            this.manOnEarth.Draw(gameTime, this.spriteBatch, angleOfEarthRotation);
            this.goalFlag.Draw(gameTime, this.spriteBatch);
            
            this.spriteBatch.Draw(boxTexture
                , this.Origo
                ,Color.White
                );

            this.spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
