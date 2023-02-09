
using CS_Java_VM.Src.Validator.Type.Models;

using System.Text.RegularExpressions;

namespace CS_Java_VM.Src.Validator.Services;

public static class TypeCheckConstants {
  /// <summary>
  /// This dictionary contains all the, valid types in the Java language.<br/>
  /// WARNING: This dose not include the types that require linking or
  ///          the array.
  /// </summary>
  private static Dictionary<char, string> TYPE_NAMES = new Dictionary<char, string>() {
    { 'B',  "byte" },
    { 'C',  "char" },
    { 'D',  "double" },
    { 'F' , "float"},
    { 'I' , "int"},
    { 'J' , "long"},
    { 'S' , "short"},
    { 'Z' , "boolean"},
  };

  /// <summary>
  /// Gets a Variable type, and name, and returns a VarType with the compiled information.
  /// </summary>
  /// <param name="type"> The type information for the variable </param>
  /// <param name="name"> The name of the variable,<br/>WARNING: Can be null if the type is an inner type </param>
  /// <returns> Returns a VarType with the information for the variable </returns>
  public static VarType GetType(string type, string? name=null) {
    // Checks to see if the input is a single char
    bool isChar = char.TryParse(type, out char res);

    if (isChar) {
      // Checks if the char is in the TYPE_NAMES dict
      bool inTYPE_NAMES = TYPE_NAMES.TryGetValue(res, out string? value);
      // If the char is in the TYPE_NAMES, return a new VarType with that type.
      // NOTE: in this case the value will not be null, this is just a check to
      //       remove a yellow line.
      if (inTYPE_NAMES && value != null)
        return new VarType(value, name);
    } else {
      // If the input is not a char, checks if the first letter is a
      // oppening bracket, or if its a  L.
      string prefix = type.Substring(0,1);
      if (prefix == "[") {
        // If the type seems to be an array, the functions returns a
        // VarType with the type tag "array", and recursivly calls it
        // selv, without the first char.
        return new VarType("array",
                           name,
                           new VarType[] {
                            GetType(type.Substring(1, type.Length-1))
                          });
      }
      else if (prefix == "L") {
        // If the type is found to be a linker, program checks the last
        // char to see if it is a ";". This makes sure a file/folder that
        // starts with "L" is not falesly identified.
        string sufix = type.Substring(type.Length-1, 1);
        if (sufix != ";")
          throw new InvalidDataException("If a linker is discoved, it must end with \";\"");

        // Greps a substring where the linker L and ; is not present
        string linkedType = type.Substring(1, type.Length-2);
        // Make a regex ready to see if the type contains generics
        Regex regex = new Regex(@"[a-zA-Z_\/]+<[a-zA-Z_\/]+>");
        VarType result = new VarType(linkedType, name);

        // if the linkedType is found to be a match for the regex
        // statment, then it will be treated as a generic type.
        if (regex.IsMatch(linkedType)) {
          // Checks the type tag of the current VarType result
          // to make sure it is not with the generic in the to
          // level tag
          result.TypeTag = result.TypeTag.Substring(0, result.TypeTag.IndexOf('<'));

          // Generates the inner types for the VarType
          VarType[] genericInnerTypes =
            GenerateInnerTypes(linkedType.Substring(linkedType.IndexOf('<'), linkedType.Length-2));

          // Assignes the inner types to result.InnerType
          result.Inner = genericInnerTypes;
        }

        // Returns the VarType for the linked type, and with the name set.
        return result;
      }
    }
    // If these checks fail, the program throws an InvalidDataException
    throw new InvalidDataException($"The input: {type} is not a type");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="">  </param>
  private static VarType[] GenerateInnerTypes(string type) {
    throw new NotImplementedException();
  }

}
