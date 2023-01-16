
using CS_Java_VM.Src.Java.Constants;

using System.Collections.Generic;
using System;

namespace CS_Java_VM.Src.Java.Models;

public class FieldsInfo {
  private int ArrayPointer = 0;
  UInt16 AccessFlags;
  UInt16 NameIndex;
  UInt16 DescriptorIndex;
  UInt16 AttributesCount;

  IAttributeInfo[] Attributes;

  public FieldsInfo(UInt16 accessFlags, UInt16 nameIndex, UInt16 descriptorIndex, UInt16 attributesCount) {
    AccessFlags = accessFlags;
    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
    AttributesCount = attributesCount;
    Attributes = new IAttributeInfo[attributesCount-1];
  }

  /// <summary>
  /// Adds a element to the Attributes array
  /// </summary>
  /// <param name="attribute"> The AttributeInfo instance that gets pushed to the Attributes array </param>
  public void AddAttributeToAttributeArray(IAttributeInfo attribute) {
    if (Attributes.Length == AttributesCount)
      throw new IndexOutOfRangeException("Could not push the attribute to the Attributes array.");

    Attributes[ArrayPointer] = attribute;
    ArrayPointer++;
  }

  /// <summary>
  /// Converts the access flags mask into the desired flags
  /// </summary>
  /// <param name="accessFlags"> The access flags used to figure out which flags should be set </param>
  #pragma warning disable
  private List<E_FieldAccessFlags> ParseAccessFlagsMask(UInt16 accessFlags) {
    throw new Exception("Not in use");
    List<E_FieldAccessFlags> result = new List<E_FieldAccessFlags>();

    const UInt16 visibilityMask = 0x000F;
    const UInt16 finalityStausMask = 0x00F0;
    const UInt16 declarationTypeMask = 0x0F00;
    const UInt16 syntheticMask = 0xF000;

    result.Add((E_FieldAccessFlags)(accessFlags & visibilityMask));
    result.Add((E_FieldAccessFlags)(accessFlags & finalityStausMask));
    result.Add((E_FieldAccessFlags)(accessFlags & declarationTypeMask));

    UInt16 isSynthetic = (UInt16)(accessFlags & syntheticMask);
    if (isSynthetic != 0x0000)
      result.Add(E_FieldAccessFlags.ACC_SYNTHETIC);

    return result;
  }
  #pragma warning restore
}
