using CS_Java_VM.Src.Java.Constants;

using System.Collections.Generic;
using System;

namespace CS_Java_VM.Src.Java;

public class MethodInfo {
  private int ArrayPointer = 0;

  public List<E_AccessFlags> AccessFlags;
  public UInt16 NameIndex;
  public UInt16 DescriptorIndex;
  public UInt16 AttributesCount;
  public AttributeInfo[] Attributes;

  /// <summary>
  /// Constructs an MethodInfo class Instance with access flags, name index, 
  /// descriptor index, attributes count, and attributes array
  /// </summary>
  public MethodInfo(
    UInt16 accessFlagsMask,
    UInt16 nameIndex,
    UInt16 descriptorIndex,
    UInt16 attributesCount
  ) {
    AccessFlags = ParseAccessFlagsMask(accessFlagsMask);
    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
    AttributesCount = attributesCount;
    Attributes = new AttributeInfo[attributesCount];
  }

  /// <summary>
  /// Adds a element to the Attributes array
  /// </summary>
  /// <param name="attribute"> The AttributeInfo instance that gets pushed to the Attributes array </param>
  public void AddAttributeToAttributeArray(AttributeInfo attribute) {
    if (Attributes.Length == AttributesCount)
      throw new IndexOutOfRangeException("Could not push the attribute to the Attributes array.");

    Attributes[ArrayPointer] = attribute;
    ArrayPointer++;
  }

  /// <summary>
  /// Converts the access flags mask into the desired flags
  /// </summary>
  private List<E_AccessFlags> ParseAccessFlagsMask(UInt16 mask) {
    List<E_AccessFlags> result = new List<E_AccessFlags>();

    const UInt16 visibilityMask = 0x000F;
    const UInt16 finalityStausMask = 0x00F0;
    const UInt16 declarationTypeMask = 0x0F00;
    const UInt16 syntheticMask = 0xF000;

    UInt16 visibilityMask

    return result;
  }

}
