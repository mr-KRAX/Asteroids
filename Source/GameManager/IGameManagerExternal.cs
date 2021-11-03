using System.Collections.Generic;

//TODO: remove XNA dependency
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
  public interface IGameManagerExternal {
    void SetTexture(TextureID type, object gfx);
    void HandleInput(KeyboardState keyboardState);
    bool Update(float deltaTime);
    IEnumerable<IGraphicalObject> GraphicalObjects();
    IEnumerable<ITextObject> TextObjects();
    Vector2 WindowSize { get; }
  }
}