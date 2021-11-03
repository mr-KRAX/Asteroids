namespace Asteroids {
  class Bullet : GameObject {
    float _timeLeft;

    public Bullet() : base() {
      var gm = GameManager.GetInternalInstance();

      this.Graphics.Texture = gm.GetTexture(TextureID.BULLET);

      this.PhysicalComponent.MaxSpeed = gm.Configs.ShipConfigs.BulletSpeed;
      this.PhysicalComponent.Teleportable = false;

      _timeLeft = gm.Configs.ShipConfigs.BulletLifeTime;
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);

      _timeLeft -= deltaTime;
      if (_timeLeft < 0f)
        GameManager.GetInternalInstance().DestroyGameObject(this);
    }
  }
}