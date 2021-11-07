namespace Asteroids {
  public enum TextAlign {
    LEFT,
    CENTRE,
    RIGHT,
  }

  public interface ITextObject : IBasicObject {
    string Text { get; set; }
    TextAlign Align { get; set; }
    bool BlinkingOn { get; set; }
    void StartTyping();
  }
}