using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids {
  class PhysicalComponent : IPhysicalComponent {
    private IPhysicsHandler _physics;
    private ICommonConfigs _configs;

    private float _mass;
    public float Mass {
      get => _mass;
      set {
        if (value < 0) throw new ArgumentException();
        _mass = value;
      }
    }
    public bool Drag { get; set; }
    public bool Teleportable { get; set; }

    private Vector2 _speed;
    public Vector2 Speed {
      get => _speed;
      set { _speed = correctSpeed(value); }
    }
    public float MaxSpeed { get; set; }
    public IBasicObject Origin { get; private set; }

    private Vector2 _instantForce;

    public PhysicalComponent(IBasicObject origin) {
      Origin = origin;
      var gm = GameManager.GetInternalInstance();
      _physics = gm.PhysicsHandler;
      _configs = gm.Configs.CommonConfigs;

      Mass = 1;
      Drag = false;
      Teleportable = true;
      Speed = Vector2.zero;
      MaxSpeed = 0f;

      _physics.AddPhysicalComponent(this);

      _instantForce = Vector2.zero;
    }

    public void UpdateState(float deltaTime) {
      Vector2 acceleration = _instantForce / Mass;

      Speed += acceleration * deltaTime;

      Origin.Transform.Pos += Speed * deltaTime;
      if (Teleportable)
        Origin.Transform.Pos = correctPosition(Origin.Transform.Pos);

      _instantForce = Vector2.zero;
    }

    private Vector2 correctSpeed(Vector2 speed) {
      if (speed.Magnitude > MaxSpeed)
        return speed.Dir * MaxSpeed;
      return speed;
    }
    
    private Vector2 correctPosition(Vector2 pos) {
      return new Vector2(
        (pos.x % _configs.FieldSize.x + _configs.FieldSize.x) % _configs.FieldSize.x,
        (pos.y % _configs.FieldSize.y + _configs.FieldSize.y) % _configs.FieldSize.y
      );
    }

    
    public void ApplyForce(Vector2 force) {
      _instantForce += force;
    }

    public void OnDestroy() {
      _physics.RemovePhysicalComponent(this);
    }
  }
}
