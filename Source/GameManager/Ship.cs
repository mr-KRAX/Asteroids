using Microsoft.Xna.Framework.Input;

namespace Asteroids {
  class Ship : GameObject {
    
    private float maxSpeed = 300f;
    private float turningSpeed = 3f;
    private float flyForce = 1000f;

    public Ship(IPhysicsHandler physics) : base() {
      transform.Pos = new Vector2(100,100);

      physicalBody = new PhysicalComponent(physics, Transform);
      physicalBody.Drag = true;
      physicalBody.MaxSpeed = maxSpeed;

 
      graphics = new GraphicsComponent(); 
    }

    public override void Update() {

      base.Update();
    }

    public void TurnRight(){
      PhysicalBody.RotateOnDegrees(turningSpeed);
    }
    public void TurnLeft(){
      PhysicalBody.RotateOnDegrees(-turningSpeed);
    }

    public void FlyForward() {
      PhysicalBody.ApplyForce(Transform.RotationDir * flyForce);
    }
  }
}