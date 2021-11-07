namespace Asteroids {
  class CommonConfigs : ICommonConfigs {
    public Vector2 WindowSize => new Vector2(1000f, 800f);
    public Vector2 FieldSize => new Vector2(1000f, 700f);
    public float DragForce => 300f;
    public int LevelStep => 20;
  }

  class ShipConfigs : IShipConfigs {
    public float MaxSpeed => 300f;
    public float TurningSpeed => 4f;
    public float Thrust => 1000f;
    public float BulletSpeed => 450f;
    public float BulletLifeTime => 1.5f;
    public float ShootDelay => 0.1f;
    public float LaserChargingTime => 5f;
    public float MaxLaserCapacity => 5f;
    public float LaserLifeTime => 0.5f;
    public float LaserLength => 1000f;
  }

  class EnemiesConfigs : IEnemiesConfigs {
    public float AsteroidMaxSpeed => 150f;
    public float DebrisMaxSpeed => 250f;
    public float UFOMaxSpeed => 150f;
    public float UFOThrust => 500f;
  }

  class UIConfigs : IUIConfigs {
    public float TextBlinkingRate => 0.7f;
    public float TextTypingSpeed => 0.1f;
  }
}