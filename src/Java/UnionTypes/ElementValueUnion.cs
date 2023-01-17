
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
}

#endregion