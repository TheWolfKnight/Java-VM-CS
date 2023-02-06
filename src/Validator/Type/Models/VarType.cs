
using CS_Java_VM.Src.Validator.Type.Constants;

namespace CS_Java_VM.Src.Validator.Type.Models;

public class VarType {
  public string VarName;
  public E_TypeTags TypeTag;
  public VarType? Inner;

  public VarType(string varName, E_TypeTags typeTag, VarType? inner=null) {
    VarName = varName;
    TypeTag = typeTag;
    Inner = inner;
  }

  public override string ToString()
  {
    return $"VarType(VarName={VarName}, TypeTag={TypeTag})";
  }
}
