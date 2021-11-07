namespace Asteroids {
  abstract class EnemyCreator {
    public abstract Enemy CreateEnemy(IEnemySpawner spawner, Vector2 pos, float rotation, Vector2 speed);
  }

  class AsteroidCreator1 : EnemyCreator {
    public override Enemy CreateEnemy(IEnemySpawner spawner, Vector2 pos, float rotation, Vector2 speed) {
      Asteroid a = new Asteroid(spawner);
      a.Graphics.Texture = TextureID.ASTEROID1;
      Vector2[] polygon = {new Vector2(-51, -11), new Vector2(-51, 24),
                           new Vector2(-26, 48), new Vector2(23, 48),
                           new Vector2(51, 23), new Vector2(48, -25),
                           new Vector2(11, -48), new Vector2(-29, -48)};
      a.ColliderComponent.UpdateVertices(polygon);
      a.Transform.Pos = pos;
      a.Transform.Rotation = rotation;
      a.PhysicalComponent.Speed = speed;
      return a;
    }
  }

  class AsteroidCreator2 : EnemyCreator {
    public override Enemy CreateEnemy(IEnemySpawner spawner, Vector2 pos, float rotation, Vector2 speed) {
      Asteroid a = new Asteroid(spawner);
      a.Graphics.Texture = TextureID.ASTEROID2;
      Vector2[] polygon = {new Vector2(-41, -29), new Vector2(-45, 12),
                           new Vector2(-12, 51), new Vector2(21, 35),
                           new Vector2(27, 30), new Vector2(45, 20),
                           new Vector2(35, -28), new Vector2(-9, -51)};
      a.ColliderComponent.UpdateVertices(polygon);
      a.Transform.Pos = pos;
      a.Transform.Rotation = rotation;
      a.PhysicalComponent.Speed = speed;
      return a;
    }
  }

  class DebrisCreator : EnemyCreator {
    public override Enemy CreateEnemy(IEnemySpawner spawner, Vector2 pos, float rotation, Vector2 speed) {
      var d = new Debris(spawner);
      d.Graphics.Texture = TextureID.DEBRIS;
      Vector2[] polygon = {new Vector2(10, -19), new Vector2(19, -10),
                           new Vector2(19, 11), new Vector2(7, 19),
                           new Vector2(-9, 19), new Vector2(-19, -7),
                           new Vector2(-19, -4), new Vector2(-7, -19)};
      d.ColliderComponent.UpdateVertices(polygon);

      d.Transform.Pos = pos;
      d.Transform.Rotation = rotation;
      d.PhysicalComponent.Speed = speed;
      return d;
    }
  }

  class UFOCreator : EnemyCreator {
    public override Enemy CreateEnemy(IEnemySpawner spawner, Vector2 pos, float rotation, Vector2 speed) {
      UFO uFO = new UFO(spawner);
      uFO.Graphics.Texture = TextureID.UFO;
      Vector2[] polygon = {new Vector2(4, 16), new Vector2(12, 6),
                           new Vector2(23, -5), new Vector2(13, -16),
                           new Vector2(-13, -16), new Vector2(-23, -5),
                           new Vector2(-12, 6), new Vector2(-4, 16)};
      uFO.ColliderComponent.UpdateVertices(polygon);
      uFO.Transform.Pos = pos;
      return uFO;
    }
  }
}