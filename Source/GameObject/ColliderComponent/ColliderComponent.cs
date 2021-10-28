using System.Collections.Generic;

namespace Asteroids {
  class ColliderComponent : IColliderComponent {
    private List<Vector2> _vertices;
    OnCollision _onCollision;
    public ColliderComponent(OnCollision onCollision, params Vector2[] vertices) {
      _vertices = new List<Vector2>();
      _vertices.AddRange(vertices);
      _onCollision = onCollision;
    }
  }
}