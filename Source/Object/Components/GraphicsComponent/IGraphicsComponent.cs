namespace Asteroids {
  public interface IGraphicsComponent : IComponent {
    TextureID Texture { get; set; }
    ColorRGB Color { get; set; }
  }
}