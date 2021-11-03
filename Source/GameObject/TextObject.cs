namespace Asteroids {
  class TextObject : BasicObject, ITextObject {
    public string Text { get; set; }
    public TextAlign Align { get; set; }

    public TextObject() : this("", Vector2.zero, TextAlign.LEFT) { }
    public TextObject(string text, Vector2 pos) : this(text, pos, TextAlign.LEFT) { }
    public TextObject(string text, Vector2 pos, TextAlign align) : base() {
      var gm = GameManager.GetInternalInstance();

      this.Graphics.Texture = gm.GetTexture(TextureID.TEXT);

      this.Transform.Pos = pos;

      Text = text;
      Align = align;
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);
    }
  }
}