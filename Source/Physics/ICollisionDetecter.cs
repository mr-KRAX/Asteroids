namespace Asteroids {
  public interface ICollisionDetecter {
    void AddColliderComponent(IColliderComponent cc);
    void RemoveColliderComponent(IColliderComponent cc);
    void Update();
  }
}