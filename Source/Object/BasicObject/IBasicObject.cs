namespace Asteroids {
  public interface IBasicObject {
    ITransformComponent Transform { get; }
    IGraphicsComponent Graphics { get; }
    bool IsActive { get; set; }
    bool IsAlive { get; }
    void Update(float deltatime);
    void Destroy();
  }
}