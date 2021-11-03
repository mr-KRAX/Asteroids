using System;
using System.Collections.Generic;

namespace Asteroids {
  class PhysicsHandler : IPhysicsHandler {
    List<IPhysicalComponent> _objects = new List<IPhysicalComponent>();
    ICommonConfigs _configs;
    public PhysicsHandler(){
      _configs = GameManager.GetInternalInstance().Configs.CommonConfigs;
    }

    public void Update(float deltatTime) {
      foreach(var phc in _objects){
        if(phc.Drag && phc.Speed.Magnitude != 0f ){
          if (phc.Speed.Magnitude > 0.1f)
            phc.ApplyForce(_configs.DragForce * phc.Speed.Dir * (-1));
          else
            phc.Speed = Vector2.zero;
        }
        phc.UpdateState(deltatTime);
        phc.Origin.Transform.Pos = correctPosition(phc.Origin.Transform.Pos);
      }
    }

    private Vector2 correctPosition(Vector2 pos) {
      return new Vector2(
        (pos.x % _configs.FieldSize.x + _configs.FieldSize.x) % _configs.FieldSize.x,
        (pos.y % _configs.FieldSize.y + _configs.FieldSize.y) % _configs.FieldSize.y
      );
    }

    public void AddPhysicalComponent(IPhysicalComponent pc){
      _objects.Add(pc);
    }
    public void RemovePhysicalComponent(IPhysicalComponent pc) {
      _objects.Remove(pc);
    }
  }
}