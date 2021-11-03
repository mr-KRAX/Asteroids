using System;

namespace Asteroids {
  class TransformComponent : ITransformComponent {

    private Vector2 _position;
    private float _rotation;


    public TransformComponent(IBasicObject go) {
      Origin = go;
    }

    public Vector2 Pos { get; set; }
    public float Rotation {
      get => _rotation;
      set { _rotation = (value % 360 + 360) % 360; }
    }
    public Vector2 RotationDir => new Vector2((float)Math.Sin(_rotation * (float)Math.PI / 180),
                                              (float)Math.Cos(-_rotation * (float)Math.PI / 180));

    public IBasicObject Origin { get; private set; }
    public void OnDestroy() { }
  }
}