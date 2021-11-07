using System.Collections.Generic;

namespace Asteroids {
  abstract class UI : IUI {
    public bool IsActive { get; set; } = true;
    protected IBasicObject[] _allElements;
    public UI(){
        _allElements = new IBasicObject[0];
    }
    public virtual IEnumerable<IBasicObject> GetActiveElements() {
      foreach(var e in _allElements)
        if(e.IsActive) 
          yield return e;
    }
    public virtual void Update(float deltaTime) {
      foreach(var el in GetActiveElements())
        el.Update(deltaTime);
    }
  }
}