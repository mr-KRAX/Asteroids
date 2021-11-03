using System.Collections.Generic;

namespace Asteroids {
  class StartMenuUI : IUI {
    private TextObject _title;

    public StartMenuUI() {
      string str = "ASTEROIDS by KRAX";
      Vector2 pos = GameManager.GetInternalInstance().Configs.CommonConfigs.WindowSize / 2;
      _title = new TextObject(str, pos, TextAlign.CENTRE);
    }

    public void Update(float deltaTime) { }

    public IEnumerable<ITextObject> GetGraphicsUIObjects() {
      throw new System.NotImplementedException();
    }

    public IEnumerable<ITextObject> GetTextUIObjects() {
      yield return _title;
    }
  }
}