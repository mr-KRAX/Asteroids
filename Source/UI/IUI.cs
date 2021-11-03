using System.Collections.Generic;

namespace Asteroids {
  public interface IUI {
    IEnumerable<ITextObject> GetTextUIObjects();
    void Update(float deltaTime);

  }
}