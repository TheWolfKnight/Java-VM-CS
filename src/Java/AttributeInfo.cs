using CS_Java_VM.Src.Java.Constants;

using System;

namespace CS_Java_VM.Src.Java;

#region Interface
public interface IAttributeInfo {}

#endregion

#region Implementations

public class ConstantValueAttribute: IAttributeInfo {
  public UInt16 AttributeNameIndex;
  public UInt16 ConstantValueIndex;
  public UInt32 AttributeLength;

  public ConstantValueAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 constantValueIndex) {
    AttributeNameIndex = attributeNameIndex;
    AttributeLength = attributeLength;
    ConstantValueIndex = constantValueIndex;
  }
}

public class CodeAttribute : IAttributeInfo {
  private int[] Pointers = new int[3] { 0, 0, 0 };

  public UInt16 AttributeNameIndex;
  public UInt16 MaxStack, MaxLocals;
  public UInt16 ExceptionTableLength;
  public UInt16 AttributesCount;
  public UInt32 AttributeLength;
  public UInt32 CodeLength;
  public byte[] Code;
  public ExceptionEntry[] ExceptionTable;
  public IAttributeInfo[] Attributes;

  public CodeAttribute(
    UInt16 attributeNameIndex,
    UInt32 attributeLength,
    UInt16 maxStack,
    UInt16 maxLocals,
    UInt32 codeLength,
    UInt16 exceptionTableLength,
    UInt16 attributesCount
  ) {
    AttributeNameIndex = attributeNameIndex;
    AttributeLength = attributeLength;
    MaxStack = maxStack;
    MaxLocals = maxLocals;
    CodeLength = codeLength;
    ExceptionTableLength = exceptionTableLength;
    AttributesCount = attributesCount;

    Code = new byte[codeLength-1];
    ExceptionTable = new ExceptionEntry[ExceptionTableLength-1];
    Attributes = new IAttributeInfo[AttributesCount-1];
  }

  public void AddAttributeToAttributeArray(IAttributeInfo attribute) {
    if (Pointers[0] == AttributeLength)
      throw new IndexOutOfRangeException("Could not push the attribute to the Attributes array.");

      Attributes[Pointers[0]] = attribute;
      Pointers[0]++;
  }

  public void AddExceptionEntryToExceptionTableArray(ExceptionEntry exceptionEntry) {
    if (Pointers[1] == ExceptionTableLength)
      throw new IndexOutOfRangeException("Could not push the attribute to the ExceptionTable array.");

      ExceptionTable[Pointers[1]] = exceptionEntry;
      Pointers[1]++;
  }

  public void AddByteToCodeArray(byte code) {
    if (Pointers[2] == CodeLength)
      throw new IndexOutOfRangeException("Could not push the attribute to the Code array.");

    Code[Pointers[2]] = code;
    Pointers[2]++;
  }
}

public class StackMapTableAttribute: IAttributeInfo {
  public UInt16 AttributeNameIndex;
  public UInt32 AttributeLength;
  public UInt16 NumberOfEntrys;

}

#endregion

#region Structures

public struct ExceptionEntry {
  public UInt16 StartPc, EndPc;
  public UInt16 HandlerPc;
  public UInt16 CatchType;

  public ExceptionEntry(UInt16 startPc, UInt16 endPc, UInt16 handlerPc, UInt16 catchType) {
    StartPc = startPc;
    EndPc = endPc;
    HandlerPc = handlerPc;
    CatchType = catchType;
  }
}

#endregion
