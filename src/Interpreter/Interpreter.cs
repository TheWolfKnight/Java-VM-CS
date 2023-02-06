using CS_Java_VM.Src.Java.Constants;
using CS_Java_VM.Src.Java.Models;
using CS_Java_VM.Src.Java;

namespace CS_Java_VM.Src.Interpreter;

public class Interpreter {

  private string RootPath;
  public JavaClass RootFile;

  public List<JavaClass> ClassList;

  public Interpreter(string origin) {
    origin.Replace("\\", "/");
    RootPath = origin.Substring(0, origin.LastIndexOf("/"));
    System.Console.WriteLine(RootPath);
    Environment.Exit(1);
    RootFile = new JavaClass(origin);
    ClassList = new List<JavaClass>();
  }

  public void ComplieDependencies() {
    if (RootFile.ConstantPool == null)
      return;

    ConstantPoolUtf8Info[] utf8Constants = RootFile.ConstantPool
        .Where(constant => constant.GetTag() == E_ConstantPoolTag.CONSTANT_UTF8)
        .Select(item => (ConstantPoolUtf8Info)item)
        .ToArray();

    string[] paths = utf8Constants.Where(item => {
      string info = item.GetStringRepresentation();
      return info[0] == 'L' && info[^1] == ';';
    })
    .Select(item => {
      string info = item.GetStringRepresentation();
      return info.Substring(1, info.Length-2);
    })
    .ToArray();



  }

  public override string ToString()
  {
    return $"Interpreter()";
  }
}
