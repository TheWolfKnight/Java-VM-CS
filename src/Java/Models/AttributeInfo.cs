using CS_Java_VM.Src.Java.Constants;
using CS_Java_VM.Src.Java.Union;

using System;

namespace CS_Java_VM.Src.Java.Models;

#region Interface
public interface IAttributeInfo {}

#endregion

#region Generic Implementation

public class AttributeGeneric: IAttributeInfo {
  public UInt16 AttributeNameIndex;
  public UInt32 AttributeLength;

  public AttributeGeneric(UInt16 attributeNameIndex, UInt32 attributeLength) {
    AttributeNameIndex = attributeNameIndex;
    AttributeLength = attributeLength;
  }
}

#endregion

#region Implementations

public class ConstantValueAttribute: AttributeGeneric, IAttributeInfo {
  public UInt16 ConstantValueIndex;

  public ConstantValueAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 constantValueIndex) : base(attributeNameIndex, attributeLength) {
    ConstantValueIndex = constantValueIndex;
  }
}

public class CodeAttribute: AttributeGeneric, IAttributeInfo {
  private int[] Pointers = new int[3] { 0, 0, 0 };

  public UInt16 MaxStack, MaxLocals;
  public UInt16 ExceptionTableLength;
  public UInt16 AttributesCount;
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
  ) : base(attributeNameIndex, attributeLength) {
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

public class StackMapTableAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0; 

  public UInt16 NumberOfEntrys;
  public IStackMapFrameUnion[] Entries;

  public StackMapTableAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 numberOfEntrys) : base(attributeNameIndex, attributeLength) {
    NumberOfEntrys = numberOfEntrys;

    Entries = new IStackMapFrameUnion[numberOfEntrys];
  }

  public void AddToEntrysArray(IStackMapFrameUnion entry) {
    if (ArrayPointer == NumberOfEntrys)
      throw new Exception("Could not add the entry to entryes, out of bounds");

    Entries[ArrayPointer] = entry;
    ArrayPointer++;
  }
}

public class ExceptionAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0;

  public UInt16 NumberOFExceptions;
  public UInt16[] ExceptionIndexTable;

  public ExceptionAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 numberOfExceptions) : base(attributeNameIndex, attributeLength) {
    NumberOFExceptions = numberOfExceptions;

    ExceptionIndexTable = new UInt16[numberOfExceptions];
  }

  public void AddExceptionsIndexToArray(UInt16 index) {
    if (ArrayPointer == NumberOFExceptions)
      throw new Exception("Could not add the entry to entryes, out of bounds");

    ExceptionIndexTable[ArrayPointer] = index;
    ArrayPointer++;
  }
}

public class InnerClassesAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0;

  public UInt16 NumberOfClasses;
  public InnerClass[] Classes;

  public InnerClassesAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 numberOfClasses) : base(attributeNameIndex, attributeLength) {
    NumberOfClasses = numberOfClasses;

    Classes = new InnerClass[numberOfClasses];
  }

  public void AddInnerClassToArray(UInt16 innerClassInfoIndex, UInt16 outerClassInfoIndex, UInt16 innereNameIndex, UInt16 outerNameIndex) {
    if (ArrayPointer == NumberOfClasses)
      throw new Exception("Could not add the entry to entryes, out of bounds");

    Classes[ArrayPointer] = new InnerClass(innerClassInfoIndex, outerClassInfoIndex, innereNameIndex, outerNameIndex);
    ArrayPointer++;
  }
}

public class EnclosingMethodAttribute: AttributeGeneric, IAttributeInfo {
  public UInt16 ClassIndex;
  public UInt16 MehtodIndex;

  public EnclosingMethodAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 classIndex, UInt16 methodIndex) : base(attributeNameIndex, attributeLength) {
    ClassIndex = classIndex;
    MehtodIndex = methodIndex;
  }
}

public class SyntheticAttribute: AttributeGeneric, IAttributeInfo {
  public SyntheticAttribute(UInt16 attributeNameIndex, UInt32 attributeLength) : base(attributeNameIndex, attributeLength) {}
}

public class SignatureAttribute: AttributeGeneric, IAttributeInfo {
  public UInt16 SignatureIndex;

  public SignatureAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 signatureIndex) : base(attributeNameIndex, attributeLength) {
    SignatureIndex = signatureIndex;
  }
}

public class SourceFileAttribute: AttributeGeneric, IAttributeInfo {
  public UInt16 SourceFileIndex;

  public SourceFileAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 sourceFileIndex) : base(attributeNameIndex, attributeLength) {
    SourceFileIndex = sourceFileIndex;
  }
}

public class SourceDebugExtensionAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0;
  public byte[] DebugExtension;

  public SourceDebugExtensionAttribute(UInt16 attributeNameIndex, UInt32 attributeLength) : base(attributeNameIndex, attributeLength) {
    DebugExtension = new byte[attributeLength];
  }

  public void AddDebugExtensionToArray(byte extension) {
    if (ArrayPointer == AttributeLength)
      throw new Exception("Could not add the entry to entryes, out of bounds");

    DebugExtension[ArrayPointer] = extension;
    ArrayPointer++;
  }
}

