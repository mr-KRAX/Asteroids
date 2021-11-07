using System;
using System.Collections.Generic;

namespace Asteroids {
  class EnemySpawner : Module, IEnemySpawner, IEnemySpawnerStats {
    private uint _asteroidsCount;
    private uint _uFOsCount;
    private uint _debrisCount;
    private uint _totalCount => _asteroidsCount + _uFOsCount + _debrisCount;
    private float _spawnTimeOut;
    private List<Vector2> _spawnPositions;
    private Random _rnd;
    private EnemyCreator[] _ufoCreators;
    private EnemyCreator[] _debrisCreators;
    private EnemyCreator[] _asteroidCreators;

    private IGameManagerInternal _gm;
    private IEnemiesConfigs _configs;
    public EnemySpawner() {
      _spawnTimeOut = 0f;
      _asteroidsCount = 0;
      _uFOsCount = 0;
      _debrisCount = 0;

      _gm = GameManager.GetInternalInstance();
      _configs = _gm.Configs.EnemiesConfigs;

      _rnd = new Random();
      _spawnPositions = new List<Vector2>();

      _ufoCreators = new EnemyCreator[] { new UFOCreator() };
      _debrisCreators = new EnemyCreator[] { new DebrisCreator() };
      _asteroidCreators = new EnemyCreator[] { new AsteroidCreator1(), new AsteroidCreator2() };

      GenerateSpawnPositions();
    }

    public override void Update(float deltaTime) {
      base.Update(deltaTime);

      _spawnTimeOut -= deltaTime;
      if (_spawnTimeOut > 0f || _totalCount > 10f)
        return;
      if (!(_asteroidsCount < 2 + _gm.GameStats.Level / 2f ||
             _uFOsCount < _gm.GameStats.Level / 2f))
        return;

      if (_asteroidsCount < 2 + _gm.GameStats.Level)
        SpawnAsteroid();
      if (_uFOsCount < _gm.GameStats.Level / 2f)
        SpawnUFO();
      _spawnTimeOut = 2f;
    }

    private void SpawnAsteroid() {
      var cr = _asteroidCreators[_rnd.Next() % _asteroidCreators.Length];
      var pos = GeneratePos();
      var rotation = GenerateAngle();
      var speed = GenerateSpeed(_configs.AsteroidMaxSpeed);
      var asteroid = cr.CreateEnemy(this, pos, rotation, speed);

      _gm.SpawnGameObject(asteroid);
      _asteroidsCount++;
    }

    private void SpawnUFO() {
      var cr = _ufoCreators[_rnd.Next() % _ufoCreators.Length];
      var pos = GeneratePos();
      var uFO = cr.CreateEnemy(this, pos, 0, Vector2.zero);

      _gm.SpawnGameObject(uFO);
      _uFOsCount++;
    }

    private void SpawnDebris(IGameObject ancestor) {
      Vector2 aSpeed = ancestor.PhysicalComponent.Speed;
      Vector2 aPos = ancestor.Transform.Pos;
      int n = 1 + _rnd.Next() % 3;
      for (int i = 0; i < n; i++) {
        var cr = _debrisCreators[_rnd.Next() % _debrisCreators.Length];
        var rot = GenerateAngle();
        var speed = GenerateSpeed(aSpeed.Dir.RotateOnDegrees(-25 + GenerateAngle(50), Vector2.zero),
                                  _configs.DebrisMaxSpeed);
        var debris = cr.CreateEnemy(this, aPos, rot, speed);
        _gm.SpawnGameObject(debris);
        _debrisCount++;
      }
    }

    private Vector2 GeneratePos() {
      return _spawnPositions[_rnd.Next() % _spawnPositions.Count];
    }

    private float GenerateAngle(float max = 360) {
      return _rnd.Next() % max;
    }

    private Vector2 GenerateSpeed(float maxSpeed) {
      Vector2 dir = Vector2.one.RotateOnDegrees(GenerateAngle(), Vector2.zero);
      return GenerateSpeed(dir, maxSpeed);
    }
    private Vector2 GenerateSpeed(Vector2 dir, float maxSpeed) {
      float speed = maxSpeed * (6 + _rnd.Next() % 5) / 10f;
      return (dir * speed);
    }

    private void GenerateSpawnPositions() {
      Vector2 winSize = _gm.Configs.CommonConfigs.WindowSize;
      _spawnPositions.Clear();
      _spawnPositions.Add(new Vector2(0, 0));
      int N = 5;
      for (int i = 1; i < N; i++) {
        _spawnPositions.Add(new Vector2(0, winSize.y / N * i));
        _spawnPositions.Add(new Vector2(winSize.x / N * i, 0));
      }
    }

    public void EnemyHit(IGameObject enemy) {
      if (enemy.GetType() == typeof(Asteroid))
        SpawnDebris(enemy);
      _gm.DestroyGameObject(enemy);
      _gm.NotifyEnemyDead();
    }

    public void EnemyDestroyed(IGameObject enemy) {
      if (enemy.GetType() == typeof(Asteroid))
        _asteroidsCount--;
      else if (enemy.GetType() == typeof(UFO))
        _uFOsCount--;
      else if (enemy.GetType() == typeof(Debris))
        _debrisCount--;
    }

    #region Stats
    public uint AsteroidCount => _asteroidsCount;
    public uint UFOsCount => _uFOsCount;
    public uint DebrisCount => _debrisCount;

    #endregion Stats
  }
}