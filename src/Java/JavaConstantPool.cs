using System;

using CS_Java_VM.Src.Java.Constants;

namespace CS_Java_VM.Src.Java;

#region InterfaceDefinition
public interface IConstantPool {
  ushort GetByteSize();
}
#endregion

#region  GenericPoolDefinition
public class ConstantPoolGeneric: IConstantPool {
  public const ushort ByteSize = 2;
  public E_ConstantPoolTag Tag;
  public byte[] Info;

  public ConstantPoolGeneric(byte tag, byte[] info) {
    Tag = (E_ConstantPoolTag)tag;
    Info = info;
  }
  public ConstantPoolGeneric(E_ConstantPoolTag tag, byte[] info) {
    Tag = tag;
    Info = info;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }

}
#endregion

#region MajorPoolDefinitions
public class ConstantPoolClass: IConstantPool {
  public const ushort ByteSize = 3;
  public E_ConstantPoolTag Tag;
  public UInt16 NameIndex;

  public ConstantPoolClass(byte tag, UInt16 nameIndex) {
    Tag = (E_ConstantPoolTag)tag;
    NameIndex = nameIndex;
  }

  public ConstantPoolClass(E_ConstantPoolTag tag, UInt16 nameIndex) {
    Tag = tag;
    NameIndex = nameIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolRef: IConstantPool {
  public const ushort ByteSize = 5;
  public E_ConstantPoolTag Tag;
  public UInt16 ClassIndex;
  public UInt16 NameAndTypeIndex;

  public ConstantPoolRef(byte tag, UInt16 classIndex, UInt16 nameAndTypeIndex) {
    Tag = (E_ConstantPoolTag)tag;
    ClassIndex = classIndex;
    NameAndTypeIndex = nameAndTypeIndex;
  }

  public ConstantPoolRef(E_ConstantPoolTag tag, UInt16 classIndex, UInt16 nameAndTypeIndex) {
    Tag = tag;
    ClassIndex = classIndex;
    NameAndTypeIndex = nameAndTypeIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolString: IConstantPool {
  public const ushort ByteSize = 3;
  public E_ConstantPoolTag Tag;
  public UInt16 StringIndex;

  public ConstantPoolString(byte tag, UInt16 stringIndex) {
    Tag = (E_ConstantPoolTag)tag;
    StringIndex = stringIndex;
  }

  public ConstantPoolString(E_ConstantPoolTag tag, UInt16 stringIndex) {
    Tag = tag;
    StringIndex = stringIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolNumberInfo: IConstantPool {
  public const ushort ByteSize = 5;
  public E_ConstantPoolTag Tag;
  public UInt32 Bytes;

  public ConstantPoolNumberInfo(byte tag, UInt32 bytes) {
    Tag = (E_ConstantPoolTag)tag;
    Bytes = bytes;
  }

  public ConstantPoolNumberInfo(E_ConstantPoolTag tag, UInt32 bytes) {
    Tag = tag;
    Bytes = bytes;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolLongDoubleInfo: IConstantPool {
  public const ushort ByteSize = 9;
  public E_ConstantPoolTag Tag;
  public UInt32 HighBytes, LowBytes;

  public ConstantPoolLongDoubleInfo(byte tag, UInt32 highBytes, UInt32 lowBytes) {
    Tag = (E_ConstantPoolTag)tag;
    HighBytes = highBytes;
    LowBytes = lowBytes;
  }

  public ConstantPoolLongDoubleInfo(E_ConstantPoolTag tag, UInt32 highBytes, UInt32 lowBytes) {
    Tag = tag;
    HighBytes = highBytes;
    LowBytes = lowBytes;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolNameAndTypeInfo: IConstantPool {
  public const ushort ByteSize = 5;
  public E_ConstantPoolTag Tag;
  public UInt16 NameIndex;
  public UInt16 DescriptorIndex;

  public ConstantPoolNameAndTypeInfo(byte tag, UInt16 nameIndex, UInt16 descriptorIndex) {
    Tag = (E_ConstantPoolTag)tag;
    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
  }

  public ConstantPoolNameAndTypeInfo(E_ConstantPoolTag tag, UInt16 nameIndex, UInt16 descriptorIndex) {
    Tag = tag;
    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolUtf8Info: IConstantPool {
  private const ushort AssuredByteSize = 3;
  private uint ByteArrayPointer = 0;

  public E_ConstantPoolTag Tag;
  public UInt16 Length;
  public byte[] Bytes;

  public ConstantPoolUtf8Info(byte tag, UInt16 length) {
    Tag = (E_ConstantPoolTag)tag;
    Length = length;
    Bytes = new byte[length];
  }

  public ConstantPoolUtf8Info(E_ConstantPoolTag tag, UInt16 length) {
    Tag = tag;
    Length = length;
    Bytes = new byte[length];
  }

  public void AddToByteArray(byte n) {
    if (ByteArrayPointer == Length)
      throw new IndexOutOfRangeException($"Could not push the element: b{n} to the array");

    Bytes[ByteArrayPointer] = n;
    ByteArrayPointer++;
  }

  public UInt16 GetByteSize() {
    return (UInt16)(AssuredByteSize + Length);
  }
}

public class ConstantPoolMethodHandleInfo: IConstantPool {
  public const ushort ByteSize = 4;
  public E_ConstantPoolTag Tag;
  public E_ReferenceKind ReferenceKind;
  public UInt16 ReferenceIndex;

  public ConstantPoolMethodHandleInfo(byte tag, byte referenceKind, UInt16 referenceIndex) {
    Tag = (E_ConstantPoolTag)tag;
    ReferenceKind = (E_ReferenceKind)referenceKind;
    ReferenceIndex = referenceIndex;
  }

  public ConstantPoolMethodHandleInfo(E_ConstantPoolTag tag, E_ReferenceKind referenceKind, UInt16 referenceIndex) {
    Tag = tag;
    ReferenceKind = referenceKind;
    ReferenceIndex = referenceIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolMethodTypeInfo: IConstantPool {
  public const ushort ByteSize = 3;
  public E_ConstantPoolTag Tag;
  public UInt16 DescriptorIndex;

  public ConstantPoolMethodTypeInfo(byte tag, UInt16 descriptorIndex) {
    Tag = (E_ConstantPoolTag)tag;
    DescriptorIndex = descriptorIndex;
  }

  public ConstantPoolMethodTypeInfo(E_ConstantPoolTag tag, UInt16 descriptorIndex) {
    Tag = tag;
    DescriptorIndex = descriptorIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolDynamicInfo: IConstantPool {
  public const ushort ByteSize = 5;
  public E_ConstantPoolTag Tag;
  public UInt16 BootstrapMethodAttrIndex;
  public UInt16 NameAndTypeIndex;

  public ConstantPoolDynamicInfo(byte tag, UInt16 bootstrapMethodAttrIndex, UInt16 nameAndTypeIndex) {
    Tag = (E_ConstantPoolTag)tag;
    BootstrapMethodAttrIndex = bootstrapMethodAttrIndex;
    NameAndTypeIndex = nameAndTypeIndex;
  }

  public ConstantPoolDynamicInfo(E_ConstantPoolTag tag, UInt16 bootstrapMethodAttrIndex, UInt16 nameAndTypeIndex) {
    Tag = tag;
    BootstrapMethodAttrIndex = bootstrapMethodAttrIndex;
    NameAndTypeIndex = nameAndTypeIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

public class ConstantPoolPackageModuleInfo: IConstantPool {
  public const ushort ByteSize = 3;
  public E_ConstantPoolTag Tag;
  public UInt16 NameIndex;

  public ConstantPoolPackageModuleInfo(byte tag, UInt16 nameIndex) {
    Tag = (E_ConstantPoolTag)tag;
    NameIndex = nameIndex;
  }

  public ConstantPoolPackageModuleInfo(E_ConstantPoolTag tag, UInt16 nameIndex) {
    Tag = tag;
    NameIndex = nameIndex;
  }

  public UInt16 GetByteSize() {
    return ByteSize;
  }
}

#endregion