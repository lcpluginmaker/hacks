using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_Hacks {
  public class Crash : ICommand {
    public string Name { get { return "crash"; } }
    public string Description { get { return "crash LeoConsole"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();
    private string usersFile;

    public void Command() {
      Environment.Exit(1);
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
