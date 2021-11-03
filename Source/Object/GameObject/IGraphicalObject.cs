namespace Asteroids {
  public interface IGraphicalObject {
    IGraphicsComponent Graphics { get; }
    ITransformComponent Transform { get; }
  }
}