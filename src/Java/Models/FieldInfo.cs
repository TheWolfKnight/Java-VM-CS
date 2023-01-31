
using CS_Java_VM.Src.Java.Constants;

using System.Collections.Generic;
using System;

namespace CS_Java_VM.Src.Java.Models;

public class FieldsInfo {
  private int ArrayPointer = 0;
  List<E_FieldAccessFlags> AccessFlags;
  UInt16 NameIndex;
  UInt16 DescriptorIndex;
  UInt16 AttributesCount;
  AttributeInfo[] Attributes;

  public FieldsInfo(UInt16 accessFlags, UInt16 nameIndex, UInt16 descriptorIndex, UInt16 attributesCount) {
    AccessFlags = ParseAccessFlags(accessFlags);
    FlagsToString();

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
    if (ArrayPointer == AttributesCount)
      throw new IndexOutOfRangeException("Could not push the attribute to the Attributes array.");

    Attributes[ArrayPointer] = attribute;
    ArrayPointer++;
  }

  /// <summary>
  /// Converts the access flags mask into the desired flags
  /// </summary>
  /// <param name="accessFlags"> The access flags used to figure out which flags should be set </param>
  private List<E_FieldAccessFlags> ParseAccessFlags(UInt16 accessFlags) {
    List<E_FieldAccessFlags> result = new List<E_FieldAccessFlags>();

    const UInt16 visibilityMask       = 0x000F,
                 finalityStausMask    = 0x00F0,
                 syntheticMask        = 0xF000;

    UInt16 visibilityStatus = (UInt16)(accessFlags & visibilityMask);
    if (visibilityStatus == 0x0000)
      throw new ArgumentException("The visibility status must be set");
    else {
      const UInt16 publicMask    = 0x0001,
                   privateMask   = 0x0002,
                   protectedMask = 0x0004,
                   staticMask    = 0x0008;
      if ((UInt16)(visibilityStatus & publicMask) != 0x0000)
        result.Add(E_FieldAccessFlags.ACC_PUBLIC);
      else if ((UInt16)(visibilityStatus & privateMask) != 0x0000)
        result.Add(E_FieldAccessFlags.ACC_PRIVAT);
      else if ((UInt16)(visibilityStatus & protectedMask) != 0x0000)
        result.Add(E_FieldAccessFlags.ACC_PROTECTED);
      if ((UInt16)(visibilityStatus & staticMask) != 0x0000)
        result.Add(E_FieldAccessFlags.ACC_STATIC);
    }

    UInt16 finalityStaus = (UInt16)(accessFlags & finalityStausMask);
    if (finalityStaus != 0x0000)
      result.Add((E_FieldAccessFlags)finalityStaus);

    UInt16 isSynthetic = (UInt16)(accessFlags & syntheticMask);
    if (isSynthetic != 0x0000)
      result.Add(E_FieldAccessFlags.ACC_SYNTHETIC);

    return result;
  }

  private string FlagsToString() {
    string result = string.Join(", ", AccessFlags.Select(item => item.ToString()));
    return result;
  }

  public override string ToString()
  {
    string accessFlagsString = FlagsToString();
    string attributesString = string.Join($",{Environment.NewLine}\t\t\t\t", Attributes.AsEnumerable());

    return $"FieldsInfo(AccessFlags=[{accessFlagsString}], NameIndex={NameIndex}, DescriptorIndex={DescriptorIndex}),AttributesCount={AttributesCount},{Environment.NewLine}\t\t\tAttributes={attributesString})";
  }

}
