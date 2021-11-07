namespace Asteroids {
  class TextObject : BasicObject, ITextObject {
    public string Text {
      get => _currentText;
      set {
        _origText = value;
        _currentText = value;
      }
    }
    public TextAlign Align { get; set; }
    public bool BlinkingOn { get; set; }

    private ColorRGB _origColor;
    private string _origText;
    private string _currentText;
    private IUIConfigs _configs;
    private float _blinkingTimeOut;
    private float _typingTimeOut;
    private bool _typingInProgress;

    public TextObject() : this("", Vector2.zero, TextAlign.LEFT, ColorRGB.White) { }
    public TextObject(string text, Vector2 pos) : this(text, pos, TextAlign.LEFT, ColorRGB.White) { }
    public TextObject(string text, Vector2 pos, TextAlign align, bool big = false) : this(text, pos, align, ColorRGB.White, big) { }
    public TextObject(string text, Vector2 pos, TextAlign align, ColorRGB color, bool big = false) : base() {
      var gm = GameManager.GetInternalInstance();

      this.Graphics.Texture = (big ? TextureID.BIGTEXT : TextureID.TEXT) ;
      this.Graphics.Color = color;

      this.Transform.Pos = pos;

      Text = text;
      Align = align;
      BlinkingOn = false;

      _configs = gm.Configs.UIConfigs;
      _origColor = color;
      _blinkingTimeOut = _configs.TextBlinkingRate;
      _typingInProgress = false;
    }

    public void StartTyping() {
      _typingInProgress = true;
      _typingTimeOut = _configs.TextTypingSpeed;
      _currentText = "_";
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);
      if (BlinkingOn) {
        _blinkingTimeOut -= deltaTime;
        if (_blinkingTimeOut < 0f) {
          _currentText = (_currentText == _origText ? "" : _origText);
          _blinkingTimeOut = _configs.TextBlinkingRate;
        }
      }

      if (_typingInProgress) {
        if (_currentText.Length != _origText.Length) {
          _typingTimeOut -= deltaTime;

          if (_typingTimeOut < 0f) {
            _currentText = _origText.Substring(0, _currentText.Length) + "_";
            _typingTimeOut = _configs.TextTypingSpeed;
            if (_currentText.Length == _origText.Length) {
              _currentText = _origText;
              _typingInProgress = false;
            }
          }
        }
      }
    }
  }
}