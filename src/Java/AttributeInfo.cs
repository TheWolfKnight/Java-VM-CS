
using System;

namespace CS_Java_VM.Src.Java;

public class AttributeInfo {
  UInt16 AttributeNameIndex;
  UInt16 AttributeLength;
  byte[] Info;

  public AttributeInfo(UInt16 attributeNameIndex, UInt16 attributeLength, byte[] info) {
    AttributeNameIndex = attributeNameIndex;
    AttributeLength = attributeLength;
    Info = info;
  }
}