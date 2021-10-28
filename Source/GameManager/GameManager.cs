using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
  class GameManager {
    IPhysicsHandler physics;

    Dictionary<TextureID, object> graphics;

    List<IGameObject> gameObjects;
    Ship ship1;
    Ship ship2;

    List<Asteroid> asteroids;
    List<UFO> uFOs;
    UFO ufo;

    public GameManager(){
      graphics = new Dictionary<TextureID, object>();
      gameObjects = new List<IGameObject>();
      asteroids = new List<Asteroid>();
      uFOs = new List<UFO>();



      Vector2 positionLimits = new Vector2(1000, 800);
      physics = new PhysicsHandler(positionLimits);
      ship1 = new Ship(physics);
      ship2 = new Ship(physics);

      // ship2 = new Ship(physics);
      
      // ufo = new UFO(physics, ship1);
      asteroids.Add(new Asteroid(physics, new Vector2(150, 30)));
      asteroids.Add(new Asteroid(physics, new Vector2(300, -200)));

      uFOs.Add(new UFO(physics, ship1));
      uFOs.Add(new UFO(physics, ship2));

      IGameObject[] objs = {ship1, ship2};
      gameObjects.AddRange(objs);
      gameObjects.AddRange(asteroids); 
      gameObjects.AddRange(uFOs);

      ResetScene();
    }

    private void ResetScene(){
      ship1.Transform.Pos = new Vector2(700, 400);
      ship2.Transform.Pos = new Vector2(300, 400);
    } 

    public void SetTexture(TextureID type, object gfx){
      graphics[type] = gfx;
    }

    public void UpdateGraphics(){
      ship1.Graphics.UpdateTexture(graphics[TextureID.SHIP1]);
      ship2.Graphics.UpdateTexture(graphics[TextureID.SHIP1]);
      ship2.Graphics.SetColor(ColorRGB.Green);
      
      foreach(var ufo in uFOs)
        ufo.Graphics.UpdateTexture(graphics[TextureID.UFO]);
      foreach(var a in asteroids)
        a.Graphics.UpdateTexture(graphics[TextureID.ASTEROID1]);
    }

    public void HandleInput(KeyboardState keyboardState) {
      if(keyboardState.IsKeyDown(Keys.Up))
        ship1.FlyForward();
      
      if(keyboardState.IsKeyDown(Keys.Left))
        ship1.TurnLeft();

      if(keyboardState.IsKeyDown(Keys.Right))
        ship1.TurnRight();

      if(keyboardState.IsKeyDown(Keys.W))
        ship2.FlyForward();
      
      if(keyboardState.IsKeyDown(Keys.A))
        ship2.TurnLeft();

      if(keyboardState.IsKeyDown(Keys.D))
        ship2.TurnRight();

      
    }

    public void UpdatePhysics(float deltaTime) {
      physics.Update(deltaTime);
    }
    public void UpdateObjects(){
      foreach(IGameObject go in gameObjects)
        go.Update();
    }

    private IEnumerable ActiveObject(){
      foreach (IGameObject go in gameObjects) {
        if (go.IsActive)
          yield return go as IGameObject;
      }
    }

    public System.Collections.IEnumerable ActiveGraphicalObjects() {
      foreach (IGameObject go in ActiveObject()) {
        if (go.IsActive)
          yield return go as IGraphicalObject;
      }
    } 
  }
}