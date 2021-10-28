namespace Asteroids {
  public struct ColorRGB {
    float _r;
    float _g;
    float _b;

    public ColorRGB(float red, float green, float blue) {
      _r = Utils.Clamp(red, 0f, 1f);
      _g = Utils.Clamp(green, 0f, 1f);
      _b = Utils.Clamp(blue, 0f, 1f);
    }

    public float r {
      get => _r;
      set => _r = Utils.Clamp(value, 0f, 1f);
    }
    public float g {
      get => _g;
      set => _g = Utils.Clamp(value, 0f, 1f);
    }
    public float b {
      get => _b;
      set => _b = Utils.Clamp(value, 0f, 1f);
    }

    public static ColorRGB White => new ColorRGB(1f,1f,1f);
    public static ColorRGB Red => new ColorRGB(1f,0,0);
    public static ColorRGB Green => new ColorRGB(0,1f,0);
    public static ColorRGB Blue => new ColorRGB(0,0,1f);

  }
}