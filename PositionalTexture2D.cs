using Microsoft.Xna.Framework.Graphics;

namespace Atmosphere
{
    public interface IPositionalTexture2D
    {
        float XPos {get;set;}
        float Width {get;}
        float YPos {get;set;}
        float Height{get;}
        Texture2D GetTexture();
    }

    public class PositionalTexture2D : IPositionalTexture2D
    {
        private Texture2D texture;
        
        public float XPos {get;set;}
        public float XPosCenter => this.XPos + this.Width/2;

        public float Width => this.texture.Width * this.scale;
        public float YPos {get;set;}
        public float YPosCenter=> this.YPos + this.Height/2;
        public float Height => this.texture.Height * this.scale;
        public float scale {get;set;}
        
        public PositionalTexture2D(Texture2D texture)
        {
            this.YPos = 1;
            this.XPos = 1;
            this.scale = 1;
            this.texture = texture;
        }
        public Texture2D GetTexture()
        {
            return this.texture;
        }

       
    }


}