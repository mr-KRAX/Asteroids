namespace Asteroids {
  public enum TextAlign {
    LEFT,
    CENTRE,
    RIGHT,
  }

  public interface ITextObject : IBasicObject {
    string Text { get; }
    TextAlign Align { get; }
  }
}