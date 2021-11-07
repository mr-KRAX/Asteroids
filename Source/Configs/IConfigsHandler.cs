namespace Asteroids {
  public interface IConfigsStorage {
    ICommonConfigs CommonConfigs { get; }
    IShipConfigs ShipConfigs { get; }
    IEnemiesConfigs EnemiesConfigs { get; }
    IUIConfigs UIConfigs { get; }
  }
}