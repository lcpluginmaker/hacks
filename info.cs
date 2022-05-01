using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_Hacks {
  public class Info : ICommand {
    public string Name { get { return "info"; } }
    public string Description { get { return "print some data about the running LeoConsole instance"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();
    private string usersFile;

    public void Command() {
      Console.Write(data.Version);
      Environment.Exit(1);
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
