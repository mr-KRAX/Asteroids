using System.Collections.Generic;

namespace Asteroids {
  class InGameUI : IUI{
    private TextObject _shipPos;
    private TextObject _shipRotation;
    private TextObject _shipSpeed;
    private TextObject _score;
    // private ITextObject _shipLaserCapacity;
    // private ITextObject _shipLaserTimeOut;

    private IGameObject _shipObject;

    private IGameManagerInternal _gm;

    public InGameUI(){
      _gm = GameManager.GetInternalInstance();
      _shipObject = _gm.Ship;

      _shipPos = new TextObject("", new Vector2(10, 765));
      _shipRotation = new TextObject("", new Vector2(10, 750));
      _shipSpeed = new TextObject("", new Vector2(10, 735));
      _score = new TextObject("", new Vector2(500f, 750f), TextAlign.CENTRE);
    }

    public void Update(float deltaTime) {
      _shipPos.Text =      $"pos:   ({_shipObject.Transform.Pos.x:000.}, {_shipObject.Transform.Pos.y:000.})";
      _shipRotation.Text = $"rot:   {_shipObject.Transform.Rotation:000.}";
      _shipSpeed.Text =    $"speed: ({_shipObject.PhysicalComponent.Speed.x:000.}, {_shipObject.PhysicalComponent.Speed.y:000.})";
      _score.Text = $"{_gm.Score}";
    }

    public IEnumerable<ITextObject> GetTextUIObjects() {
      yield return _shipPos;
      yield return _shipRotation;
      yield return _shipSpeed;
      yield return _score;
    }
  }
}