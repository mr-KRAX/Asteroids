namespace Asteroids {
  public interface IGameStats {
    int Score { get; }
    int BestScore { get; }
    int Level { get; }
    IShipStats ShipStats { get; }
    IEnemySpawnerStats EnemySpawnerStats { get; }
  }
}