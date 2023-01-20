using CS_Java_VM.Src.Java.Models;

using System;

namespace CS_Java_VM.Src.Java.Union;

#region Interface Definition

public interface IElementValueUnion {}

#endregion

#region Implementations

public class EV_ConstValueIndex: IElementValueUnion {
  public UInt16 ConstValueIndex;

  public EV_ConstValueIndex(UInt16 constValueIndex) => ConstValueIndex = constValueIndex;
}

public class EV_EnumConstValue: IElementValueUnion {
  public UInt16 TypeNameIndex;
  public UInt16 ConstNameIndex;

  public EV_EnumConstValue(UInt16 typeNameIndex, UInt16 constNameIndex) {
    TypeNameIndex = typeNameIndex;
    ConstNameIndex = constNameIndex;
  }
}

public class EV_ClassInfoValue: IElementValueUnion {
  public UInt16 ClassInfoIndex;

  public EV_ClassInfoValue(UInt16 classInfoIndex) => ClassInfoIndex = classInfoIndex;
}

public class EV_AnnotationValue: IElementValueUnion {
  public Annotation AnnotationValue;

  public EV_AnnotationValue(Annotation annotationValue) => AnnotationValue = annotationValue;
}

public class EV_ArrayValue: IElementValueUnion {
  private int ArrayPointer = 0;

  public UInt16 NumValues;
  public ElementValue[] Values;

  public EV_ArrayValue(UInt16 numValues) {
    NumValues = numValues;

    Values = new ElementValue[numValues];
  }

  public void AddElementValueToArray(ElementValue element) {
    if (ArrayPointer == NumValues)
      throw new IndexOutOfRangeException();
  }
}

#endregion