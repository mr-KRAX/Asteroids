namespace Asteroids {
  public interface IEnemySpawnerStats {
    uint AsteroidCount { get; }
    uint UFOsCount { get ; }
    uint DebrisCount { get; }
  }
}