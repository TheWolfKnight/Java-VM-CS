
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
  public List<Stack<VarType>> Heap;

  public Stack<VarType> FieldStack;
  public Dictionary<string, VarType> MethodMap;

  private JavaClass JC;

  /// <summary>
  /// Generates a instance of the TypeValidator
  /// </summary>
  /// <param name="jc"> A reference to an instance of a JavaClass </param>
  public TypeValidator(JavaClass jc) {
    CallStack = new Stack<VarType>();
    Heap = new List<Stack<VarType>>();
    FieldStack = new Stack<VarType>();
    MethodMap = new Dictionary<string, VarType>();

    JC = jc;

    if (jc.Fields != null)
      GenerateFieldStack(ref jc.Fields);

    if (jc.Methods != null) {
      GenerateMethodMap(ref jc.Methods);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="">  </param>
  private void GenerateMethodMap(ref MethodInfo[] methods) {
    Span<MethodInfo> methodSpan = methods.AsSpan();
    for (int i = 0; i < methodSpan.Length; ++i) {
      MethodInfo info = methodSpan[i];
      IConstantPool methodName       = JC.ConstantPool[info.NameIndex-1],
                    methodDescriptor = JC.ConstantPool[info.DescriptorIndex-1];

      if (methodName.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8 ||
          methodDescriptor.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8)
        throw new InvalidDataException("A methods NameIndex and DescriptorIndex must point to a valid UTF-8 constant");

      ConstantPoolUtf8Info cstMethodName       = (ConstantPoolUtf8Info)methodName,
                           cstMethodDescriptor = (ConstantPoolUtf8Info)methodDescriptor;

      // Cast the attributes array to a span, for faster enumeration
      Span<AttributeInfo> Attributes = info.Attributes.AsSpan();
      for (int j = 0; j < Attributes.Length; ++j) {
        AttributeInfo attribute = Attributes[j];
        if(IsGenreicTyped(attribute, out UInt16? res)) {
          if (res == null) throw new UnrechableCodeException();
          ConstantPoolUtf8Info genericConstant = (ConstantPoolUtf8Info)JC.ConstantPool[(int)res-1];
          cstMethodDescriptor = genericConstant;
          break;
        }
      }

      VarType result =
        TypeCheckService.GetType(cstMethodDescriptor.GetStringRep(), cstMethodName.GetStringRep());

      MethodMap.Add(cstMethodName.GetStringRep(), result);
    }

  }

  /// <summary>
  /// Compiles the JC fields from a JavaClass instance
  /// </summary>
  /// <param name=""> A reference to the JavaClass instancs field array </param>
  private void GenerateFieldStack(ref FieldsInfo[] fields) {
    // loops all the fields in the file
    foreach (FieldsInfo info in fields) {
      // gets the fieldName and descriptor from the class file
      IConstantPool fieldName       = JC.ConstantPool[info.NameIndex-1],
                    fieldDescriptor = JC.ConstantPool[info.DescriptorIndex-1];

      // Checks if the field and descriptor both have the
      // E_ConstantPoolTag.CONSTANT_UTF8 tag.
      if (fieldName.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8 ||
          fieldDescriptor.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8)
        throw new InvalidDataException("A FieldsInfo descriptor or name does not point to a UTF-8 constant");

      // Cast both to ConstantPoolUtf8Info instances
      ConstantPoolUtf8Info cstFieldName  = (ConstantPoolUtf8Info)fieldName,
                           cstDescriptor = (ConstantPoolUtf8Info)fieldDescriptor;

      // Loops all, if any, attributes pressent
      foreach (AttributeInfo attribute in info.Attributes) {
        // if the attribute is found to be for a generic type, change the cstDescriptor
        // to the descriptor for the generic type.
        if (IsGenreicTyped(attribute, out UInt16? res)) {
          if (res == null) throw new UnrechableCodeException();
          ConstantPoolUtf8Info genericConstant = (ConstantPoolUtf8Info)JC.ConstantPool[(int)res-1];
          cstDescriptor = genericConstant;
          break;
        }
      }

      // set the result to be the return value for the TypeCheckService.GetType method
      VarType result =
        TypeCheckService.GetType(cstDescriptor.GetStringRep(), cstFieldName.GetStringRep());

      // Push the result to the FieldStack
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
}
