namespace Asteroids {
  class BasicObject : IBasicObject {
    public ITransformComponent Transform { get; private set; }
    public IGraphicsComponent Graphics { get; private set; }
    public bool IsActive { get; set; }
    public bool IsAlive { get; set; }

    public BasicObject() {
      this.Transform = new TransformComponent(this);
      this.Graphics = new GraphicsComponent(this);
      this.IsActive = true;
      this.IsAlive = true;
    }

    public virtual void Update(float deltaTime) {
      if (!IsAlive)
        throw new System.Exception("Dead object update!");
    }

    public virtual void Destroy() {
      this.IsAlive = false;
      this.Transform.OnDestroy();
      this.Graphics.OnDestroy();
    }
  }
}