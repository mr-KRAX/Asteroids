using System;

namespace Asteroids {
  class Asteroid : Enemy {
    public Asteroid(IEnemySpawner spawner) : base(spawner) {
      var gm = GameManager.GetInternalInstance();
      this.PhysicalComponent.MaxSpeed = gm.Configs.EnemiesConfigs.AsteroidMaxSpeed;
    }
  }
}