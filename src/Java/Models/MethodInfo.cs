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
  public IAttributeInfo[] Attributes;

  /// <summary>
  /// Constructs an MethodInfo class Instance with access flags, name index, 
  /// descriptor index, attributes count, and attributes array
  /// </summary>
  public MethodInfo(
    UInt16 accessFlags,
    UInt16 nameIndex,
    UInt16 descriptorIndex,
    UInt16 attributesCount
  ) {
    AccessFlags = ParseAccessFlags(accessFlags);
    FlagsToString();


    NameIndex = nameIndex;
    DescriptorIndex = descriptorIndex;
    AttributesCount = attributesCount;
    Attributes = new IAttributeInfo[attributesCount];
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

    result.Add((E_MethodAccessFlags)(accessFlags & visibilityMask));
    result.Add((E_MethodAccessFlags)(accessFlags & finalityStausMask));
    result.Add((E_MethodAccessFlags)(accessFlags & declarationTypeMask));

    UInt16 isSynthetic = (UInt16)(accessFlags & syntheticMask);
    if (isSynthetic != 0x0000)
      result.Add(E_MethodAccessFlags.ACC_SYNTHETIC);

    return result;
  }

  private string FlagsToString() {
    string result = string.Empty;

    foreach (E_MethodAccessFlags flag in AccessFlags) {
      result += flag.ToString();
      System.Console.WriteLine("Here");
      System.Console.WriteLine(flag.ToString());
    }

    Environment.Exit(1);

    return result;
  }

  #endregion

  public override string ToString()
  {
    string attributes = string.Join($",{Environment.NewLine}", Attributes.Select(attr => attr.ToString()));
    return $"MethodInfo(AccessFlags={AccessFlags},{Environment.NewLine}NameIndex={NameIndex},{Environment.NewLine}DescriptorIndex={DescriptorIndex},{Environment.NewLine}AttributesCount={{{AttributesCount}}},{Environment.NewLine}Attributes={attributes})";
  }
}
