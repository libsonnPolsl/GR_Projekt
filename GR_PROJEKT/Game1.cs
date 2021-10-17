using GR_Projekt.Content.Fonts;
using GR_Projekt.Content.Images;
using GR_Projekt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GR_Projekt
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _background;
        private SpriteFont _arial12;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>(assetName: Images.backgroundImage);
            _arial12 = Content.Load<SpriteFont>(assetName: Fonts.arial(fontSize: 12));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_background, GraphicsDevice.Viewport.Bounds, Colors.red);
            _spriteBatch.DrawString(_arial12, "Test text", Vector2.Zero, Colors.red) ;

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
