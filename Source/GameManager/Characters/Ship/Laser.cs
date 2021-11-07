namespace Asteroids {
  class Laser : GameObject {
    private float _timeLeft;
    public Laser() : base() {
      this.Graphics.Texture = TextureID.LASER;
      var polygon = new Vector2[]{new Vector2( 2,  500), new Vector2( 2, -500),
                                  new Vector2(-2, -500), new Vector2(-2, 500)};
      this.ColliderComponent.UpdateVertices(polygon);

      this.PhysicalComponent.Teleportable = false;
      var gm = GameManager.GetInternalInstance();
      _timeLeft = gm.Configs.ShipConfigs.LaserLifeTime;
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);

      _timeLeft -= deltaTime;
      if (_timeLeft < 0f)
        GameManager.GetInternalInstance().DestroyGameObject(this);
    }
  }
}