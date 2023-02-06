
using CS_Java_VM.Src.Interpreter;

namespace CS_Java_VM {
  public class Program {
    static void Main(string[] argv) {
      Interpreter interp = new Interpreter(@"./test/Test.class");
      System.Console.WriteLine(interp.RootFile);
      interp.ComplieDependencies();
    }
  }
}
