using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_Hacks {
  public class Info : ICommand {
    public string Name { get { return "info"; } }
    public string Description { get { return "print some data about the running LeoConsole instance"; } }
    public Action CommandFunction { get { return () => Command(); } }
    public Action HelpFunction { get { return () => Console.WriteLine("print some data about running LC instance"); } }
    private string[] _Arguments;
    public string[] Arguments { get { return _Arguments; } set { _Arguments = value; } }
    public IData data = new ConsoleData();
    private string usersFile;

    public void Command() {
      Console.Write(data.Version);
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
