namespace Asteroids {
  class UFO : GameObject {
    private float _trust;
    AsteroidsSpawner _spawner;
    private IGameObject _aim;

    public UFO(AsteroidsSpawner spawner) : base() {
      var gm = GameManager.GetInternalInstance();

      this.Graphics.Texture = gm.GetTexture(TextureID.UFO);

      Vector2[] polygon = { new Vector2(0, 17) };
      this.ColliderComponent.UpdateVertices(polygon);
      this.ColliderComponent.OnCollisionEnter = OnCollisionEnter;

      this.PhysicalComponent.Drag = true;
      this.PhysicalComponent.MaxSpeed = gm.Configs.EnemiesConfigs.UFOMaxSpeed;

      _trust = gm.Configs.EnemiesConfigs.UFOThrust;
      _spawner = spawner;
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

    private void OnCollisionEnter(IColliderComponent cc) {
      if (cc.Origin.GetType() == typeof(Bullet))
        _spawner.UFODestroyed(this);
    }
  }
}