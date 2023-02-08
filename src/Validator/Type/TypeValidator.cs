
using CS_Java_VM.Src.Validator.Type.Models;

using CS_Java_VM.Src.Java;

namespace CS_Java_VM.Src.Validator.Type;

public class TypeValidator {

  public Stack<VarType> CallStack;
  public List<Stack<VarType>> Heap;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="">  </param>
  public TypeValidator(ref JavaClass jc) {
    CallStack = new Stack<VarType>();
    Heap = new List<Stack<VarType>>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="">  </param>
  public void PushToCallStack(VarType varType) {
    CallStack.Push(varType);
  }

  /// <summary>
  /// 
  /// </summary>
  public VarType PopFromCallStack() {
    return CallStack.Pop();
  }

  /// <summary>
  /// 
  /// </summary>
  public void PushScopeToHeap() {
    Heap.Append(new Stack<VarType>());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="">  </param>
  public void PushVarTypeToHeap(VarType varType) {
    Heap[^1].Push(varType);
  }

  /// <summary>
  /// 
  /// </summary>
  public Stack<VarType> PopScopeFromHeap() {
    Stack<VarType> result = Heap[^1];
    Heap.RemoveAt(Heap.Count-1);
    return result;
  }
}
