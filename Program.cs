
using CS_Java_VM.Src.Interpreter;
using CS_Java_VM.Src.Java;

namespace CS_Java_VM {
  public class Program {
    static void Main(string[] argv) {
      Interpreter interp = new Interpreter(@"./Ltest/Test.class");
      interp.ComplieDependencies();

      System.Console.WriteLine("Root File:");
      System.Console.WriteLine(interp.RootFile);

      foreach (KeyValuePair<string, JavaClass> clsFile in interp.ClassList.AsEnumerable()) {
        System.Console.WriteLine(clsFile.Key+":");
        System.Console.WriteLine(clsFile.Value);
      }

    }
  }
}
