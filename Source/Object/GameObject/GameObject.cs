using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
  class GameObject : BasicObject, IGameObject {
    public IPhysicalComponent PhysicalComponent { get; private set; }
    public IColliderComponent ColliderComponent { get; private set; }
    public GameObject() : base() {
      this.PhysicalComponent = new PhysicalComponent(this);
      this.ColliderComponent = new ColliderComponent(this);
    }
    public override void Update(float deltaTime) {
      base.Update(deltaTime);
    }
    public override void Destroy() {
      this.PhysicalComponent.OnDestroy();
      this.ColliderComponent.OnDestroy();
      base.Destroy();
    }
  }

}