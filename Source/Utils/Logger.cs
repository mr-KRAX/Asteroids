using System.Collections.Generic;
using System;

namespace Asteroids {
  public static class Logger{
    private static Dictionary<string, string> staticLog;

    static Logger(){
      staticLog = new Dictionary<string, string>();
      Console.WriteLine();
    }
    public static void Log(string msg) {
      Console.SetCursorPosition(0,Console.CursorTop-1);
      Console.WriteLine(msg);
      printStaticLog();
    }
    public static void LogStatic(string key, string msg) {
      staticLog[key] = msg;
      Console.SetCursorPosition(0,Console.CursorTop-1);
      printStaticLog();
    }

    public static void removeStatic(string key) {
      staticLog.Remove(key);
    }

    private static void printStaticLog(){
      string msg = "";

      bool first = true;
      foreach(var l in staticLog){
        if (first) first = false;
        else msg += ", ";
        msg += $"<{l.Key}> = {l.Value}";
      }

      Console.WriteLine(msg);
    }
  }
}