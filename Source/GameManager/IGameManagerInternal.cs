namespace Asteroids {
  public interface IGameManagerInternal {
    IConfigsStorage Configs { get; }
    IPhysicsHandler PhysicsHandler { get; }
    ICollisionDetecter CollisionDetecter { get; }
    IGameObject Ship { get; } // game state
    int Score { get; } // game state
    object GetTexture(TextureID id);
    void DestroyGameObject(IGameObject gameObject);
    void SpawnGameObject(IGameObject gameObject);
    void NotifyEnemyDead();
  }
}