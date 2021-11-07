using System;
using System.Collections.Generic;

namespace Asteroids {
  class PhysicsHandler : Module, IPhysicsHandler {
    private List<IPhysicalComponent> _objects = new List<IPhysicalComponent>();
    private ICommonConfigs _configs;
    public PhysicsHandler() {
      _configs = GameManager.GetInternalInstance().Configs.CommonConfigs;
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);

      foreach (var phc in _objects) {
        if (phc.Drag && phc.Speed.Magnitude != 0f) {
          if (phc.Speed.Magnitude > 0.9f)
            phc.ApplyForce(_configs.DragForce * phc.Speed.Dir * (-1));
          else
            phc.Speed = Vector2.zero;
        }
        phc.UpdateState(deltaTime);
      }
    }


    public void AddPhysicalComponent(IPhysicalComponent pc) {
      _objects.Add(pc);
    }
    public void RemovePhysicalComponent(IPhysicalComponent pc) {
      _objects.Remove(pc);
    }
  }
}