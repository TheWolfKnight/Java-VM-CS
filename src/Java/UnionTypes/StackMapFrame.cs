
using CS_Java_VM.Src.Java.Constants;

using System;

namespace CS_Java_VM.Src.Java.Union;

#region Interface

public interface IStackMapFrame {}

#endregion

#region ClassDefinitions

public class SameFrame: IStackMapFrame {
  public byte SpecificByte;
  public E_StackMapFrameTags FrameType;

  public SameFrame(byte frameType, byte specificByte) {
    FrameType = (E_StackMapFrameTags)frameType;
    SpecificByte = specificByte;
  }

  public SameFrame(E_StackMapFrameTags frameType, byte specificByte) {
    FrameType = frameType;
    SpecificByte = specificByte;
  }
}

public class SameLocales1StackFrame: IStackMapFrame {
  public byte SpecificByte;
  public E_StackMapFrameTags FrameType;
  public IVerificationType Stack;

  public SameLocales1StackFrame(byte frameType, byte specificByte, IVerificationType stack) {
    FrameType = (E_StackMapFrameTags)frameType;
    SpecificByte = specificByte;
    Stack = stack;
  }

  public SameLocales1StackFrame(E_StackMapFrameTags frameType, byte specificByte, IVerificationType stack) {
    FrameType = frameType;
    SpecificByte = specificByte;
    Stack = stack;
  }
}

#endregion