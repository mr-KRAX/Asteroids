namespace Asteroids {
  class GraphicsComponent : IGraphicsComponent {
    public object Texture { get; set; }
    public ColorRGB Color { get; set; }
    public IBasicObject Origin { get; private set; }

    public GraphicsComponent(IBasicObject go, object texture = null) {
      Origin = go;
      if (texture != null)
        Texture = texture;
      Color = ColorRGB.White;
    }

    public void OnDestroy() { }
  }
}