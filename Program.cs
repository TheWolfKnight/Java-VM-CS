
using CS_Java_VM.Src.Validator.Type.Services;
using CS_Java_VM.Src.Validator.Type.Models;
using CS_Java_VM.Src.Validator.Type;

using CS_Java_VM.Src.Interpreter;

using CS_Java_VM.Src.Java.Models;
using CS_Java_VM.Src.Java;

namespace CS_Java_VM {
  public class Program {
    static void Main(string[] argv) {
      Interpreter interp = new Interpreter(@"./test/Test.class");
      interp.ComplieDependencies();

      TypeValidator validator = new TypeValidator(interp.RootFile.Value);

      foreach (VarType t in validator.FieldStack) {
        System.Console.WriteLine(t);
      }

    }
  }
}
