using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids {
  class PhysicalComponent : IPhysicalComponent {
    private IPhysicsHandler _physics;
    // private IPhysicsState;
    private ITransformComponent _transform;
    private float _mass; // kg
    private bool _drag;
    private Vector2 _speed;
    private float _maxSpeed;

    private Vector2 _instantForce;
    private float _instantRotation;

    private Vector2 limits = new Vector2(500, 250);

    public PhysicalComponent(IPhysicsHandler physics, ITransformComponent transform) {
      _physics = physics;
      _physics.LinkPhysicalComponent(this);

      _transform = transform;
      _mass = 1;
      _drag = false;
      _speed = Vector2.zero;
      _maxSpeed = 0f;

      _instantForce = Vector2.zero;
      _instantRotation = 0;
    }

    #region  IPhysicalObject

    public float Mass {
      get => _mass;
      set {
        if (value < 0) throw new ArgumentException();
        _mass = value;
      }
    }
    public bool Drag { get => _drag; set => _drag = value; }
    public Vector2 Speed {
      get => _speed;
      set { correctSpeed(ref value); _speed = value; }
    }
    public float MaxSpeed {get => _maxSpeed; set => _maxSpeed = value; }

    public void ApplyForce(Vector2 force) {
      _instantForce += force;
    }

    public void RotateOnDegrees(float degrees) {
      _instantRotation += degrees;
    }

    private void correctSpeed(ref Vector2 speed) {
      if (speed.Magnitude > _maxSpeed)
        speed = Speed.Dir * _maxSpeed;
    }
    public void UpdateState(float deltaTime) {
      if(_drag && _speed.Magnitude != 0)
        ApplyForce(_physics.DragForce * _speed.Dir * (-1));

      _transform.Rotation += _instantRotation;

      Vector2 acceleration = _instantForce / _mass;

      _speed += acceleration * deltaTime;
      correctSpeed(ref _speed);
      _transform.Pos += _speed * deltaTime;

      _instantForce = Vector2.zero;
      _instantRotation = 0;
    }

    #endregion IPhysicalObject
  }
}
