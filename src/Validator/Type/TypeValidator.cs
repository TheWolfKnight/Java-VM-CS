
using CS_Java_VM.Src.Validator.Type.Services;
using CS_Java_VM.Src.Validator.Type.Models;

using CS_Java_VM.Src.Maths.Convertor;


using CS_Java_VM.Src.Java.Constants;
using CS_Java_VM.Src.Java.Models;
using CS_Java_VM.Src.Java;

using CS_Java_VM.Src.Exceptions;

namespace CS_Java_VM.Src.Validator.Type;

public class TypeValidator {

  public Stack<VarType> CallStack;
  public Stack<VarType> FieldStack;
  public List<Stack<VarType>> Heap;

  private JavaClass JC;

  /// <summary>
  /// Generates a instance of the TypeValidator
  /// </summary>
  /// <param name="jc"> A reference to an instance of a JavaClass </param>
  public TypeValidator(JavaClass jc) {
    CallStack = new Stack<VarType>();
    Heap = new List<Stack<VarType>>();
    FieldStack = new Stack<VarType>();

    JC = jc;

    if (jc.Fields != null)
      GenerateFieldStack(ref jc.Fields);

    return;

    if (jc.Methods != null) {
      throw new NotImplementedException();
    }

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="">  </param>
  private void GenerateFieldStack(ref FieldsInfo[] fields) {
    foreach (FieldsInfo info in fields) {
      IConstantPool fieldName  = JC.ConstantPool[info.NameIndex-1],
                    descriptor = JC.ConstantPool[info.DescriptorIndex-1];

      if (fieldName.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8 ||
          descriptor.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8)
        throw new InvalidDataException("A FieldsInfo descriptor or name does not point to a UTF-8 constant");

      ConstantPoolUtf8Info cstFieldName  = (ConstantPoolUtf8Info)fieldName,
                           cstDescriptor = (ConstantPoolUtf8Info)descriptor;

      foreach (AttributeInfo attribute in info.Attributes) {
        if (IsGenreicTyped(attribute, out UInt16? res)) {
          if (res == null) throw new UnrechableCodeException();
          ConstantPoolUtf8Info genericConstant = (ConstantPoolUtf8Info)JC.ConstantPool[(int)res-1];
          cstDescriptor = genericConstant;
          break;
        }
      }

      VarType result =
        TypeCheckService.GetType(cstDescriptor.GetStringRep(), cstFieldName.GetStringRep());

      FieldStack.Push(result);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="">  </param>
  /// <param name="">  </param>
  /// <returns>  </returns>
  private bool IsGenreicTyped(AttributeInfo info, out UInt16? result) {
    // look at https://docs.oracle.com/javase/specs/jvms/se7/html/jvms-4.html#jvms-4.7.9
    // where the Signature Attribute is defined

    // Make sure the attribute points to a utf-8 constant
    IConstantPool attributeName = JC.ConstantPool[info.AttributeNameIndex-1];
    if (attributeName.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8) {
      result = null;
      return false;
    }

    // Make sure the utf-8 constant says Signature
    ConstantPoolUtf8Info signitureUTF8 = (ConstantPoolUtf8Info)attributeName;
    if (signitureUTF8.GetStringRep() != "Signature") {
      result = null;
      return false;
    }

    // Take the two bytes from the attributes info field, and assign them to
    // the result parameter.
    UInt16 res = Convertor.BytesToUInt16(info.Info.Take(2));
    result = res;

    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  private void GenerateMethodStack() {
    throw new NotImplementedException();
  }

}
