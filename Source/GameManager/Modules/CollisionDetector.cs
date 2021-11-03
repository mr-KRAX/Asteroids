using System.Collections.Generic;

namespace Asteroids {
  class CollisionDetecter : ICollisionDetecter {
    struct Collision {
      public IColliderComponent second;
      public IColliderComponent first;

      public Collision(IColliderComponent first, IColliderComponent second) {
        if (second.GetHashCode() > first.GetHashCode()) {
          this.first = first;
          this.second = second;
        } else {
          this.first = second;
          this.second = first;
        }
      }

      public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType())
          return false;
        var col = (Collision)obj;
        return first == col.first && second == col.second;
      }

      // override object.GetHashCode
      public override int GetHashCode() {
        return base.GetHashCode();
      }
    }

    private List<IColliderComponent> _colliderComponents;
    private List<Collision> _collisions;
    public CollisionDetecter() {
      _colliderComponents = new List<IColliderComponent>();
      _collisions = new List<Collision>();
    }

    public void AddColliderComponent(IColliderComponent cc) {
      _colliderComponents.Add(cc);
    }

    public void RemoveColliderComponent(IColliderComponent cc) {
      _colliderComponents.Remove(cc);
      List<Collision> collisionsToRemove = new List<Collision>();
      foreach (var col in _collisions) {
        if (col.first == cc || col.second == cc)
          collisionsToRemove.Add(col);
      }
      foreach (var colToRemove in collisionsToRemove)
        _collisions.Remove(colToRemove);
    }
    public void Update() {
      for (int i = 0; i < _colliderComponents.Count; i++) {
        for (int j = i + 1; j < _colliderComponents.Count; j++) {
          var oi = _colliderComponents[i];
          var oj = _colliderComponents[j];

          var col = new Collision(oi, oj);
          if (!checkCollision(oi, oj)) {
            if (_collisions.Contains(col)) {
              _collisions.Remove(col);
              oi.OnCollisionExit?.Invoke(oj);
              oj.OnCollisionExit?.Invoke(oi);
            }
            continue;
          }
          if (_collisions.Contains(col)) {
            oi.OnCollisionStay?.Invoke(oj);
            oj.OnCollisionStay?.Invoke(oi);
          } else {
            _collisions.Add(col);
            oi.OnCollisionEnter?.Invoke(oj);
            oj.OnCollisionEnter?.Invoke(oi);
          }
        }
      }
    }

    private bool checkCollision(IColliderComponent lhs, IColliderComponent rhs) {
      if ((lhs.Origin.Transform.Pos - rhs.Origin.Transform.Pos).Magnitude <= (lhs.Radius + rhs.Radius))
        return true;
      return false;
    }
  }
}