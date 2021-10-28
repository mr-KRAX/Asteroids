using Microsoft.Xna.Framework.Input;


namespace Asteroids {
  public interface IGameObject {
    ITransformComponent Transform {get;}
    IGraphicsComponent Graphics {get;}
    IPhysicalComponent PhysicalBody {get;}

    bool IsActive {get; set;}

    void Update();
    //TODO: do via CommandPattern 
    void ProcessInput(KeyboardState keyboardState);
  }
}