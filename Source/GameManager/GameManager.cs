using System.Collections;
using System.Collections.Generic;

//TODO: remove XNA dependency
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
  enum GameState {
    INITIAL,
    MENU,
    PLAY,
    PAUSE,
    EXIT
  }
  class GameManager : IGameManagerExternal, IGameManagerInternal {
    #region  Singletone
    private static GameManager _instance = null;

    private static GameManager GetInstance() {
      if (_instance == null)
        new GameManager(); // _instance initialized inside
      return _instance;
    }

    public static IGameManagerInternal GetInternalInstance() {
      return GetInstance();
    }

    public static IGameManagerExternal GetExternalInstance() {
      return GetInstance();
    }
    #endregion Singletone

    #region Modules
    private IConfigsStorage _configs;
    private IPhysicsHandler _physicsHandler;
    private ICollisionDetecter _collisionDetecter;
    private AsteroidsSpawner _asteroidSpawner;
    #endregion Modules

    #region Graphics
    private Dictionary<TextureID, object> _graphicsStorage;
    #endregion Graphics

    #region Objects
    private List<IGameObject> _gameObjects;
    private Ship _ship;
    private HashSet<IGameObject> _gameObjectsToDestroy;
    private HashSet<IGameObject> _gameObjectsToSpawn;
    #endregion Objects

    #region UI
    private IUI _activeUI;
    #endregion UI

    #region GameStates
    private GameState _state;
    private int _score;
    #endregion GameStates

    private GameManager() {
      _graphicsStorage = new Dictionary<TextureID, object>();
      _gameObjects = new List<IGameObject>();
      _gameObjectsToDestroy = new HashSet<IGameObject>();
      _gameObjectsToSpawn = new HashSet<IGameObject>();

      _configs = new ConfigsStorage();

      _instance = this;

      _physicsHandler = new PhysicsHandler();
      _collisionDetecter = new CollisionDetecter();
      _asteroidSpawner = new AsteroidsSpawner();

      _state = GameState.INITIAL;
    }

    private void ResetScene() {
      _ship.Transform.Pos = new Vector2(700, 400);
    }

    #region IGameObjectExternal
    public void SetTexture(TextureID type, object gfx) {
      _graphicsStorage[type] = gfx;
    }

    public void HandleInput(KeyboardState keyboardState) {
      switch (_state) {
        case GameState.PLAY: {
            if (keyboardState.IsKeyDown(Keys.Up))
              _ship.FlyForward();

            if (keyboardState.IsKeyDown(Keys.Left))
              _ship.TurnLeft();

            if (keyboardState.IsKeyDown(Keys.Right))
              _ship.TurnRight();

            if (keyboardState.IsKeyDown(Keys.Space))
              _ship.Shoot();
            if (keyboardState.IsKeyDown(Keys.Escape)) {
              InitializeStartMenu();
              _state = GameState.MENU;
            }
            break;
          }
        case GameState.MENU: {
            if (keyboardState.IsKeyDown(Keys.Enter)) {
              InitializeNewGame();
              _state = GameState.PLAY;
            }
            if (keyboardState.IsKeyDown(Keys.Q))
              _state = GameState.EXIT;
            break;
          }
        default:
          break;
      }
    }

    public bool Update(float deltaTime) {
      CheckAliveObjects();

      switch (_state) {
        case GameState.INITIAL:
          InitializeStartMenu();
          _state = GameState.MENU;
          break;
        case GameState.MENU:
          break;
        case GameState.PLAY:
          _physicsHandler.Update(deltaTime);
          _collisionDetecter.Update();
          _asteroidSpawner.Update(deltaTime);
          foreach (IGameObject go in _gameObjects)
            go.Update(deltaTime);
          break;
        case GameState.EXIT:
          return false;
        default:
          return false;
      }

      _activeUI.Update(deltaTime);
      return true;
    }

    public IEnumerable<ITextObject> TextObjects() {
      foreach (ITextObject to in _activeUI.GetTextUIObjects())
        yield return to;
    }

    public IEnumerable<IGraphicalObject> GraphicalObjects() {
      if (_state == GameState.PLAY)
        foreach (IGameObject go in ActiveObjects())
          yield return go as IGraphicalObject;
    }

    public Vector2 WindowSize => _configs.CommonConfigs.WindowSize;
    #endregion IGameManagerExternal

    private void CheckAliveObjects() {
      if (_gameObjectsToDestroy.Count != 0) {
        foreach (var go in _gameObjectsToDestroy) {
          _gameObjects.Remove(go);
          go.Destroy();
        }
        _gameObjectsToDestroy.Clear();
      }

      if (_gameObjectsToSpawn.Count != 0) {
        foreach (var go in _gameObjectsToSpawn) {
          _gameObjects.Add(go);
        }
        _gameObjectsToSpawn.Clear();
      }
    }

    private IEnumerable<IGameObject> ActiveObjects() {
      foreach (IGameObject go in _gameObjects)
        if (go.IsActive)
          yield return go as IGameObject;
    }

    #region Initializers
    private void InitializeNewGame() {
      _score = 0;
      _ship = new Ship();
      _ship.Transform.Pos = _configs.CommonConfigs.FieldSize / 2f;
      _gameObjects.Add(_ship);
      _activeUI = new InGameUI();
    }

    private void InitializeStartMenu() {
      _activeUI = new StartMenuUI();
    }
    #endregion Initializers

    #region IGameManagerInternal
    public IConfigsStorage Configs => _configs;
    public IPhysicsHandler PhysicsHandler => _physicsHandler;
    public ICollisionDetecter CollisionDetecter => _collisionDetecter;

    public IGameObject Ship => _ship;
    public int Score => _score;

    public void NotifyEnemyDead() {
      _score++;
    }

    public void DestroyGameObject(IGameObject gameObject) {
      _gameObjectsToDestroy.Add(gameObject);
    }

    public void SpawnGameObject(IGameObject gameObject) {
      _gameObjectsToSpawn.Add(gameObject);
    }

    public object GetTexture(TextureID id) {
      return _graphicsStorage[id];
    }
    #endregion IGameManagerInternal
  }
}