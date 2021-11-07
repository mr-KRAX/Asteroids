namespace Asteroids {
  public interface IBasicObject : IUpdatable {
    ITransformComponent Transform { get; }
    IGraphicsComponent Graphics { get; }
    bool IsAlive { get; }
    void Destroy();
  }
}