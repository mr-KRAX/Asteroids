using System.Collections.Generic;

namespace Asteroids {
  class GameOverUI : UI {
    #region  UIElements
    private ITextObject _gameOver;
    private ITextObject _score;
    private ITextObject _bestScore;
    private ITextObject _guideLine;
    #endregion  UIElements
    private IGameStats _gameStats;

    public GameOverUI() {
      var gm = GameManager.GetInternalInstance();
      _gameStats = gm.GameStats;
      Vector2 offset = new Vector2(0, -30f);
      Vector2 pos = gm.Configs.CommonConfigs.WindowSize / 2 - offset;
      _gameOver = new TextObject("GAME OVER", pos - 2 * offset, TextAlign.CENTRE, true);


      _score = new TextObject("", pos, TextAlign.CENTRE);
      _bestScore = new TextObject("", pos += offset, TextAlign.CENTRE);
      _guideLine = new TextObject("Press ENTER to restart!", pos += offset, TextAlign.CENTRE);
      _guideLine.BlinkingOn = true;

      _allElements = new IBasicObject[] {_gameOver, _score, _guideLine, _bestScore };
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);
      _score.Text = $"Your Score: {_gameStats.Score}";
      _bestScore.Text = $"BEST: {_gameStats.BestScore}";
    }
  }
}