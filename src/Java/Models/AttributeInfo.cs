using CS_Java_VM.Src.Java.Constants;
using CS_Java_VM.Src.Java.Union;

using System.Text;

namespace CS_Java_VM.Src.Java.Models;


public class AttributeInfo {
  public UInt16 AttributeNameIndex;
  public UInt32 AttributeLength;
  public IEnumerable<byte> Info;

  public AttributeInfo(UInt16 attributeNameIndex, UInt32 attributeLength, IEnumerable<byte> info) {
    AttributeNameIndex = attributeNameIndex;
    AttributeLength = attributeLength;
    Info = info;
  }

  public string StringRep() {
    return Encoding.Default.GetString(Info.ToArray());
  }

  public override string ToString()
  {
    string infoString = Info != null ? string.Join($", ", Info) : "";

    return $"AttributeInfo(AttributeNameIndex={AttributeNameIndex}, AttributeLength={AttributeLength}, Info=[{infoString}])";
  }
}
