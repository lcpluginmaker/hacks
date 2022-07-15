using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_Hacks {

  public class ConsoleData : IData {
    public static User _User;
    public User User { get { return _User; } set { _User = value; } }
    public static string _SavePath;
    public string SavePath { get { return _SavePath; } set { _SavePath = value; } }
    public static string _DownloadPath;
    public string DownloadPath { get { return _DownloadPath; } set { _DownloadPath = value; } }
    public static string _Version;
    public string Version { get { return _Version; } set { _Version = value; } }
    public static string _CurrentWorkingPath;
    public string CurrentWorkingPath { get { return _CurrentWorkingPath; } set { _CurrentWorkingPath = value; } }
  }
  
  public class LCHacks : IPlugin {
    public string Name { get { return "hacks"; } }
    public string Explanation { get { return "do stuff that never was intended"; } }
    
    private IData _data;
    public IData data { get { return _data; } set { _data = value; } }
    
    private List<ICommand> _Commands;
    public List<ICommand> Commands { get { return _Commands; } set { _Commands = value; } }
    
    public void PluginInit() {
      _data = new ConsoleData();
      _Commands = new List<ICommand>();
    }

    public void RegisterCommands(){
      _Commands.Add(new CHLCPW());
      _Commands.Add(new Crash());
      _Commands.Add(new Info());
    }

    public void PluginMain() { }
    public void PluginShutdown() { }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
