namespace Asteroids {
  public interface IPhysicalComponent : IComponent {
    float Mass { get; set; }
    bool Drag { get; set; }
    Vector2 Speed { get; set; }
    float MaxSpeed { get; set; }
    void ApplyForce(Vector2 force);
    void UpdateState(float deltaTime);
  }
}