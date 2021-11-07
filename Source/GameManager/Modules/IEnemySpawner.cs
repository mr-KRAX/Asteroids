namespace Asteroids {
  public interface IEnemySpawner : IModule {
    void EnemyHit(IGameObject enemy);
    void EnemyDestroyed(IGameObject enemy);
  }
}