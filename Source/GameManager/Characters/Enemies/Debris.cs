namespace Asteroids {
  class Debris : Enemy {
    public Debris(IEnemySpawner spawner) : base(spawner) {
      var gm = GameManager.GetInternalInstance();
      this.PhysicalComponent.MaxSpeed = gm.Configs.EnemiesConfigs.DebrisMaxSpeed;
    }
  }
}