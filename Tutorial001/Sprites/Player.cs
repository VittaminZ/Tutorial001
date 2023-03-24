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
    public class Player : Sprite
    {
        public float Speed = 2f;
        public float runSpeed = 4f;

        private float CD = 2f;
        public Vector2 Velocity;
        private bool couch = false;
        private bool jump = false;
        public Player(Dictionary<string, Animation> animation) : base(animation)
        {
            _animation = animation;
            _animationManager = new AnimationManager(_animation.First().Value);
        }

        public Player(Texture2D texture) : base(texture)
        {
            _texture = texture;
        }

        protected virtual void Move()
        {


            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Velocity.X = -Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Left) && (Keyboard.GetState().IsKeyDown(Input.Shift)))
            {
                Velocity.X = -runSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                couch = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Input.Down))
            {
                couch = false;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Velocity.X = +Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Right) && (Keyboard.GetState().IsKeyDown(Input.Shift)))
            {
                Velocity.X = +runSpeed;
            }
            if ((Keyboard.GetState().IsKeyDown(Input.Space) || Keyboard.GetState().IsKeyDown(Input.Up)) && jump == false && CD > 1.25f)
            {
                _position.Y -= 60;
                Velocity.Y = -30f;
                CD = 0f;
                jump = true;
            }
            if (_position.Y >= 510)
            {
                jump = false;
            }
            if (jump == false)
            {
                Velocity.Y = 0;
            }


        }
        protected virtual void SetAnimation()
        {
            if (couch)
            {
                jump = true;
                _animationManager.Play(_animation["couch"]);
                Velocity.X = 0;
                if (_position.Y < 510)
                {
                    Velocity.Y += 3f;
                }
            }
            else if (jump && Velocity.X == 0)
            {
                _animationManager.Play(_animation["jumpright"]);
                Velocity.Y += 2f;
            }
            else if (jump && Velocity.X < 0)
            {
                _animationManager.Play(_animation["jumpleft"]);
                Velocity.Y += 2f;
            }
            else if (jump && Velocity.X >= 0)
            {
                _animationManager.Play(_animation["jumpright"]);
                Velocity.Y += 2f;
            }
            else if (Velocity.X < 0 && Velocity.X > -4)
                _animationManager.Play(_animation["WalkLeft"]);
            else if (Velocity.X >= 4)
                _animationManager.Play(_animation["RunRight"]);
            else if (Velocity.X <= -4)
                _animationManager.Play(_animation["RunLeft"]);
            else if (Velocity.X > 0 && Velocity.X < 4)
                _animationManager.Play(_animation["WalkRight"]);
            else
                _animationManager.Play(_animation["idle"]);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprite)
        {
            CD += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Move();
            SetAnimation();
            if (_position.X < 0)
            {
                _position.X = 800;
            }
            else if (_position.X > 800)
            {
                _position.X = 0;
            }
            _animationManager.Update(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;
        }
    }
}

