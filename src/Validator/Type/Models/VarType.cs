
using CS_Java_VM.Src.Validator.Type;

namespace CS_Java_VM.Src.Validator.Type.Models;


public class VarType {
  public string TypeTag;
  public string? VarName;
  public VarType[]? Inner;

  public VarType(string typeTag, string? varName=null,  VarType[]? inner=null) {
    VarName = varName;
    TypeTag = typeTag;
    Inner = inner;
  }

  public override string ToString()
  {
    string varName = VarName != null ? VarName : "InnerType";
    return $"VarType(VarName={varName}, TypeTag={TypeTag})";
  }
}
