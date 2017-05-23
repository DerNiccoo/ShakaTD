using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ShakaTD.Manager;

namespace ShakaTD
{
    public class Game1 : Game
    {
        static int SCREEN_HEIGHT = 720, SCREEN_WIDTH = 1280;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Screen_Manager screenManager;

        public Game1()
        {
            this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            Content.RootDirectory = "ShakaTD";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Content_Manager.getInstance().LoadTextures(this.Content);
            screenManager = new Screen_Manager(this);
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState ms = Mouse.GetState();
            Window.Title = "ShakuTD  --  Maus (" + ms.Position.X + " , " + ms.Position.Y + ")";

            screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            screenManager.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
