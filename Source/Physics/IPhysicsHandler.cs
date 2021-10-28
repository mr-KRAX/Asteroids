namespace Asteroids {
  public interface IPhysicsHandler {
    Vector2 GetPositionLimits();
    void LinkPhysicalComponent(IPhysicalComponent pc);    
    float DragForce{get;}
    void Update(float deltaTime);
  }
}