﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Levels;
using ShakaTD.Manager;
using System;

namespace ShakaTD.Components.Enemys
{
    enum Enemys
    {
        Soldat, Airplain
    }

    enum Direction
    {
        Left, Right, Up, Down
    }

    class Enemy : Game_Component
    {
        public float leben;
        public float speed;
        public int money;
        public int schaden;
        public Direction currentDirection, oldDirection;
        public FieldType[,] map;

        
        private float waypoint;
        private bool lastWaypoint;

        public Enemy(Vector2 spawn, FieldType[,] map) : base()
        {
            this.map = map;                                                 //gefühlt waste of speicher                                                    
            Position = new Vector2(spawn.X - Level.BLOCKSIZE, spawn.Y);
            Height = Level.BLOCKSIZE;
            Width = Height;
            lastWaypoint = false;
            Weight = 5;
            currentDirection = Direction.Right;
            oldDirection = currentDirection;
            rotation = -(float)Math.PI / 2;
            calculateWaypoint();
        }

        public override void Update(GameTime gameTime)
        {
            if (leben <= 0)
            {
                activ = false;
                UI_Manager.getInstance().stats.money += money;
            }
                

            float force = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            deltaX = 0;
            deltaY = 0;

            if (currentDirection == Direction.Right && (Position.X <= waypoint || lastWaypoint))
                deltaX = force;
            else if (currentDirection == Direction.Left && (Position.X >= waypoint || lastWaypoint))
                deltaX = -force;
            else if (currentDirection == Direction.Up && (Position.Y >= waypoint || lastWaypoint))
                deltaY = -force;
            else if (currentDirection == Direction.Down && (Position.Y <= waypoint || lastWaypoint))
                deltaY = force;
            else
            {
                if (!lastWaypoint)
                {
                    changeDirection();
                }
            }

            if (lastWaypoint)
            {    
                if (Position.X >= Game1.SCREEN_WIDTH + 30 || Position.X <= -30 || Position.Y + Height <= -30 || Position.Y >= Game1.SCREEN_HEIGHT + 30)
                {
                    activ = false;
                    //Leben abziehen und res freigeben
                    UI_Manager.getInstance().stats.leben -= schaden;
                }                
            }

            Position = new Vector2(Position.X + deltaX, Position.Y + deltaY);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height),
                    null, Color.White, rotation + (float)(Math.PI * 0.5), origin, SpriteEffects.None, 1);
            //Wenn die gegner später mal eine eigene Lebensanzeige bekommen sollten
            //base.Draw(spriteBatch);
        }

        private void calculateWaypoint()
        {
            int currX = Toolbox.fixCoords(Position.X) / Level.BLOCKSIZE;
            int currY = Toolbox.fixCoords(Position.Y) / Level.BLOCKSIZE;
            int forceX = 0, forceY = 0;

            if (currentDirection == Direction.Right)
                forceX = 1;
            else if (currentDirection == Direction.Left)
                forceX = -1;
            if (currentDirection == Direction.Up)
                forceY = -1;
            else if (currentDirection == Direction.Down)
                forceY = 1;

            int moveX = forceX, moveY = forceY;
            try
            {
                while (map[currX + moveX, currY + moveY] == FieldType.Path)
                {
                    moveX += forceX;
                    moveY += forceY;
                    if (currX + moveX >= 16 || currX + moveX < 0 || currY >= 7 || currY < 0)
                        lastWaypoint = true;
                }
            } catch (Exception e) { }

            if (forceX != 0)
                waypoint = (currX + moveX + (forceX * -1)) * Level.BLOCKSIZE;
            else
                waypoint = (currY + moveY + (forceY * -1)) * Level.BLOCKSIZE;
        }

        private void changeDirection()
        {
            int currX = Toolbox.fixCoords(Position.X) / Level.BLOCKSIZE;
            int currY = Toolbox.fixCoords(Position.Y) / Level.BLOCKSIZE;

            if (currX + 1 <= 16 && map[currX + 1, currY] == FieldType.Path && oldDirection != Direction.Left)
            {
                currentDirection = Direction.Right;
                rotation = -(float)Math.PI / 2;
            }
            else if (currY - 1 >= 0 && map[currX, currY - 1] == FieldType.Path && oldDirection != Direction.Down)
            {
                currentDirection = Direction.Up;
                rotation = (float)Math.PI;
            }
            else if (currY + 1 <= 7 && map[currX, currY + 1] == FieldType.Path && oldDirection != Direction.Up)
            {
                currentDirection = Direction.Down;
                rotation = 0;
            }
            else if (currX - 1 >= 0 && map[currX - 1, currY] == FieldType.Path && oldDirection != Direction.Right)
            {
                currentDirection = Direction.Left;
                rotation = (float)Math.PI / 2;
            }
            
            oldDirection = currentDirection;
            calculateWaypoint();
        }
    }
}
