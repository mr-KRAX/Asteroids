namespace Asteroids {
  class Module : IModule {
    public bool IsActive { get; set; } = true;
    public virtual void Update(float deltaTime) { }
  }
}