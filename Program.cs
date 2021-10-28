using System;

namespace AsteroidsGame {
  public static class Program {
    [STAThread]
    static void Main() {

      using (var game = new AsteroidsGame())
        game.Run();
    }
  }
}
