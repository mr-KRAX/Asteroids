namespace Asteroids {
  class UFO : GameObject {
    private float maxSpeed = 280f;
    private float flyForce = 1000f;

    private IGameObject _aim;

    public UFO(IPhysicsHandler physics, IGameObject aim, object gfx = null) : base() {
      physicalBody = new PhysicalComponent(physics, Transform);
      physicalBody.Drag = true;
      physicalBody.MaxSpeed = maxSpeed;

      _aim = aim;

      graphics = new GraphicsComponent();
    }

    public override void Update() {
      if (_aim.Transform.Pos != this.Transform.Pos){
        FollowAim(_aim.Transform.Pos);
      }
      
      base.Update();
    }

    private void FollowAim(Vector2 aimPos){
      Vector2 dir = (aimPos - Transform.Pos).Dir;
      PhysicalBody.ApplyForce(dir*flyForce);
    }
  }
}