using CS_Java_VM.Src.Validator.Type.Models;

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
  /// Contains all the default elements of the java language.
  /// </summary>
  private static Dictionary<string, string> JAVA_CONSTANTS = new Dictionary<string, string>() {
    { "java/lang/Object",    "Object" },
    { "java/lang/String",    "String" },
    { "java/lang/Integer",   "Integer"},
    { "java/io/PrintStream", "PrintStream" },
    { "java/util/ArrayList", "ArrayList" },
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
        if (sufix == ";") {
          // Greps a substring where the linker L and ; is not present
          string linkedType = type.Substring(1, type.Length-2);

          // Checks if the type is a default for java, as the java/lang
          // files will not be compiled, but emulated in the C# engine
          bool isJavaDefault = JAVA_CONSTANTS.TryGetValue(linkedType, out string? defaultName);

          // If the linkedType is in the JAVA_CONSTANTS return a VarType
          // with the type and name.
          if (isJavaDefault && defaultName != null) return new VarType(defaultName, name);

          // TODO: Make a change such that generics get accounted for.
          // INFO: the signature for a generic is something
          //       like: L{link location}<{inner type}>;

          // Returns the VarType for the linked type, and with the name set.
          return new VarType(linkedType, name);
        }
      }
    }
    // If these checks fail, the program throws an InvalidDataException
    throw new InvalidDataException($"The input: {type} is not a type");
  }
}
