using System.Collections.Generic;

namespace Asteroids {
  class PauseUI : UI {
    #region  UIElements
    private TextObject _title;
    private TextObject _guideLine;
    private TextObject _continueLine;
    #endregion  UIElements


    public PauseUI() {
      Vector2 pos = GameManager.GetInternalInstance().Configs.CommonConfigs.WindowSize / 2;
      _title = new TextObject($"PAUSE", pos + new Vector2(0, 100), TextAlign.CENTRE);
      _title.BlinkingOn = true;
      string guideText = "UP         - Fly forward\n" +
                         "LEFT/RIGHT - Turn around\n" +
                         "SPACE      - Shoot\n" +
                         "CTRL       - Laser";
      
      _guideLine = new TextObject(guideText, pos, TextAlign.CENTRE);

      _continueLine = new TextObject($"Press ENTER to resume!", pos + new Vector2(0, -100f), TextAlign.CENTRE);
      _allElements = new IBasicObject[]{_title, _guideLine, _continueLine};
      
    }
  }
}