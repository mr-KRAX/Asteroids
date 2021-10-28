using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
  public class GameObject : IGameObject, IGraphicalObject{
    protected ITransformComponent transform;
    protected IGraphicsComponent graphics;
    protected IPhysicalComponent physicalBody;
    protected IColliderComponent collider;

    protected bool isActive;

    public GameObject(){
      transform = new TransformComponent(new Vector2(1000, 800));
      graphics = null;
      physicalBody = null;
      isActive = true;
    }
    public virtual void Update() {}
    public virtual void ProcessInput(KeyboardState keyboardState) {}

    public ITransformComponent Transform => transform;
    public IGraphicsComponent Graphics => graphics;
    public IPhysicalComponent PhysicalBody => physicalBody;
    public bool IsActive {get => isActive; set => isActive = value;}
  }

}