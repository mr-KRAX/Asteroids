using System;
using System.Collections.Generic;
using System.Text;


namespace Asteroids {
  public struct Vector2 {
    private float _x;
    private float _y;

    public Vector2(float x, float y) {
      this._x = x;
      this._y = y;
    }

    public float x { get => _x; set => _x = value; }
    public float y { get => _y; set => _y = value; }

    /// <summary>
    /// The length of the vector
    /// </summary>
    public float Magnitude { get { return (float)Math.Sqrt(x * x + y * y); } }
    
    /// <summary>
    /// Get a vector of unit length that is parallel to the original vector
    /// </summary>
    public Vector2 Dir { get { return this / Magnitude; } }

    /// <summary>
    /// Get the cockwise rotated vector
    /// </summary>
    /// <param name="degrees">angle in degrees</param>
    /// <returns>new rotated vector</returns>
    public Vector2 RotateOnDegrees(float degrees, Vector2 axis) {
      float angle = - degrees / 180f * (float)Math.PI; // minus to rotate clock
      Vector2 v = this - axis;
      float sin = (float)Math.Sin(angle);
      float cos = (float)Math.Cos(angle);

      Vector2 res = new Vector2(cos * v.x - sin * v.y,
                         sin * v.x + cos * v.y);
      return res + axis;
    }

    public override String ToString() => $"({x:000}, {y:000})";

    public static Vector2 operator +(Vector2 lhs, Vector2 rhs) => new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
    public static Vector2 operator -(Vector2 lhs, Vector2 rhs) => new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
    public static Vector2 operator *(Vector2 lhs, float rhs) => new Vector2(lhs.x * rhs, lhs.y * rhs);
    public static Vector2 operator *(float lhs, Vector2 rhs) => rhs * lhs;
    public static Vector2 operator /(Vector2 lhs, float rhs) => new Vector2(lhs.x / rhs, lhs.y / rhs);
    public static Vector2 operator /(float lhs, Vector2 rhs) => rhs / lhs;
    public static Vector2 operator -(Vector2 v) => Vector2.zero - v;

    public static bool operator == (Vector2 lhs, Vector2 rhs) => (lhs.x == rhs.x) && (lhs.y == rhs.y); 
    public static bool operator != (Vector2 lhs, Vector2 rhs) => !(lhs == rhs); 
    public override int GetHashCode() => (x, y).GetHashCode();
    public override bool Equals(object obj) => (obj is Vector2) && this == (Vector2)obj;


    public static Vector2 zero { get { return new Vector2(); } }
    public static Vector2 one { get {return new Vector2(1,1); } }  
  }




}