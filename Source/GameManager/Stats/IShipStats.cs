namespace Asteroids {
  public interface IShipStats {
    Vector2 Pos { get; }
    float Rotation { get; }
    Vector2 Speed { get; }
    float LaserTimeOut { get; }
    int LaserCapacity { get; }
  }
}