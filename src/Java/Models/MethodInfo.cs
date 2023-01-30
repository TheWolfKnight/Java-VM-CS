using CS_Java_VM.Src.Java.Constants;

using System;
using System.Linq;
using System.Collections.Generic;

namespace CS_Java_VM.Src.Java.Models;

public class MethodInfo {
  private int ArrayPointer = 0;

  public List<E_MethodAccessFlags> AccessFlags;
  public UInt16 NameIndex;
  public UInt16 DescriptorIndex;
  public UInt16 AttributesCount;
  public AttributeInfo[] Attributes;

  /// <summary>
  /// Constructs an MethodInfo class Instance with access flags, name index, 
  /// descriptor index, attributes count, and attributes array
  /// </summary>
  public MethodInfo(UInt16 accessFlags,
                    UInt16 nameIndex,
                    UInt16 descriptorIndex,
                    UInt16 attributesCount)
  {
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

  #region Unused region

  /// <summary>
  /// Converts the access flags mask into the desired flags
  /// </summary>
  /// <param name="accessFlags"> The access flags used to figure out which flags should be set </param>
  private List<E_MethodAccessFlags> ParseAccessFlags(UInt16 accessFlags) {
    List<E_MethodAccessFlags> result = new List<E_MethodAccessFlags>();

    const UInt16 visibilityMask = 0x000F;
    const UInt16 finalityStausMask = 0x00F0;
    const UInt16 declarationTypeMask = 0x0F00;
    const UInt16 syntheticMask = 0xF000;

    UInt16 visbilityStatus = (UInt16)(accessFlags & visibilityMask);
    if (visbilityStatus == 0x0000)
      throw new ArgumentException("The visibility status must be set");
    else
      result.Add((E_MethodAccessFlags)visbilityStatus);

    UInt16 finalityStaus = (UInt16)(accessFlags & finalityStausMask);
    if (finalityStaus != 0x0000)
      result.Add((E_MethodAccessFlags)finalityStaus);

    UInt16 declarationType = (UInt16)(accessFlags & declarationTypeMask);
    if (declarationType != 0x0000)
      result.Add((E_MethodAccessFlags)declarationType);

    UInt16 isSynthetic = (UInt16)(accessFlags & syntheticMask);
    if (isSynthetic != 0x0000)
      result.Add(E_MethodAccessFlags.ACC_SYNTHETIC);

    return result;
  }

  private string FlagsToString() {
    string result = string.Join(", ", AccessFlags);
    return result;
  }

  #endregion

  public override string ToString()
  {
    string attributes = string.Join($",{Environment.NewLine}\t\t\t\t", Attributes.AsEnumerable());
    return $"MethodInfo(AccessFlags={AccessFlags}, NameIndex={NameIndex},DescriptorIndex={DescriptorIndex},AttributesCount={AttributesCount},{Environment.NewLine}\t\t\tAttributes={attributes})";
  }
}
