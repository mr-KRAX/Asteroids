using System.Collections.Generic;

namespace Asteroids {
  public delegate void OnCollisionCallback(IColliderComponent go);
  public interface IColliderComponent : IComponent {
    /// <summary>
    /// Get the distance to the most distant vertice of collider polygon 
    /// </summary>
    /// <value>The distance</value>
    float Radius { get; }
    /// <summary>
    /// Updates the collider polygon.
    /// </summary>
    /// <param name="newVertices">New collider vertices with respect to the centre (Transform.Pos) of the object </param>
    void UpdateVertices(IEnumerable<Vector2> newVertices);
    /// <summary>
    /// Get an array of points representing the collider with respect to the point (0,0)
    /// </summary>
    /// <returns>Real coordinates of the collider</returns>
    Vector2[] RealPolygon();
    /// <summary>
    /// Callback 
    /// </summary>
    OnCollisionCallback OnCollisionEnter { get; set; }
    /// <summary>
    /// Callback 
    /// </summary>
    OnCollisionCallback OnCollisionStay { get; set; }
    /// <summary>
    /// Callback 
    /// </summary>
    OnCollisionCallback OnCollisionExit { get; set; }
  }
}