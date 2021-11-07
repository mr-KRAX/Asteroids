using System.Collections.Generic;

namespace Asteroids {
  class InGameUI : UI {
    #region  UIElements
    private TextObject _shipPos;
    private TextObject _shipRotation;
    private TextObject _shipSpeed;
    private TextObject _score;
    private ITextObject _shipLaserState;
    #endregion  UIElements


    private IGameStats _gameStats;

    private IGameManagerInternal _gm;

    public InGameUI(){
      _gm = GameManager.GetInternalInstance();
      _gameStats = _gm.GameStats;

      Vector2 pos = _gm.Configs.CommonConfigs.WindowSize;
      pos.x = 10;
      pos.y -= 15;
      Vector2 offset = new Vector2(0, 23); 

      _shipPos = new TextObject("", pos);
      _shipRotation = new TextObject("", pos -= offset);
      _shipSpeed = new TextObject("", pos -= offset);

      pos = _gm.Configs.CommonConfigs.WindowSize;
      pos.x /=2;
      pos.y -= 50;
      _score = new TextObject("", pos, TextAlign.CENTRE, true);

      _shipLaserState = new TextObject("", pos + new Vector2(270,0));

      _allElements = new IBasicObject[]{_shipPos, _shipRotation, _shipSpeed, _score, _shipLaserState};
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);

      _shipPos.Text =      $"pos:   ({_gameStats.ShipStats.Pos.x:000.}, {_gameStats.ShipStats.Pos.y:000.})";
      _shipRotation.Text = $"rot:   {_gameStats.ShipStats.Rotation:000.}";
      _shipSpeed.Text =    $"speed: ({_gameStats.ShipStats.Speed.x:000.}, {_gameStats.ShipStats.Speed.y:000.})";
      _score.Text = $"{_gameStats.Score}";

      _shipLaserState.Text = $"Laser x {_gameStats.ShipStats.LaserCapacity} ({_gameStats.ShipStats.LaserTimeOut:0.00})";
    }
  }
}