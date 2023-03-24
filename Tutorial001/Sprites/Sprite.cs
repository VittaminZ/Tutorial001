using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial001.Manage;
using Tutorial001.Model;

namespace Tutorial001.Sprites
{
    public class Sprite : Component
    {
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animation;

        protected Vector2 _position;

        protected Texture2D _texture;

        public Input Input;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("This is bruh");
        }

        public Sprite(Dictionary<string, Animation> animation)
        {
            _animation = animation;
            _animationManager = new AnimationManager(_animation.First().Value);
        }

        public Sprite(Texture2D texture) 
        {
            _texture = texture;
        }
 
        public override void Update(GameTime gameTime, List<Sprite> sprite)
        {

        }
    }
}
