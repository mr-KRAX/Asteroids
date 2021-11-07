namespace Asteroids {
  public interface ICommonConfigs {
    Vector2 WindowSize { get; }
    Vector2 FieldSize { get; }
    float DragForce { get; }
    int LevelStep { get; }
  }

  public interface IShipConfigs {
    float MaxSpeed { get; }
    float TurningSpeed { get; }
    float Thrust { get; }
    float BulletSpeed { get; }
    float BulletLifeTime { get; }
    float ShootDelay { get; }
    float LaserChargingTime { get; }
    float MaxLaserCapacity { get; }
    float LaserLifeTime { get; }
    float LaserLength { get; }
  }

  public interface IEnemiesConfigs {
    float AsteroidMaxSpeed { get; }
    float DebrisMaxSpeed { get; }
    float UFOMaxSpeed { get; }
    float UFOThrust { get; }
  }

  public interface IUIConfigs {
    float TextBlinkingRate { get; }
    float TextTypingSpeed { get; }
  }
}