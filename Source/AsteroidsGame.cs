using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Asteroids;

namespace AsteroidsGame {
  public class AsteroidsGame : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private IGameManagerExternal _gameManager;
    private Microsoft.Xna.Framework.Vector2 _windowSize;

    // private bool _firstFrame = true;

    public AsteroidsGame() {
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

      _gameManager.SetTexture(TextureID.SHIP, Content.Load<Texture2D>("Ship"));
      _gameManager.SetTexture(TextureID.ASTEROID1, Content.Load<Texture2D>("Asteroid_1"));
      _gameManager.SetTexture(TextureID.UFO, Content.Load<Texture2D>("UFO"));
      _gameManager.SetTexture(TextureID.BULLET, Content.Load<Texture2D>("Bullet"));

      _gameManager.SetTexture(TextureID.TEXT, Content.Load<SpriteFont>("MainText"));
      
      // _gameManager.UpdateGraphics();
      base.LoadContent();
    }

    protected override void Update(GameTime gameTime) {
      _gameManager.HandleInput(Keyboard.GetState());

      bool exit = !_gameManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
      if (exit) 
        Exit();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);
      
      _spriteBatch.Begin();
      foreach(IGraphicalObject go in _gameManager.GraphicalObjects()){
        Logger.LogStatic("IGraphicalObject", $"{go}");
        Texture2D texture = go.Graphics.Texture as Texture2D;
        var origin = TextureOrigin(texture);
        var position = ToScreenPos(go.Transform.Pos);
        var color = ToXnaColor(go.Graphics.Color);
        var rotation = ToRadians(go.Transform.Rotation);

        _spriteBatch.Draw(texture, position, null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
      }

      foreach(ITextObject to in _gameManager.TextObjects()) {
        SpriteFont font = to.Graphics.Texture as SpriteFont;
        var textMiddlePoint = font.MeasureString(to.Text);
        textMiddlePoint.Y /= 2;
        switch (to.Align){
          case TextAlign.LEFT:
            textMiddlePoint.X = 0;
            break;
          case TextAlign.CENTRE:
            textMiddlePoint.X /=2;
            break;
          case TextAlign.RIGHT:
            break;
        }
        var position = ToScreenPos(to.Transform.Pos);
        var color = ToXnaColor(to.Graphics.Color);
        var rotation = ToRadians(to.Transform.Rotation);
        _spriteBatch.DrawString(font, to.Text, position, color, rotation, textMiddlePoint, 1f, SpriteEffects.None, 0f);

      }
      _spriteBatch.End();

      base.Draw(gameTime);
    }

    private Microsoft.Xna.Framework.Vector2 ToScreenPos(Asteroids.Vector2 pos) {
      return new Microsoft.Xna.Framework.Vector2(pos.x, _windowSize.Y-pos.y);
    }

    private Microsoft.Xna.Framework.Vector2 ToXnaVector2(Asteroids.Vector2 v) {
      return new Microsoft.Xna.Framework.Vector2(v.x, v.y);
    }

    private Microsoft.Xna.Framework.Color ToXnaColor(Asteroids.ColorRGB c){
      return new Microsoft.Xna.Framework.Color(c.r, c.g, c.b);
    }

    private Microsoft.Xna.Framework.Vector2 TextureOrigin(Texture2D texture){
      return new Microsoft.Xna.Framework.Vector2(texture.Width / 2f, texture.Height / 2f);
    }

    private float ToRadians(float degrees) {
      return degrees *(float)Math.PI / 180f;
    }

  }



}
