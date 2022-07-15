using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_Hacks {
  public class Crash : ICommand {
    public string Name { get { return "crash"; } }
    public string Description { get { return "crash LeoConsole"; } }
    public Action CommandFunction { get { return () => Command(); } }
    public Action HelpFunction { get { return () => Console.WriteLine("HAHA self documenting code"); } }
    private string[] _Arguments;
    public string[] Arguments { get { return _Arguments; } set { _Arguments = value; } }
    public IData data = new ConsoleData();
    private string usersFile;

    public void Command() {
      Environment.Exit(1);
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
