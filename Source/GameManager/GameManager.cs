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
    GAMEOVER,
    EXIT
  }
  class GameManager : IGameManagerExternal, IGameManagerInternal, IGameStats {
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
    private IEnemySpawner _enemySpawner;
    #endregion Modules

    #region Objects
    private List<IGameObject> _gameObjects;
    private Ship _ship;
    private HashSet<IGameObject> _gameObjectsToDestroy;
    private HashSet<IGameObject> _gameObjectsToSpawn;
    #endregion Objects

    #region UI
    private IUI _inGameUI;
    private IUI _pauseUI;
    private IUI _startMenuUI;
    private IUI _debugUI;
    private IUI _gameOverUI;
    private IEnumerable<IUI> _allUIs;
    #endregion UI

    #region GameStates
    private GameState _state;
    private int _score;
    private int _bestScore;
    #endregion GameStates

    private GameManager() {
      _gameObjects = new List<IGameObject>();
      _gameObjectsToDestroy = new HashSet<IGameObject>();
      _gameObjectsToSpawn = new HashSet<IGameObject>();

      _configs = new ConfigsStorage();

      _instance = this;

      _physicsHandler = new PhysicsHandler();
      _collisionDetecter = new CollisionDetecter();
      _enemySpawner = new EnemySpawner();

      _state = GameState.INITIAL;

      _inGameUI = new InGameUI();
      _pauseUI = new PauseUI();
      _startMenuUI = new StartMenuUI();
      _debugUI = new DebugUI();
      _gameOverUI = new GameOverUI();

      _allUIs = new IUI[] { _inGameUI, _pauseUI, _startMenuUI, _gameOverUI, _debugUI };

      foreach (var ui in _allUIs)
        ui.IsActive = false;
    }
    #region IGameObjectExternal
    public void HandleInput(KeyPressed keyPressed) {
      switch (_state) {
        case GameState.PLAY: {
            if (keyPressed(ControlKeys.THRUST))
              _ship.FlyForward();

            if (keyPressed(ControlKeys.LEFT))
              _ship.TurnLeft();

            if (keyPressed(ControlKeys.RIGHT))
              _ship.TurnRight();

            if (keyPressed(ControlKeys.SHOOT))
              _ship.Shoot();
            if (keyPressed(ControlKeys.LASER))
              _ship.Laser();

            if (keyPressed(ControlKeys.PAUSE)) {
              _pauseUI.IsActive = true;
              _state = GameState.PAUSE;
            }
            break;
          }
        case GameState.MENU: {
            if (keyPressed(ControlKeys.ENTER)) {
              InitializeNewGame();
              _gameOverUI.IsActive = false;
              _startMenuUI.IsActive = false;
              _inGameUI.IsActive = true;
              _state = GameState.PLAY;
            }
            break;
          }
        case GameState.PAUSE:
          if (keyPressed(ControlKeys.ENTER)) {
            _pauseUI.IsActive = false;
            _state = GameState.PLAY;
          }
          break;
        default:
          break;
      }

      if (keyPressed(ControlKeys.QUIT))
        _state = GameState.EXIT;
      if (keyPressed(ControlKeys.DEBUG))
        _debugUI.IsActive ^= true;

    }

    public bool Update(float deltaTime) {
      UpdateGameObjectsList();

      switch (_state) {
        case GameState.INITIAL:
          _state = GameState.MENU;
          _startMenuUI.IsActive = true;
          break;
        case GameState.MENU:
          break;
        case GameState.PLAY:
          break;
        case GameState.PAUSE:
          break;
        case GameState.GAMEOVER:
          _state = GameState.MENU;
          _inGameUI.IsActive = false;
          _gameOverUI.IsActive = true;
          break;
        case GameState.EXIT:
          return false;
        default:
          return false;
      }

      foreach (var el in UpdatableElements())
        el.Update(deltaTime);
      return true;
    }

    public IEnumerable<ObjectInfo> GetObjectsToDraw() {
      foreach (IGameObject go in ActiveGameObjects())
        yield return new ObjectInfo(go);

      foreach (IUI ui in ActiveUIs()) {
        foreach (ITextObject to in ui.GetActiveElements())
          if (to is ITextObject)
            yield return new ObjectInfo(to as ITextObject);
          else
            yield return new ObjectInfo(to as IBasicObject);
      }
    }

    public Vector2 WindowSize => _configs.CommonConfigs.WindowSize;
    #endregion IGameManagerExternal

    private void UpdateGameObjectsList() {
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

    public IEnumerable<IGameObject> ActiveGameObjects() {
      foreach (IGameObject go in _gameObjects)
        if (go.IsActive)
          yield return go as IGameObject;
    }

    private IEnumerable<IUI> ActiveUIs() {
      foreach (var ui in _allUIs)
        if (ui.IsActive)
          yield return ui;
    }

    private IEnumerable<IUpdatable> UpdatableElements() {
      switch (_state) {
        case GameState.INITIAL:
          break;
        case GameState.MENU:
          break;
        case GameState.PLAY: {
            yield return _collisionDetecter;
            yield return _physicsHandler;
            yield return _enemySpawner;
            foreach (var go in ActiveGameObjects())
              yield return go;
            break;
          }
        case GameState.PAUSE:
          break;
        case GameState.EXIT:
          break;
        default:
          break;
      }
      foreach (var ui in ActiveUIs())
        yield return ui;
    }

    #region Initializers
    private void InitializeNewGame() {
      foreach (var go in _gameObjects) DestroyGameObject(go);
      _score = 0;
      _ship = new Ship();
      _ship.Transform.Pos = _configs.CommonConfigs.FieldSize / 2f;
      _gameObjects.Add(_ship);
    }
    #endregion Initializers

    #region IGameStats
    public int Score => _score;
    public int BestScore => _bestScore;
    public int Level => _score / _configs.CommonConfigs.LevelStep;
    public IShipStats ShipStats => _ship;
    public IEnemySpawnerStats EnemySpawnerStats => _enemySpawner as IEnemySpawnerStats;
    #endregion IGameStats


    #region IGameManagerInternal
    public IConfigsStorage Configs => _configs;
    public IPhysicsHandler PhysicsHandler => _physicsHandler;
    public ICollisionDetecter CollisionDetecter => _collisionDetecter;
    public IGameStats GameStats => this;
    public IGameObject Ship => _ship;

    public void NotifyEnemyDead() {
      _score++;
      if (_score > _bestScore)
        _bestScore = _score;
    }

    public void NotifyShipCrashed() {
      _state = GameState.GAMEOVER;
    }

    public void DestroyGameObject(IGameObject gameObject) {
      _gameObjectsToDestroy.Add(gameObject);
    }

    public void SpawnGameObject(IGameObject gameObject) {
      _gameObjectsToSpawn.Add(gameObject);
    }
    #endregion IGameManagerInternal
  }
}