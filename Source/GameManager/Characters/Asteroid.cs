using System;

namespace Asteroids {
  class Asteroid : GameObject {
    AsteroidsSpawner _spawner;

    public Asteroid(AsteroidsSpawner spawner) : base() {
      var gm = GameManager.GetInternalInstance();

      this.Graphics.Texture = gm.GetTexture(TextureID.ASTEROID1);

      Vector2[] polygon = { new Vector2(50, 50) };
      this.ColliderComponent.UpdateVertices(polygon);
      this.ColliderComponent.OnCollisionEnter = OnCollisionEnter;
      
      this.PhysicalComponent.MaxSpeed = gm.Configs.EnemiesConfigs.AsteroidMaxSpeed;

      _spawner = spawner;
    }

    private void OnCollisionEnter(IColliderComponent cc) {
      if (cc.Origin.GetType() == typeof(Bullet))
        _spawner.AsteroidDestroyed(this);
    }
  }
}