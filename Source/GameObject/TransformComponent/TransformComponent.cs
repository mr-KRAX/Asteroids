using System;

namespace Asteroids {
  class TransformComponent : ITransformComponent {

    private Vector2 _position;
    private float _rotation;

    private Vector2 _limits;

    public TransformComponent(Vector2 limits) {
      _limits = limits;
    }

    public Vector2 Pos {
      get => _position;
      set { 
        Vector2 newPos = new Vector2();
        newPos.x = (value.x % _limits.x + _limits.x) % _limits.x;
        newPos.y = (value.y % _limits.y + _limits.y) % _limits.y;
        _position = newPos;
      }
    }
    public float Rotation {
      get => _rotation;
      set { _rotation = (value % 360 + 360) % 360; }
    }
    public Vector2 RotationDir => new Vector2((float)Math.Sin(_rotation * (float)Math.PI / 180),
                                              (float)Math.Cos(-_rotation * (float)Math.PI / 180));
  }
}