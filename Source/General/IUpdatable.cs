namespace Asteroids {
  public interface IUpdatable{
    void Update(float deltaTime);
    bool IsActive { get; set; }
  }
}