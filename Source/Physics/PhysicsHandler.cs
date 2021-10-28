using System;
using System.Collections.Generic;

namespace Asteroids {
  class PhysicsHandler : IPhysicsHandler {
    List<IPhysicalComponent> _objects = new List<IPhysicalComponent>();
    private Vector2 _positionLimits;
    private float _dragForce = 100f;

    public PhysicsHandler(Vector2 posLimits, float dragForce = 100f){
      _positionLimits = posLimits;
      _dragForce = dragForce;
    }

    public float DragForce => _dragForce;

    public void LinkPhysicalComponent(IPhysicalComponent pc){
      _objects.Add(pc);
    }

    public void Update(float deltatTime) {
      foreach(var obj in _objects)
        obj.UpdateState(deltatTime);
    }
    public Vector2 GetPositionLimits() {
      return _positionLimits;
    }
  }
}