namespace Asteroids {
  class Asteroid : GameObject {
    
    private float turningSpeed = 3f;
    private float flyForce = 1000f;

    public Asteroid(IPhysicsHandler physics, Vector2 speed) : base() {
      physicalBody = new PhysicalComponent(physics, Transform);

      physicalBody.MaxSpeed = speed.Magnitude;
      physicalBody.Speed = speed;

      graphics = new GraphicsComponent();
    }
  }
}