using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Asteroids;

namespace AsteroidsGame {
  public class AsteroidsGame : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private IGameManagerExternal _gameManager;
    private Microsoft.Xna.Framework.Vector2 _windowSize;
    private Dictionary<TextureID, object> _graphicsStorage;
    public AsteroidsGame() {
      _graphicsStorage = new Dictionary<TextureID, object>();

      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      _gameManager = GameManager.GetExternalInstance();
      _windowSize = ToXnaVector2(_gameManager.WindowSize);

      _graphics.IsFullScreen = false;
      _graphics.PreferredBackBufferWidth = (int)_windowSize.X;
      _graphics.PreferredBackBufferHeight = (int)_windowSize.Y;
      _graphics.ApplyChanges();

      base.Initialize();
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      _graphicsStorage[TextureID.SHIP] = Content.Load<Texture2D>("Ship");
      _graphicsStorage[TextureID.SHIPTHRUSTING] = Content.Load<Texture2D>("ShipThrusting");
      _graphicsStorage[TextureID.SHIPCRASHED] = Content.Load<Texture2D>("ShipCrashed");
      _graphicsStorage[TextureID.ASTEROID1] = Content.Load<Texture2D>("Asteroid_1");
      _graphicsStorage[TextureID.ASTEROID2] = Content.Load<Texture2D>("Asteroid_2");
      _graphicsStorage[TextureID.DEBRIS] = Content.Load<Texture2D>("Debris");
      _graphicsStorage[TextureID.UFO] = Content.Load<Texture2D>("UFO");
      _graphicsStorage[TextureID.BULLET] = Content.Load<Texture2D>("Bullet");
      _graphicsStorage[TextureID.LASER] = Content.Load<Texture2D>("Laser");
      _graphicsStorage[TextureID.NONE] = Content.Load<Texture2D>("None");

      _graphicsStorage[TextureID.TEXT] = Content.Load<SpriteFont>("Font");
      _graphicsStorage[TextureID.BIGTEXT] = Content.Load<SpriteFont>("BigFont");
      base.LoadContent();
    }

    protected override void Update(GameTime gameTime) {
      _gameManager.HandleInput(TranslateKeyPressed);

      bool exit = !_gameManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      if (exit)
        Exit();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      _spriteBatch.Begin();
      foreach (ObjectInfo oi in _gameManager.GetObjectsToDraw()) {
        switch (oi.type) {
          case ObjectInfoType.GRAPHICAL:
            DrawGraphicalObject(oi);
            break;
          case ObjectInfoType.TEXT:
            DrawTextObject(oi);
            break;
        }
      }
      _spriteBatch.End();

      base.Draw(gameTime);
    }

    private void DrawGraphicalObject(ObjectInfo oi) {
      Texture2D texture = _graphicsStorage[oi.textureID] as Texture2D;
      var origin = TextureOrigin(texture);
      var position = ToScreenPos(oi.pos);
      var rotation = ToRadians(oi.rotation);
      var color = ToXnaColor(oi.color);

      _spriteBatch.Draw(texture, position, null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
    }

    private void DrawTextObject(ObjectInfo oi) {
      SpriteFont font = _graphicsStorage[oi.textureID] as SpriteFont;
      var textMiddlePoint = font.MeasureString(oi.text);
      textMiddlePoint.Y /= 2;
      switch (oi.align) {
        case TextAlign.LEFT:
          textMiddlePoint.X = 0;
          break;
        case TextAlign.CENTRE:
          textMiddlePoint.X /= 2;
          break;
        case TextAlign.RIGHT:
          break;
      }
      var position = ToScreenPos(oi.pos);
      var rotation = ToRadians(oi.rotation);
      var color = ToXnaColor(oi.color);
      _spriteBatch.DrawString(font, oi.text, position, color, rotation, textMiddlePoint, 1f, SpriteEffects.None, 0f);
    }

    private Microsoft.Xna.Framework.Vector2 ToScreenPos(Asteroids.Vector2 pos) {
      return new Microsoft.Xna.Framework.Vector2(pos.x, _windowSize.Y - pos.y);
    }

    bool TranslateKeyPressed(Asteroids.ControlKeys key) {
      switch (key) {
        case Asteroids.ControlKeys.ENTER:
          return Keyboard.GetState().IsKeyDown(Keys.Enter);
        case Asteroids.ControlKeys.QUIT:
          return Keyboard.GetState().IsKeyDown(Keys.Q);
        case Asteroids.ControlKeys.PAUSE:
          return Keyboard.GetState().IsKeyDown(Keys.Escape);
        case Asteroids.ControlKeys.DEBUG:
          return Keyboard.GetState().IsKeyDown(Keys.D);

        case Asteroids.ControlKeys.THRUST:
          return Keyboard.GetState().IsKeyDown(Keys.Up);
        case Asteroids.ControlKeys.LEFT:
          return Keyboard.GetState().IsKeyDown(Keys.Left);
        case Asteroids.ControlKeys.RIGHT:
          return Keyboard.GetState().IsKeyDown(Keys.Right);
        case Asteroids.ControlKeys.SHOOT:
          return Keyboard.GetState().IsKeyDown(Keys.Space);
        case Asteroids.ControlKeys.LASER:
          return Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.RightControl);

        default:
          return false;
      }
    }

    private Microsoft.Xna.Framework.Vector2 ToXnaVector2(Asteroids.Vector2 v) {
      return new Microsoft.Xna.Framework.Vector2(v.x, v.y);
    }

    private Microsoft.Xna.Framework.Color ToXnaColor(Asteroids.ColorRGB c) {
      return new Microsoft.Xna.Framework.Color(c.r, c.g, c.b);
    }

    private Microsoft.Xna.Framework.Vector2 TextureOrigin(Texture2D texture) {
      return new Microsoft.Xna.Framework.Vector2(texture.Width / 2f, texture.Height / 2f);
    }

    private float ToRadians(float degrees) {
      return degrees * (float)Math.PI / 180f;
    }
  }
}