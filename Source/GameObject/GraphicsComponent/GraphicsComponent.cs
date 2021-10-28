namespace Asteroids {
  class GraphicsComponent : IGraphicsComponent {
    private object _texture;
    private ColorRGB _color;

    public GraphicsComponent(object texture = null){
      if (texture != null)
        UpdateTexture(texture);
      _color = ColorRGB.White;
    }

    public object Texture => _texture;
    public ColorRGB Color => _color;

    public void UpdateTexture(object newTexture) {
      _texture = newTexture;
    }
    public void SetColor(ColorRGB newColor) {
      _color = newColor;
    }
  }
}