namespace Asteroids {
  public interface ICommonConfigs {
    Vector2 WindowSize { get; }
    Vector2 FieldSize { get; }
    float DragForce { get; }
  }

  public interface IShipConfigs {
    float MaxSpeed { get; }
    float TurningSpeed { get; }
    float Thrust { get; }
    float BulletSpeed { get; }
    float BulletLifeTime { get; }
    float ShootDelay { get; }
  }

  public interface IEnemiesConfigs {
    float AsteroidMaxSpeed { get; }
    float UFOMaxSpeed { get; }
    float UFOThrust { get; }
  }

}