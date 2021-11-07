namespace Asteroids {
  public interface ICollisionDetecter : IModule {
    void AddColliderComponent(IColliderComponent cc);
    void RemoveColliderComponent(IColliderComponent cc);
  }
}