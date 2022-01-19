using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_Hacks {
  public class CHLCPW : ICommand
  {
    public string Name { get { return "chlcpw"; } }
    public string Description { get { return "manipulate passwords and stuff lol"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();
    private string usersFile;

    public void Command() {
      usersFile = Path.Join(data.SavePath, "user", "Users.lcs");
      if (!File.Exists(usersFile)) {
        Console.WriteLine("for some reason, the users file at " + usersFile + " seems not to exist");
        return;
      }
      if (_InputProperties.Length < 2) {
        help();
        return;
      }
      switch (_InputProperties[1]) {
        case "help": help(); break;
        case "show": show(); break;
        case "edit": edit(); break;
        default: Console.WriteLine("unrecognized argument: " + _InputProperties[1]); help(); break;
      }
    }

    private void help() {
      Console.WriteLine("chlcpw - CHange LeoConsole PassWord");
      Console.WriteLine("see and change LeoConsole user details (inspired by chntpw)");
      Console.WriteLine("");
      Console.WriteLine("arguments:");
      Console.WriteLine("  help:                            show this help");
      Console.WriteLine("  show:                            list user details");
      Console.WriteLine("  edit <username> <field> <value>: change user details");
    }

    private List<User> getUsersList() {
      try {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(usersFile, FileMode.Open);
        List<User> users = formatter.Deserialize(stream) as List<User>;
        stream.Close();
        return users;
      } catch (Exception e) {
        Console.WriteLine("cannot read users list: " + e.Message);
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
      if (_InputProperties.Length < 4) {
        Console.WriteLine("not enough arguments");
        return;
      }

      List<User> users = getUsersList();
      if (users.Count == 0) {
        return;
      }

      Console.WriteLine("Working with user " + _InputProperties[2]);
      for (int i = 0; i <= users.Count - 1; i++) {
        if (users[i].name != _InputProperties[2]) {
          continue;
        }
        switch (_InputProperties[3]) {
          case "name":
            Console.WriteLine("changing username from " + users[i].name  + " to " + _InputProperties[4]);
            users[i].name = _InputProperties[4];
            break;
          case "password":
            Console.WriteLine("changing password to " + _InputProperties[4]);
            users[i].password = _InputProperties[4];
            break;
          case "greeting":
            Console.WriteLine("changing greeting from '" + users[i].begrüßung  + "' to '" + _InputProperties[4] + "'");
            users[i].begrüßung = _InputProperties[4];
            break;
          case "root":
            if (_InputProperties[4] == "yes") {
              Console.WriteLine("setting user to root");
              users[i].root = true;
            } else {
              Console.WriteLine("setting user to non-root");
              users[i].root = false;
            }
            break;
          default:
            Console.WriteLine("invalid field: " + _InputProperties[3]);
            return;
            break;
        }
      }

      Console.WriteLine("saving new user table");
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(usersFile, FileMode.Create);
      formatter.Serialize(stream, users);
      stream.Close();
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
