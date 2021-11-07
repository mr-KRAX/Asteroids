using System.Collections.Generic;

namespace Asteroids {
  public interface IGameManagerExternal {
    void HandleInput(KeyPressed keyPressed);
    bool Update(float deltaTime);
    IEnumerable<ObjectInfo> GetObjectsToDraw();
    Vector2 WindowSize { get; }
  }
}