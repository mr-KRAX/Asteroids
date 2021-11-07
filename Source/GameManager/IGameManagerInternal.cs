using System.Collections.Generic;

namespace Asteroids {
  public interface IGameManagerInternal {
    IConfigsStorage Configs { get; }
    IPhysicsHandler PhysicsHandler { get; }
    ICollisionDetecter CollisionDetecter { get; }
    IGameObject Ship { get; } // game state
    IGameStats GameStats { get; }
    void DestroyGameObject(IGameObject gameObject);
    void SpawnGameObject(IGameObject gameObject);
    IEnumerable<IGameObject> ActiveGameObjects();
    void NotifyEnemyDead();
    void NotifyShipCrashed();
  }
}