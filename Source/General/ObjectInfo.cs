namespace Asteroids {
  public enum ObjectInfoType {
    TEXT,
    GRAPHICAL
  }
  public struct ObjectInfo {
    public ObjectInfoType type;
    public TextureID textureID;
    public ColorRGB color;
    public Vector2 pos;
    public float rotation;
    public string text;
    public TextAlign align;

    public ObjectInfo(IBasicObject obj) {
      type = ObjectInfoType.GRAPHICAL;
      textureID = obj.Graphics.Texture;
      color = obj.Graphics.Color;
      pos = obj.Transform.Pos;
      rotation = obj.Transform.Rotation;
      text = "";
      align = TextAlign.LEFT;
    }

    public ObjectInfo(ITextObject obj) {
      type = ObjectInfoType.TEXT;
      textureID = obj.Graphics.Texture;
      color = obj.Graphics.Color;
      pos = obj.Transform.Pos;
      rotation = obj.Transform.Rotation;
      text = obj.Text;
      align = obj.Align;
    }
  }
}