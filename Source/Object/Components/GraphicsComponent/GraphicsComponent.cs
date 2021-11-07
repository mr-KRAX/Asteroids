namespace Asteroids {
  class GraphicsComponent : IGraphicsComponent {
    public TextureID Texture { get; set; }
    public ColorRGB Color { get; set; }
    public IBasicObject Origin { get; private set; }

    public GraphicsComponent(IBasicObject go, TextureID texture = TextureID.NONE) {
      Origin = go;
      Texture = texture;
      Color = ColorRGB.White;
    }
    public void OnDestroy() { }
  }
}