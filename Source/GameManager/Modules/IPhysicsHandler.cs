namespace Asteroids {
  public interface IPhysicsHandler : IModule {
    void AddPhysicalComponent(IPhysicalComponent pc);
    void RemovePhysicalComponent(IPhysicalComponent pc);
  }
}