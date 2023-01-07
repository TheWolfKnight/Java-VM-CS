
using CS_Java_VM.Src.Java.Constants;

namespace CS_Java_VM.Src.Java;

public class FieldsInfo {
  E_AccessFlags AccessFlags;
  UInt16 NameIndex;
  UInt16 DescriptorIndex;
  UInt16 AttributesCount;

  AttributeInfo[] Attributes;

  public FieldsInfo(UInt16 accessFlags, UInt16 nameIndex, UInt16 descriptorIndex, UInt16 attributesCount, AttributeInfo[] attributes) {
    AccessFlags = (E_AccessFlags)accessFlags;
    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
    AttributesCount = attributesCount;
    Attributes = attributes;
  }

  public FieldsInfo(E_AccessFlags accessFlags, UInt16 nameIndex, UInt16 descriptorIndex, UInt16 attributesCount, AttributeInfo[] attributes) {
    AccessFlags = (E_AccessFlags)accessFlags;
    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
    AttributesCount = attributesCount;
    Attributes = attributes;
  }
}
