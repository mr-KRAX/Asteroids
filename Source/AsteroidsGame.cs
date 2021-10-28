using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Asteroids;

namespace AsteroidsGame {
  public class AsteroidsGame : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameManager _gameManager;
    private Microsoft.Xna.Framework.Vector2 _windowSize;

    public AsteroidsGame() {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
      _windowSize = new Microsoft.Xna.Framework.Vector2(1000,800);
      _gameManager = new GameManager();
    }

    protected override void Initialize() {
      _graphics.IsFullScreen = false;
      _graphics.PreferredBackBufferWidth = 1000;
      _graphics.PreferredBackBufferHeight = 800;
      _graphics.ApplyChanges();

      base.Initialize();
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      _gameManager.SetTexture(TextureID.SHIP1, Content.Load<Texture2D>("Ship"));
      _gameManager.SetTexture(TextureID.ASTEROID1, Content.Load<Texture2D>("Asteroid_1"));
      _gameManager.SetTexture(TextureID.UFO, Content.Load<Texture2D>("UFO"));
      
      _gameManager.UpdateGraphics();
      base.LoadContent();
    }

    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      _gameManager.HandleInput(Keyboard.GetState());
      _gameManager.UpdatePhysics((float)gameTime.ElapsedGameTime.TotalSeconds);
      _gameManager.UpdateObjects();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);
      
      _spriteBatch.Begin();
      foreach(IGraphicalObject go in _gameManager.ActiveGraphicalObjects()){
        Texture2D texture = go.Graphics.Texture as Texture2D;
        var origin = new Microsoft.Xna.Framework.Vector2(texture.Width / 2f, texture.Height / 2f);
        var position = ToScreenPos(go.Transform.Pos);
        var rotation = go.Transform.Rotation * (float)Math.PI / 180f;
        Color color = new Color(go.Graphics.Color.r, go.Graphics.Color.g, go.Graphics.Color.b);

        _spriteBatch.Draw(texture, position, null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
      }
      _spriteBatch.End();

      // TODO: Add your drawing code here
      base.Draw(gameTime);
    }

    private Microsoft.Xna.Framework.Vector2 ToScreenPos(Asteroids.Vector2 pos) {
      return new Microsoft.Xna.Framework.Vector2(pos.x, _windowSize.Y-pos.y);
    }

  }



}
