namespace Asteroids {
  public interface IGraphicsComponent {
    object Texture{get;}
    ColorRGB Color{get;}

    void UpdateTexture(object newTexture);
    void SetColor(ColorRGB color);
  }
}