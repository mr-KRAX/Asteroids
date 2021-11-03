namespace Asteroids {
  public interface IGraphicsComponent : IComponent {
    object Texture { get; set; }
    ColorRGB Color { get; set; }
  }
}