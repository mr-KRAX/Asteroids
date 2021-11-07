using System;
using System.Collections.Generic;

namespace Asteroids {
  class CollisionDetecter : Module, ICollisionDetecter {
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

      public override int GetHashCode() {
        return base.GetHashCode();
      }
    }

    private List<IColliderComponent> _colliderComponents;
    private HashSet<Collision> _collisions;

    private Dictionary<Type, Type[]> _typeCollisions;

    private List<IColliderComponent> _ccToRemove;
    private List<IColliderComponent> _ccToAdd;

    public CollisionDetecter() {
      _colliderComponents = new List<IColliderComponent>();
      _ccToAdd = new List<IColliderComponent>();
      _ccToRemove = new List<IColliderComponent>();
      _collisions = new HashSet<Collision>();

      _typeCollisions = new Dictionary<Type, Type[]>();

      _typeCollisions[typeof(Asteroid)] = new Type[] { typeof(Bullet), typeof(Ship), typeof(Laser) };
      _typeCollisions[typeof(UFO)] = new Type[] { typeof(Bullet), typeof(Ship), typeof(Laser) };
      _typeCollisions[typeof(Debris)] = new Type[] { typeof(Bullet), typeof(Ship), typeof(Laser) };

      _typeCollisions[typeof(Bullet)] = new Type[] { typeof(Asteroid), typeof(UFO), typeof(Debris) };
      _typeCollisions[typeof(Laser)] = new Type[] { typeof(Asteroid), typeof(UFO), typeof(Debris) };
      _typeCollisions[typeof(Ship)] = new Type[] { typeof(Asteroid), typeof(UFO), typeof(Debris) };
    }

    public void UpdateComponentsList() {
      if (_ccToRemove.Count != 0) {
        HashSet<Collision> collisionsToRemove = new HashSet<Collision>();
        foreach (var cc in _ccToRemove) {
          _colliderComponents.Remove(cc);
          foreach (var col in _collisions)
            if (col.first == cc || col.second == cc)
              collisionsToRemove.Add(col);

        }
        foreach (var colToRemove in collisionsToRemove)
          _collisions.Remove(colToRemove);
        _ccToRemove.Clear();
      }

      if (_ccToAdd.Count != 0) {
        foreach (var go in _ccToAdd)
          _colliderComponents.Add(go);
        _ccToAdd.Clear();
      }
    }

    public void AddColliderComponent(IColliderComponent cc) {
      _ccToAdd.Add(cc);
    }

    public void RemoveColliderComponent(IColliderComponent cc) {
      _ccToRemove.Add(cc);
    }
    public override void Update(float deltaTime) {
      base.Update(deltaTime);

      UpdateComponentsList();
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
      if (!CheckTypesCollisions(lhs.Origin.GetType(), rhs.Origin.GetType()))
        return false;
      if ((lhs.Origin.Transform.Pos - rhs.Origin.Transform.Pos).Magnitude > (lhs.Radius + rhs.Radius))
        return false;

      Vector2[] poly1 = lhs.RealPolygon();
      Vector2[] poly2 = rhs.RealPolygon();

      if (poly1.Length > poly2.Length && poly2.Length > 1) {
        Vector2[] tmp = poly1;
        poly1 = poly2;
        poly1 = tmp;
      }

      for (int ib = 0; ib < poly1.Length; ib++) {
        int ie = (ib + 1) % poly1.Length;

        Vector2 axisProj = (poly1[ie] - poly1[ib]).RotateOnDegrees(90, Vector2.zero);

        float min1 = float.MaxValue;
        float max1 = float.MinValue;
        for (int i = 0; i < poly1.Length; i++) {
          float q = poly1[i].x * axisProj.x + poly1[i].y * axisProj.y;
          min1 = Math.Min(min1, q);
          max1 = Math.Max(max1, q);
        }

        float min2 = float.MaxValue;
        float max2 = float.MinValue;
        for (int i = 0; i < poly2.Length; i++) {
          float q = poly2[i].x * axisProj.x + poly2[i].y * axisProj.y;
          min2 = Math.Min(min2, q);
          max2 = Math.Max(max2, q);
        }

        if (max2 < min1 || min2 > max1)
          return false;
      }
      return true;
    }

    private bool CheckTypesCollisions(Type lhs, Type rhs) {
      if (_typeCollisions.ContainsKey(lhs))
        foreach (Type t in _typeCollisions[lhs])
          if (t == rhs)
            return true;
      return false;
    }
  }
}