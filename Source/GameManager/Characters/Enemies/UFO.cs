namespace Asteroids {
  class UFO : Enemy {
    private float _trust;
    private IGameObject _aim;

    public UFO(IEnemySpawner spawner) : base(spawner) {
      var gm = GameManager.GetInternalInstance();

      this.PhysicalComponent.Drag = true;
      this.PhysicalComponent.MaxSpeed = gm.Configs.EnemiesConfigs.UFOMaxSpeed;

      _trust = gm.Configs.EnemiesConfigs.UFOThrust;
      _aim = gm.Ship;
    }

    public override void Update(float deltaTime) {
      if (_aim.Transform.Pos != this.Transform.Pos) {
        FollowAim(_aim.Transform.Pos);
      }
      base.Update(deltaTime);
    }

    private void FollowAim(Vector2 aimPos) {
      Vector2 dir = (aimPos - Transform.Pos).Dir;
      PhysicalComponent.ApplyForce(dir * _trust);
    }
  }
}