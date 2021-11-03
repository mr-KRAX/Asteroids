namespace Asteroids {
  public interface IComponent {
    void OnDestroy();
    IBasicObject Origin { get; }
  }
}