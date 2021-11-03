using System;
using System.Collections.Generic;

namespace Asteroids {
  class AsteroidsSpawner {
    private uint _asteroidsCount;
    private uint _uFOsCount;
    private float _spawnTimeOut;
    private List<Vector2> _spawnPoss;
    public AsteroidsSpawner() {
      _spawnTimeOut = 0f;
      _asteroidsCount = 0;
      _uFOsCount = 0;
      _spawnPoss = new List<Vector2>();

      GenerateSpawnPoss();
    }
    
    public void Update(float deltaTime) {
      _spawnTimeOut -= deltaTime;
      if (_spawnTimeOut > 0f)
        return;
      if (!(_asteroidsCount < 5f || _uFOsCount < 2f))
        return;

      if (_asteroidsCount < 5f)
        SpawnAsteroid();
      if (_uFOsCount < 2f)
        SpawnUFO();
      _spawnTimeOut = 2f;
    }

    private void SpawnAsteroid() {
      var a = new Asteroid(this);
      a.Transform.Pos = GeneratePos();
      a.PhysicalComponent.Speed = GenerateSpeed();
      GameManager.GetInternalInstance().SpawnGameObject(a);
      _asteroidsCount++;
    }

    private void SpawnUFO() {
      var uFO = new UFO(this);
      uFO.Transform.Pos = GeneratePos();
      GameManager.GetInternalInstance().SpawnGameObject(uFO);
      _uFOsCount++;
    }

    private Vector2 GeneratePos() {
      Random rnd = new Random();
      return _spawnPoss[rnd.Next() % _spawnPoss.Count];
    }

    private Vector2 GenerateSpeed() {
      Random rnd = new Random();
      float angle = rnd.Next() % 360;
      float speed = GameManager.GetInternalInstance().Configs.EnemiesConfigs.AsteroidMaxSpeed;
      speed *= (6 + rnd.Next() % 5) / 10f;

      return (new Vector2((float)Math.Sin(angle * (float)Math.PI / 180),
                          (float)Math.Cos(-angle * (float)Math.PI / 180))) * speed;
    }

    private void GenerateSpawnPoss() {
      _spawnPoss.Add(new Vector2(0, 0));
    }

    public void AsteroidDestroyed(Asteroid asteroid) {
      GameManager.GetInternalInstance().DestroyGameObject(asteroid);
      GameManager.GetInternalInstance().NotifyEnemyDead();
      _asteroidsCount--;
    }

    public void UFODestroyed(UFO uFO) {
      GameManager.GetInternalInstance().DestroyGameObject(uFO);
      GameManager.GetInternalInstance().NotifyEnemyDead();
      _uFOsCount--;
    }
  }
}