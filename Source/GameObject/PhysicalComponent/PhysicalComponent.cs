using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids {
  class PhysicalComponent : IPhysicalComponent {
    private IPhysicsHandler _physics;
    private float _mass; // kg
    private bool _drag;
    private Vector2 _speed;
    private float _maxSpeed;

    private Vector2 _instantForce;

    public PhysicalComponent(IBasicObject go) {
      Origin = go;
      _physics = GameManager.GetInternalInstance().PhysicsHandler;

      _mass = 1;
      _drag = false;
      _speed = Vector2.zero;
      _maxSpeed = 0f;

      _physics.AddPhysicalComponent(this);

      _instantForce = Vector2.zero;
    }

    private void correctSpeed(ref Vector2 speed) {
      if (speed.Magnitude > _maxSpeed)
        speed = speed.Dir * _maxSpeed;
    }

    public float Mass {
      get => _mass;
      set {
        if (value < 0) throw new ArgumentException();
        _mass = value;
      }
    }

    public bool Drag {
      get => _drag;
      set => _drag = value;
    }

    public Vector2 Speed {
      get => _speed;
      set { correctSpeed(ref value); _speed = value; }
    }

    public float MaxSpeed {
      get => _maxSpeed;
      set => _maxSpeed = value;
    }

    public void ApplyForce(Vector2 force) {
      _instantForce += force;
    }

    public void UpdateState(float deltaTime) {
      Vector2 acceleration = _instantForce / _mass;

      _speed += acceleration * deltaTime;
      correctSpeed(ref _speed);
      Origin.Transform.Pos += _speed * deltaTime;

      _instantForce = Vector2.zero;
    }

    public IBasicObject Origin { get; private set; }

    public void OnDestroy() {
      _physics.RemovePhysicalComponent(this);
    }
  }
}
