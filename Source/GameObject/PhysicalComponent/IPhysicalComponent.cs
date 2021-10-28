namespace Asteroids {
  public interface IPhysicalComponent {
    float Mass {get; set;}
    bool Drag {get; set;}
    Vector2 Speed {get; set;}
    float MaxSpeed {get; set;}
    void ApplyForce(Vector2 force);
    void RotateOnDegrees(float degrees);
    void UpdateState(float deltaTime);
  }
}