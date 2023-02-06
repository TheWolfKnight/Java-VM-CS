using System;
using System.Text;

using CS_Java_VM.Src.Java.Constants;

namespace CS_Java_VM.Src.Java.Models;

#region InterfaceDefinition
public interface IConstantPool {
  E_ConstantPoolTag GetTag();
  string ToString();
}
#endregion

#region  GenericPoolDefinition
public class ConstantPoolGeneric: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolGeneric(Info=[{string.Join(", ", Info)}])";
  }

}
#endregion

#region MajorPoolDefinitions
public class ConstantPoolClass: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolClass(NameIndex={NameIndex})";
  }

}

public class ConstantPoolRef: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolRef(ClassIndex={ClassIndex}, NameAndTypeIndex={NameAndTypeIndex})";
  }
}

public class ConstantPoolString: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolString(StringIndex={StringIndex})";
  }
}

public class ConstantPoolNumberInfo: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolNumberInfo(Bytes={Bytes})";
  }
}

public class ConstantPoolLongDoubleInfo: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolLongDoubleInfo(HighBytes={HighBytes}, LowBytes={LowBytes})";
  }
}

public class ConstantPoolNameAndTypeInfo: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolNameAndTypeInfo(NameIndex={NameIndex}, DescriptorIndex={DescriptorIndex})";
  }
}

public class ConstantPoolUtf8Info: IConstantPool {
  private uint ArrayPointer = 0;

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
    if (ArrayPointer == Length)
      throw new IndexOutOfRangeException($"Could not push the element: b{n} to the array");

    Bytes[ArrayPointer] = n;
    ArrayPointer++;
  }

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public string GetStringRepresentation() {
    return Encoding.Default.GetString(Bytes);
  }

  public override string ToString()
  {
    return $"ConstantPoolUtf8Info(Length={Length}, Bytes=[{string.Join(", ", Bytes)}], StringRep={GetStringRepresentation()})";
  }
}

public class ConstantPoolMethodHandleInfo: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolMethodHandleInfo(ReferenceKind={ReferenceKind}, ReferenceIndex={ReferenceIndex})";
  }
}

public class ConstantPoolMethodTypeInfo: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolMethodTypeInfo(DescriptorIndex={DescriptorIndex})";
  }
}

public class ConstantPoolDynamicInfo: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolDynamicInfo(BootstrapMethodAttrIndex={BootstrapMethodAttrIndex}, NameAndTypeIndex={NameAndTypeIndex})";
  }
}

public class ConstantPoolPackageModuleInfo: IConstantPool {
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

  public E_ConstantPoolTag GetTag() {
    return Tag;
  }

  public override string ToString()
  {
    return $"ConstantPoolPackageModuleInfo(NameIndex={NameIndex})";
  }
}

#endregion