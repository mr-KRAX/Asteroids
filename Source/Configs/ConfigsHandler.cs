namespace Asteroids {
  class ConfigsStorage : IConfigsStorage {
    public ICommonConfigs CommonConfigs { get; private set; }
    public IShipConfigs ShipConfigs { get; private set; }
    public IEnemiesConfigs EnemiesConfigs { get; private set; }
    public IUIConfigs UIConfigs { get; private set; }
    public ConfigsStorage() {
      this.CommonConfigs = new CommonConfigs();
      this.ShipConfigs = new ShipConfigs();
      this.EnemiesConfigs = new EnemiesConfigs();
      this.UIConfigs = new UIConfigs();
    }
  }
}