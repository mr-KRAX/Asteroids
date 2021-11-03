namespace Asteroids {

  public interface ITransformComponent : IComponent {
    Vector2 Pos { get; set; }
    float Rotation { get; set; }
    Vector2 RotationDir { get; }
  }
}