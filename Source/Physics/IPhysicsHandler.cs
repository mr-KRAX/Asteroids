namespace Asteroids {
  public interface IPhysicsHandler {
    void Update(float deltaTime);
    void AddPhysicalComponent(IPhysicalComponent pc);  
    void RemovePhysicalComponent(IPhysicalComponent pc);  
  }
}