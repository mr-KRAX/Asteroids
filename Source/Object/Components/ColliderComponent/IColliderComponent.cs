using System.Collections.Generic;

namespace Asteroids {
  public delegate void OnCollisionCallback(IColliderComponent go);
  public interface IColliderComponent : IComponent {
    float Radius { get; }
    void UpdateVertices(IEnumerable<Vector2> newVertices);
    IEnumerable<Vector2> AbsoluteVertices();
    OnCollisionCallback OnCollisionEnter { get; set; }
    OnCollisionCallback OnCollisionStay { get; set; }
    OnCollisionCallback OnCollisionExit { get; set; }
  }
}