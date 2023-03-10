using CS_Java_VM.Src.Java.Constants;

namespace CS_Java_VM.Src.Java.Union;

#region Interface

public interface IVerificationTypeUnion {}

#endregion

#region BaseClassDefinition

public class VerificationTypeBase: IVerificationTypeUnion {
  public E_VerificationTypeInfo Tag;

  public VerificationTypeBase(byte tag) {
    Tag = (E_VerificationTypeInfo)tag;
  }

  public VerificationTypeBase(E_VerificationTypeInfo tag) {
    Tag = tag;
  }
}

#endregion

#region SpecialClassDefiniton

public class ObjectVerificationType: VerificationTypeBase, IVerificationTypeUnion {
  public UInt16 CPoolIndex;

  public ObjectVerificationType(byte tag, UInt16 cPoolIndex) : base(tag) {
    CPoolIndex = cPoolIndex;
  }

  public ObjectVerificationType(E_VerificationTypeInfo tag, UInt16 cPoolIndex) : base(tag) {
    CPoolIndex = cPoolIndex;
  }
}

public class UninitializedVariableVerificationType: VerificationTypeBase, IVerificationTypeUnion {
  public UInt16 Offset;

    public UninitializedVariableVerificationType(byte tag, UInt16 offset) : base(tag) {
    Offset = offset;
  }

  public UninitializedVariableVerificationType(E_VerificationTypeInfo tag, UInt16 cPoolIndex) : base(tag) {
    Offset = cPoolIndex;
  }
}

#endregion