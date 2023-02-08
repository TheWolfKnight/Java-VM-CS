
using CS_Java_VM.Src.Validator.Type;

namespace CS_Java_VM.Src.Validator.Type.Models;


public class VarType {
  public (string TypeTag, string VarName) Identity;
  public VarType[]? Inner;

  public VarType(string varName, string typeTag, VarType[]? inner=null) {
    Identity = (typeTag, varName);
    Inner = inner;
  }

  public override string ToString()
  {
    return $"VarType(VarName={Identity.VarName}, TypeTag={Identity.TypeTag})";
  }
}
