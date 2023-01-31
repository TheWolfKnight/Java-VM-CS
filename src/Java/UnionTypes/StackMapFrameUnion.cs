
using CS_Java_VM.Src.Java.Constants;

namespace CS_Java_VM.Src.Java.Union;

#region Interface

public interface IStackMapFrameUnion {}

#endregion

#region BaseClassDefinition 

public class StackFrameBaseClass: IStackMapFrameUnion {
  public byte SpecificByte;
  public E_StackMapFrameTags FrameType;

  public StackFrameBaseClass(byte frameType, byte specificByte) {
    FrameType = (E_StackMapFrameTags)frameType;
    SpecificByte = specificByte;
  }

  public StackFrameBaseClass(E_StackMapFrameTags frameType, byte specificByte) {
    FrameType = frameType;
    SpecificByte = specificByte;
  }
}

#endregion

#region ClassDefinitions

public class SameFrame: StackFrameBaseClass, IStackMapFrameUnion {
  public SameFrame(byte frameType, byte specificByte) : base(frameType, specificByte) {}

  public SameFrame(E_StackMapFrameTags frameType, byte specificByte) : base(frameType, specificByte) {}
}

public class SameLocales1StackFrame: StackFrameBaseClass, IStackMapFrameUnion {
  public IVerificationTypeUnion Stack;

  public SameLocales1StackFrame(byte frameType, byte specificByte, IVerificationTypeUnion stack) : base(frameType, specificByte) => Stack = stack;

  public SameLocales1StackFrame(E_StackMapFrameTags frameType, byte specificByte, IVerificationTypeUnion stack) : base(frameType, specificByte) => Stack = stack;
}

public class SameLocales1StackFrameExtended: SameLocales1StackFrame, IStackMapFrameUnion {
  public UInt16 OffsetDelta;

  public SameLocales1StackFrameExtended(byte frameType, byte specificByte, UInt16 offsetDelta, IVerificationTypeUnion stack) : base(frameType, specificByte, stack) => OffsetDelta = offsetDelta;

  public SameLocales1StackFrameExtended(E_StackMapFrameTags frameType, byte specificByte, UInt16 offsetDelta, IVerificationTypeUnion stack) : base(frameType, specificByte, stack) => OffsetDelta = offsetDelta;
}

public class ChopFrame: StackFrameBaseClass, IStackMapFrameUnion {
  public UInt16 OffsetDelta;

  public ChopFrame(byte frameType, byte specificByte, UInt16 offsetDelta) : base(frameType, specificByte) => OffsetDelta = offsetDelta;

  public ChopFrame(E_StackMapFrameTags frameType, byte specificByte, UInt16 offsetDelta) : base(frameType, specificByte) => OffsetDelta = offsetDelta;

}

public class SameFrameExtended: SameFrame, IStackMapFrameUnion {
  public UInt16 OffsetDelta;

  public SameFrameExtended(byte frameType, byte specificByte, UInt16 offsetDelta) : base(frameType, specificByte) => OffsetDelta = offsetDelta;

  public SameFrameExtended(E_StackMapFrameTags frameType, byte specificByte, UInt16 offsetDelta) : base(frameType, specificByte) => OffsetDelta = offsetDelta;
}

public class AppendFrame: StackFrameBaseClass, IStackMapFrameUnion {
  private const byte LOCALS_SUBTRACTION_CONSTANT = 251;
  public IVerificationTypeUnion[] Locals;
  public UInt16 OffsetDelta;

  public AppendFrame(byte frameType, byte specificByte, UInt16 offsetDelta) : base(frameType, specificByte) {
    OffsetDelta = offsetDelta;
    Locals = new IVerificationTypeUnion[specificByte - LOCALS_SUBTRACTION_CONSTANT];
  }

  public AppendFrame(E_StackMapFrameTags frameType, byte specificByte, UInt16 offsetDelta) : base(frameType, specificByte) {
    OffsetDelta = offsetDelta;
    Locals = new IVerificationTypeUnion[specificByte - LOCALS_SUBTRACTION_CONSTANT];
  }
}

public class FullFrame: StackFrameBaseClass, IStackMapFrameUnion {

  public UInt16 OffsetDelta;
  public UInt16 NumberOfLocals;
  public UInt16 NumberOfStackItems;

  public IVerificationTypeUnion[] Locals;
  public IVerificationTypeUnion[] Stack;

  public FullFrame(byte frameType, byte specificByte, UInt16 offsetDelta, UInt16 numberOfLocals, UInt16 numberOfStackItems) : base(frameType, specificByte) {
    OffsetDelta = offsetDelta;
    NumberOfLocals = numberOfLocals;
    NumberOfStackItems = numberOfStackItems;

    Locals = new IVerificationTypeUnion[numberOfLocals];
    Stack = new IVerificationTypeUnion[numberOfStackItems];
  }

  public FullFrame(E_StackMapFrameTags frameType, byte specificByte, UInt16 offsetDelta, UInt16 numberOfLocals, UInt16 numberOfStackItems) : base(frameType, specificByte) {
    OffsetDelta = offsetDelta;
    NumberOfLocals = numberOfLocals;
    NumberOfStackItems = numberOfStackItems;

    Locals = new IVerificationTypeUnion[numberOfLocals];
    Stack = new IVerificationTypeUnion[numberOfStackItems];
  }
}

#endregion