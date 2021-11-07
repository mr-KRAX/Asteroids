using System.Collections.Generic;

namespace Asteroids {
  class StartMenuUI : UI {
    #region  UIElements
    private ITextObject _title;
    private ITextObject _guideLine;
    #endregion  UIElements


    private string _titleText = "ASTEROIDS by KRAX";
    private string _guideLineText = "Press ENTER to begin!";
    private float _waitTimeOut;
    private bool _titleIsWaiting;

    public StartMenuUI() {
      Vector2 pos = GameManager.GetInternalInstance().Configs.CommonConfigs.WindowSize / 2;
      _title = new TextObject("_", pos, TextAlign.CENTRE);
      _title.BlinkingOn = true;
      _waitTimeOut = 3f + 1.8f;
      _titleIsWaiting = true;

      _guideLine = new TextObject(_guideLineText, pos + new Vector2(0, -30f), TextAlign.CENTRE);
      _guideLine.BlinkingOn = true;
      _guideLine.IsActive = false;

      _allElements = new IBasicObject[] { _title, _guideLine };

    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);
      if (_waitTimeOut >= 0f) {
        _waitTimeOut -= deltaTime;
        if (_titleIsWaiting && _waitTimeOut < 3f) {
          _title.BlinkingOn = false;
          _title.Text = _titleText;
          _title.StartTyping();
          _titleIsWaiting = false;
        }
        if (_waitTimeOut < 0f)
          _guideLine.IsActive = true;
      }
    }
  }
}