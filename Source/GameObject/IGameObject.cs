using Microsoft.Xna.Framework.Input;


namespace Asteroids {
  public interface IGameObject : IBasicObject {
    IPhysicalComponent PhysicalComponent { get; }
    IColliderComponent ColliderComponent { get; }
  }
}