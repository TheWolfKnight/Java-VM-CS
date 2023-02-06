
using CS_Java_VM.Src.Java;

namespace CS_Java_VM {
  public class Program {
    static void Main(string[] argv) {
      JavaClass java = new JavaClass(@".\test\Test.class");
      System.Console.WriteLine(java);
    }
  }
}
