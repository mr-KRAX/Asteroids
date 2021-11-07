using Microsoft.Xna.Framework.Input;
namespace Asteroids {
  class Ship : GameObject, IShipStats {
    private IShipConfigs _configs;
    private float _shootTimeOut;
    private float _laserTimeOut;
    private int _laserCapacity;
    private bool _thrusting;
    private bool _crushed;

    public Ship() : base() {
      var gm = GameManager.GetInternalInstance();
      _configs = gm.Configs.ShipConfigs;

      this.PhysicalComponent.Drag = true;
      this.PhysicalComponent.MaxSpeed = _configs.MaxSpeed;

      Vector2[] polygon = { new Vector2(0, 24), new Vector2(-17, -24), new Vector2(17, -24) };
      this.ColliderComponent.UpdateVertices(polygon);
      this.ColliderComponent.OnCollisionEnter = OnCollisionEnter;

      this.Graphics.Texture = TextureID.SHIP;

      _shootTimeOut = 0f;
      _laserCapacity = 0;
      _laserTimeOut = _configs.LaserChargingTime;
      _thrusting = false;
      _crushed = false;
    }

    private void OnCollisionEnter(IColliderComponent cc) {
      Graphics.Texture = TextureID.SHIPCRASHED;
      _crushed = true;
      GameManager.GetInternalInstance().NotifyShipCrashed();
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);
      if (_crushed)
        return;

      if (_thrusting) {
        Graphics.Texture = TextureID.SHIPTHRUSTING;
        _thrusting = false;
      } else
        Graphics.Texture = TextureID.SHIP;


      _shootTimeOut -= deltaTime;
      if (_laserCapacity < _configs.MaxLaserCapacity) {
        _laserTimeOut -= deltaTime;
        if (_laserTimeOut > 0f)
          return;

        _laserTimeOut = _configs.LaserChargingTime;
        _laserCapacity++;
        if (_laserCapacity == _configs.MaxLaserCapacity)
          _laserTimeOut = 0f;
      }
    }

    public void Laser() {
      if (_laserCapacity == 0 || _shootTimeOut > 0f)
        return;

      var laser = new Laser();

      laser.Transform.Pos = Transform.Pos + Transform.RotationDir * (_configs.LaserLength / 2f + 24f);
      laser.Transform.Rotation = Transform.Rotation;

      GameManager.GetInternalInstance().SpawnGameObject(laser);
      _laserCapacity--;
      _laserTimeOut = _configs.LaserChargingTime;
    }

    public void Shoot() {
      if (_shootTimeOut > 0f)
        return;

      var bullet = new Bullet();
      bullet.Transform.Pos = Transform.Pos + Transform.RotationDir * 24f;
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
      _thrusting = true;
      PhysicalComponent.ApplyForce(Transform.RotationDir * _configs.Thrust);
    }
    
    #region IShipStats
    public Vector2 Pos => Transform.Pos;
    public float Rotation => Transform.Rotation;
    public Vector2 Speed => PhysicalComponent.Speed;
    public float LaserTimeOut => _laserTimeOut;
    public int LaserCapacity => _laserCapacity;
    #endregion IShipStats
  }
}