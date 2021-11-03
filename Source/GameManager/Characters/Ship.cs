using Microsoft.Xna.Framework.Input;

// using static Asteroids.Logger;

namespace Asteroids {
  class Ship : GameObject {
    private IShipConfigs _configs;
    private float _shootTimeOut;

    public Ship() : base() {
      var gm = GameManager.GetInternalInstance();
      _configs = gm.Configs.ShipConfigs;

      this.PhysicalComponent.Drag = true;
      this.PhysicalComponent.MaxSpeed = _configs.MaxSpeed;

      Vector2[] polygon = { new Vector2(0, 24), new Vector2(-17, -24), new Vector2(17, -24) };
      this.ColliderComponent.UpdateVertices(polygon);
      this.ColliderComponent.OnCollisionEnter = OnCollisionEnter;
      this.ColliderComponent.OnCollisionExit = OnCollisionExit;

      this.Graphics.Texture = gm.GetTexture(TextureID.SHIP);
    }

    private void OnCollisionEnter(IColliderComponent cc) {
      if (cc.Origin.GetType() != typeof(Bullet))
        this.Graphics.Color = ColorRGB.Blue;
    }

    private void OnCollisionExit(IColliderComponent cc) {
      this.Graphics.Color = ColorRGB.White;
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);

      _shootTimeOut -= deltaTime;
    }

    public void Shoot() {
      if (_shootTimeOut > 0f)
        return;

      var bullet = new Bullet();
      bullet.Transform.Pos = Transform.Pos;
      bullet.PhysicalComponent.Speed = _configs.BulletSpeed * Transform.RotationDir;

      GameManager.GetInternalInstance().SpawnGameObject(bullet);
      _shootTimeOut = _configs.ShootDelay;
    }

    public void TurnRight() {
      Transform.Rotation += _configs.TurningSpeed;
    }

    public void TurnLeft() {
      Transform.Rotation -= _configs.TurningSpeed;
    }

    public void FlyForward() {
      PhysicalComponent.ApplyForce(Transform.RotationDir * _configs.Thrust);
    }
  }
}