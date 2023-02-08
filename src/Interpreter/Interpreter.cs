using CS_Java_VM.Src.Java.Constants;
using CS_Java_VM.Src.Java.Models;
using CS_Java_VM.Src.Java;

using System.Diagnostics;

namespace CS_Java_VM.Src.Interpreter;

public class Interpreter {

  private string RootPath;
  public JavaClass RootFile;

  public Dictionary<string, JavaClass> ClassList;

  public Interpreter(string origin) {
    origin.Replace("\\", "/");
    RootPath = origin.Substring(0, origin.LastIndexOf("/"));
    RootFile = new JavaClass(origin);
    ClassList = new Dictionary<string, JavaClass>();
  }

  public bool ComplieDependencies() {
    if (RootFile.ConstantPool == null)
      return true;

    ConstantPoolUtf8Info[] utf8Constants = RootFile.ConstantPool
        .Where(constant => constant.GetTag() == E_ConstantPoolTag.CONSTANT_UTF8)
        .Select(item => (ConstantPoolUtf8Info)item)
        .ToArray();

    ConstantPoolClass super = (ConstantPoolClass)RootFile.ConstantPool[RootFile.SuperClass-1];

    string superPath =
      ((ConstantPoolUtf8Info)RootFile.ConstantPool[super.NameIndex-1]).GetStringRepresentation();

    string[] paths = utf8Constants.Where(item => {
        string info = item.GetStringRepresentation();
        return info[0] == 'L' && info[^1] == ';';
      })
      .Select(item => {
        string info = item.GetStringRepresentation();
        return info.Substring(1, info.Length-2);
      })
      .Append(superPath)
      .ToArray();

  foreach (string path in paths) {
    if (path.Substring(0,4) == "java")
      continue;
      if (!ClassList.TryAdd(path, new JavaClass("./"+path+".class"))) {
        System.Console.WriteLine($"[ERROR] Could not compile the file at path: {"./"+path+".class"}");
        return false;
      }
    }
    return true;
  }

  public bool ValideateClassTyping() {
    throw new NotImplementedException();
  }

  public bool Run() {
    throw new NotImplementedException();
  }

  public override string ToString()
  {
    return $"Interpreter()";
  }
}
