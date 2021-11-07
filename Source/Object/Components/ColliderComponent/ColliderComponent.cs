using System.Collections.Generic;
using System;

namespace Asteroids {
  class ColliderComponent : IColliderComponent {
    private ITransformComponent _transform;
    private IGameObject _go;
    private ICollisionDetecter _collisionDetector;
    private List<Vector2> _vertices;
    private float _radius;

    public OnCollisionCallback OnCollisionEnter { get; set; }
    public OnCollisionCallback OnCollisionStay { get; set; }
    public OnCollisionCallback OnCollisionExit { get; set; }

    public ColliderComponent(IGameObject go) :
             this(go, null, null, null, null) { }

    public ColliderComponent(IGameObject go, IEnumerable<Vector2> vertices,
                             OnCollisionCallback onCollisionEnter, OnCollisionCallback onCollisionStay, OnCollisionCallback onCollisionExit) {
      _vertices = new List<Vector2>();

      _go = go;
      _transform = go.Transform;
      _collisionDetector = GameManager.GetInternalInstance().CollisionDetecter;

      if (vertices != null)
        _vertices.AddRange(vertices);
      else
        _vertices.Add(Vector2.zero);

      OnCollisionEnter = onCollisionEnter;
      OnCollisionStay = onCollisionStay;
      OnCollisionExit = onCollisionExit;
      _radius = calculateRadius();

      _collisionDetector.AddColliderComponent(this);
    }

    private float calculateRadius() {
      float max = 0;
      foreach (Vector2 v in _vertices) {
        float l = v.Magnitude;
        max = (l > max ? l : max);
      }
      return max;
    }

    public void UpdateVertices(IEnumerable<Vector2> newVertices) {
      _vertices.Clear();
      _vertices.AddRange(newVertices);
      _radius = calculateRadius();
    }

    public IBasicObject Origin => _go;

    public float Radius => _radius;

    public Vector2[] RealPolygon() {
      Vector2[] realPolygon = _vertices.ToArray();
      for (int i = 0; i < realPolygon.Length; i++) {
        realPolygon[i] += Origin.Transform.Pos;
        realPolygon[i] = realPolygon[i].RotateOnDegrees(Origin.Transform.Rotation, Origin.Transform.Pos);
      }
      return realPolygon;
    }

    public void OnDestroy() {
      _collisionDetector.RemoveColliderComponent(this);
    }
  }
}