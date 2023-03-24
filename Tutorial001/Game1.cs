using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tutorial001.Manage;
using Tutorial001.Model;
using Tutorial001.Sprites;

namespace Tutorial001
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenHeight;
        public static int ScreeWidth;

        private SpriteFont  _font;
        private Texture2D _bg;
        private List<Sprite>  _sprite;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreeWidth = _graphics.PreferredBackBufferWidth;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("File");
            _bg = Content.Load<Texture2D>("bg");

            var animations = new Dictionary<string, Animation>()
            {
                {"WalkLeft",new Animation(Content.Load<Texture2D>("walkleft"), 10) },
                {"RunLeft",new Animation(Content.Load<Texture2D>("runleft"), 8) },
                {"WalkRight",new Animation(Content.Load<Texture2D>("walk"), 10) },
                {"RunRight",new Animation(Content.Load<Texture2D>("run"), 8) },
                {"Brake",new Animation(Content.Load<Texture2D>("brake"), 1) },
                {"BrakeLeft",new Animation(Content.Load<Texture2D>("brakeleft"), 1) },
                {"idle",new Animation(Content.Load<Texture2D>("idle"), 4) },
                {"couch",new Animation(Content.Load<Texture2D>("couch"), 1) },
                {"jumpright",new Animation(Content.Load<Texture2D>("jump"), 8) },
                {"jumpleft",new Animation(Content.Load<Texture2D>("jumpleft"), 8) },
            };
            _sprite = new List<Sprite>()
            {
                new Player(animations)
                {
                    Position = new Vector2(100,510),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Left = Keys.A,
                        Right = Keys.D,
                        Down = Keys.S,
                        Shift = Keys.LeftShift,
                        Space = Keys.Space,
                    },
                }, 
            };

        }

        protected override void Update(GameTime gameTime)
        {

            foreach (var Player in _sprite)
                Player.Update(gameTime, _sprite);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            _spriteBatch.Draw(_bg, new Vector2(0, 0),Color.White);
            _spriteBatch.DrawString(_font, "Move W,A,S,D ,Run Shift, Jump Space", new Vector2(100, 50), Color.Black);
            foreach (var Player in _sprite)
                Player.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}