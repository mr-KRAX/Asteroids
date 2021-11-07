using System.Collections.Generic;

namespace Asteroids {
  public interface IUI : IUpdatable{
    IEnumerable<IBasicObject> GetActiveElements();
  }
}