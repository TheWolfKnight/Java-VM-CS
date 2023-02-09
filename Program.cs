
using CS_Java_VM.Src.Interpreter;
using CS_Java_VM.Src.Java;

namespace CS_Java_VM {
  public class Program {
    static void Main(string[] argv) {
      Interpreter interp = new Interpreter(@"./test/Test.class");
      interp.ComplieDependencies();

      System.Console.Write("Root File: ");
      System.Console.WriteLine($"{interp.RootFile.Key}{Environment.NewLine}{interp.RootFile.Value}");

      foreach (KeyValuePair<string, JavaClass> clsFile in interp.ClassList.AsEnumerable()) {
        System.Console.WriteLine(clsFile.Key+":");
        System.Console.WriteLine(clsFile.Value);
      }

    }
  }
}