public class LineNumberTableAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0;

  public UInt16 LineNumberTableLength;
  public LineNumber[] LineNumberTable;

  public LineNumberTableAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 lineNumberTableLength) : base(attributeNameIndex, attributeLength) {
    LineNumberTableLength = lineNumberTableLength;

    LineNumberTable = new LineNumber[lineNumberTableLength];
  }

  public void AddLineNumberToArray(UInt16 startPc, UInt16 lineNumber) {
    if (ArrayPointer == LineNumberTableLength)
      throw new Exception("Index out of bounds for the array");

    LineNumberTable[ArrayPointer] = new LineNumber(startPc, lineNumber);
    ArrayPointer++;
  }
}

public class LocalVariableTableAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0;

  public UInt16 LocalVariableTableLength;
  public LocalVariable[] LocalVariableTable;

  public LocalVariableTableAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 localVariableTableLength) : base(attributeNameIndex, attributeLength) {
    LocalVariableTableLength = localVariableTableLength;

    LocalVariableTable = new LocalVariable[localVariableTableLength];
  }

  public void AddLocalVariableToArray(UInt16 startPc, UInt16 length, UInt16 nameIndex, UInt16 descriptorIndex, UInt16 index) {
    if (ArrayPointer == LocalVariableTableLength)
      throw new Exception("Could not push to array as the indexer overflowed");

    LocalVariableTable[ArrayPointer] = new LocalVariable(startPc, length, nameIndex, descriptorIndex, index);
    ArrayPointer++;
  }
}

public class LocalVariableTypeTableAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0;

  public UInt16 LocalVariableTypeTableLength;
  public LocalVariable[] LocalVariableTypeTable;

  public LocalVariableTypeTableAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 localVariableTableAttribute) : base(attributeNameIndex, attributeLength) {
    LocalVariableTypeTableLength = localVariableTableAttribute;

    LocalVariableTypeTable = new LocalVariable[LocalVariableTypeTableLength];
  }

  public void AddLocaleVariabelTypeToArray(UInt16 startPc, UInt16 length, UInt16 nameIndex, UInt16 signatureIndex, UInt16 index) {
    if (ArrayPointer == LocalVariableTypeTableLength)
      throw new Exception("Could not push to array as the indexer overflowed");

    LocalVariableTypeTable[ArrayPointer] = new LocalVariable(startPc, length, nameIndex, signatureIndex, index);
    ArrayPointer++;
  }
}

public class DeprecatedAttribute: AttributeGeneric, IAttributeInfo {
  public DeprecatedAttribute(UInt16 attributeNameIndex, UInt32 attributeLength) : base(attributeNameIndex, attributeLength) {}
}

public class RuntimeVisibleAnnotationsAttribute: AttributeGeneric, IAttributeInfo {
  private int ArrayPointer = 0;

  public UInt16 NumAnnotations;
  public Annotation[] Annotations;

  public RuntimeVisibleAnnotationsAttribute(UInt16 attributeNameIndex, UInt32 attributeLength, UInt16 numAnnotations) : base(attributeNameIndex, attributeLength) {
    NumAnnotations = numAnnotations;

    Annotations = new Annotation[numAnnotations];
  }

  public void AddAnnotationToArray(Annotation annotation) {
    if (ArrayPointer == NumAnnotations)
      throw new Exception("Could not push to array as the indexer overflowed");

    Annotations[ArrayPointer] = annotation;
    ArrayPointer++;
  }
}



#endregion

#region Structures

public struct LocalVariable {
  public UInt16 StartPc;
  public UInt16 Length;
  public UInt16 NameIndex;
  public UInt16 DescriptorIndex;
  public UInt16 Index;
  
  public LocalVariable(UInt16 startPc, UInt16 length, UInt16 nameIndex, UInt16 descriptorIndex, UInt16 index) {
    StartPc = startPc;
    Length = length;
    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
    Index = index;
  }
}

public struct LineNumber {
  public UInt16 StartPc;
  public UInt16 LN;

  public LineNumber(UInt16 startPc, UInt16 ln) {
    StartPc = startPc;
    LN = ln;
  }
}

public struct InnerClass {
  public UInt16 InnerClassInfoIndex, OuterClassInfoIndex;
  public UInt16 InnerNameIndex;
  /// <summary>
  /// The access level of the current class.<br/><i>NOTE:</i> this should later be turned into a E_JavaClassAccessFlags.
  /// </summary>
  public UInt16 InnerClassAccessFlags;

  public InnerClass(UInt16 innerClassInfoIndex, UInt16 outerClassInfoIndex, UInt16 innereNameIndex, UInt16 innerClassAccessFlags) {
    InnerClassInfoIndex = innerClassInfoIndex;
    OuterClassInfoIndex = outerClassInfoIndex;
    InnerNameIndex = innerClassInfoIndex;
    InnerClassAccessFlags = innerClassAccessFlags;
  }
}

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
