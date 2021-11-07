using System.Collections.Generic;

namespace Asteroids {
  class DebugUI : UI {
    #region  UIElements
    private ITextObject _title;
    private ITextObject _debugInfo;
    private List<ITextObject> _colliders;
    #endregion  UIElements

    private string _debugInfoPattern = "Level:  {0}\n" +
                                       "AstCnt: {1}\n" + 
                                       "UFOCnt: {2}\n" +
                                       "DebCnt: {3}\n" +
                                       "FPS:    {4}";

    private IGameStats _stats;
    private IGameManagerInternal _gm;

    public DebugUI() {
      _gm = GameManager.GetInternalInstance();
      Vector2 windowSize = _gm.Configs.CommonConfigs.WindowSize;
      Vector2 pos = new Vector2(windowSize.x - 100f, windowSize.y / 4);

      _stats = _gm.GameStats;
      _title = new TextObject($"DEBUG", pos, TextAlign.CENTRE, ColorRGB.Green);
      _debugInfo = new TextObject(_debugInfoPattern, pos + new Vector2(0, -100f), TextAlign.CENTRE, ColorRGB.Green);
      _colliders = new List<ITextObject>();

      _allElements= new IBasicObject[]{_title, _debugInfo};
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);
      _debugInfo.Text = string.Format(_debugInfoPattern,
                                      _stats.Level,
                                      _stats.EnemySpawnerStats.AsteroidCount,
                                      _stats.EnemySpawnerStats.UFOsCount,
                                      _stats.EnemySpawnerStats.DebrisCount,
                                      (int)(1f/deltaTime));

      _colliders.Clear();
      foreach(IGameObject go in _gm.ActiveGameObjects()) {
        var polygon = go.ColliderComponent.RealPolygon();
        for (int i = 0; i < polygon.Length; i++)
          _colliders.Add(new TextObject ("+", polygon[i], TextAlign.CENTRE, ColorRGB.Green));
      }
     }

    public override IEnumerable<IBasicObject> GetActiveElements() {
      foreach(var v in _colliders)
        yield return v;
      foreach(var e in base.GetActiveElements())
        yield return e;
    }
  }
}