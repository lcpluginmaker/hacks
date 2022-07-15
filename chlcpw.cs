using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_Hacks {
  public class CHLCPW : ICommand {
    public string Name { get { return "chlcpw"; } }
    public string Description { get { return "manipulate passwords and stuff lol"; } }
    public Action CommandFunction { get { return () => Command(); } }
    public Action HelpFunction { get { return () => help(); } }
    private string[] _Arguments;
    public string[] Arguments { get { return _Arguments; } set { _Arguments = value; } }
    public IData data = new ConsoleData();
    private string usersFile;

    public void Command() {
      usersFile = Path.Join(data.SavePath, "user", "Users.lcs");
      if (!File.Exists(usersFile)) {
        LConsole.MessageErr0("for some reason, the users file at " + usersFile + " seems not to exist");
        return;
      }
      if (_Arguments.Length < 2) {
        help();
        return;
      }
      switch (_Arguments[1]) {
        case "help": help(); break;
        case "show": show(); break;
        case "edit": edit(); break;
        default: LConsole.MessageErr0("unrecognized argument: " + _Arguments[1]); help(); break;
      }
    }

    private void help() {
      Console.WriteLine(@"
chlcpw - CHange LeoConsole PassWord
see and change LeoConsole user details (inspired by chntpw)

arguments:
  help:                            show this help
  show:                            list user details
  edit <username> <field> <value>: change user details");
    }

    private List<User> getUsersList() {
      try {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(usersFile, FileMode.Open);
        List<User> users = formatter.Deserialize(stream) as List<User>;
        stream.Close();
        return users;
      } catch (Exception e) {
        LConsole.MessageErr0("cannot read users list: " + e.Message);
        return new List<User>();
      }
    }

    private void show() {
      List<User> users = getUsersList();
      if (users.Count == 0) {
        return;
      }

      for (int i = 0; i <= users.Count - 1; i++) {
        Console.WriteLine("User #" + i.ToString() + ":");
        Console.WriteLine("  Name:     '" + users[i].name + "'");
        Console.WriteLine("  Greeting: '" + users[i].begrüßung + "'");
        Console.WriteLine("  Password: '" + users[i].password + "'");
        if (users[i].root) {
          Console.WriteLine("  IsRoot:   yes");
        } else {
          Console.WriteLine("  IsRoot:   no");
        }
      }
    }

    private void edit() {
      if (_Arguments.Length < 4) {
        LConsole.MessageErr0("not enough arguments");
        return;
      }

      List<User> users = getUsersList();
      if (users.Count == 0) {
        return;
      }

      LConsole.MessageSuc0("Working with user " + _Arguments[2]);
      for (int i = 0; i <= users.Count - 1; i++) {
        if (users[i].name != _Arguments[2]) {
          continue;
        }
        switch (_Arguments[3]) {
          case "name":
            LConsole.MessageSuc1("changing username from " + users[i].name  + " to " + _Arguments[4]);
            users[i].name = _Arguments[4];
            break;
          case "password":
            LConsole.MessageSuc1("changing password to " + _Arguments[4]);
            users[i].password = _Arguments[4];
            break;
          case "greeting":
            LConsole.MessageSuc1("changing greeting from '" + users[i].begrüßung  + "' to '" + _Arguments[4] + "'");
            users[i].begrüßung = _Arguments[4];
            break;
          case "root":
            if (_Arguments[4] == "yes") {
              LConsole.MessageSuc1("setting user to root");
              users[i].root = true;
            } else {
              LConsole.MessageSuc1("setting user to non-root");
              users[i].root = false;
            }
            break;
          default:
            LConsole.MessageErr0("invalid field: " + _Arguments[3]);
            return;
            break;
        }
      }

      LConsole.MessageSuc0("saving new user table");
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(usersFile, FileMode.Create);
      formatter.Serialize(stream, users);
      stream.Close();
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
