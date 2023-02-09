using CS_Java_VM.Src.Java.Constants;
using CS_Java_VM.Src.Java.Models;
using CS_Java_VM.Src.Java;

namespace CS_Java_VM.Src.Interpreter;

public class Interpreter {

  private string Origin;
  public KeyValuePair<string, JavaClass> RootFile;

  public Dictionary<string, JavaClass> ClassList;

  public Interpreter(string origin) {
    origin = origin.Replace("\\", "/").Replace("./", "");
    Origin = origin;
    RootFile = new KeyValuePair<string, JavaClass>(origin, new JavaClass(origin));
    ClassList = new Dictionary<string, JavaClass>();
  }

  public bool ComplieDependencies() {
    JavaClass rootFile = RootFile.Value;

    if (rootFile.ConstantPool == null)
      return true;

    ConstantPoolUtf8Info[] utf8Constants = rootFile.ConstantPool
      .Where(constant => constant.GetTag() == E_ConstantPoolTag.CONSTANT_UTF8)
      .Select(item => (ConstantPoolUtf8Info)item)
      .ToArray();

    string[] paths = utf8Constants.Where(item => {
        string info = item.GetStringRep();
        return IsFile(info) && info+".class" != Origin;
      })
      .Select(item => {
        string info = item.GetStringRep();
        return info.Substring(0, info.Length);
      })
      .ToArray();

    foreach (string path in paths) {
      if (path.Substring(0,4) == "java")
        continue;

      if (!ClassList.TryAdd(path, new JavaClass("./"+path+".class"))) {
        Console.WriteLine($"[ERROR] Could not compile the file at path: {"./"+path+".class"}");
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

  private bool IsFile(string path) {
    return File.Exists("./"+path+".class");
  }

  public override string ToString() {
    return $"Interpreter()";
  }
}
