namespace Asteroids {
  class Enemy : GameObject {
    protected IEnemySpawner _spawner;

    public Enemy(IEnemySpawner spawner) : base() {
      this.ColliderComponent.OnCollisionEnter = OnCollisionEnter;

      _spawner = spawner;
    }

    private void OnCollisionEnter(IColliderComponent cc) {
      if (cc.Origin.GetType() != typeof(Bullet) && cc.Origin.GetType() != typeof(Laser))
        return;
      _spawner.EnemyHit(this);
    }

    public override void Destroy() {
      _spawner.EnemyDestroyed(this);
      base.Destroy();
    }
  }
}