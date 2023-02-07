
using CS_Java_VM.Src.Validator.Type.Models;

using CS_Java_VM.Src.Java;

namespace CS_Java_VM.Src.Validator.Type;

public class TypeValidator {

  public Stack<VarType> CallStack;
  public List<Stack<VarType>> Heap;

  public TypeValidator(ref JavaClass jc) {
    CallStack = new Stack<VarType>();
    Heap = new List<Stack<VarType>>();
  }
}
