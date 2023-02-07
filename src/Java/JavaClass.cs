using CS_Java_VM.Src.Maths.Convertor;
using CS_Java_VM.Src.Java.Constants;
using CS_Java_VM.Src.Java.Models;

namespace CS_Java_VM.Src.Java;


public class JavaClass {
  /// <summary>
  /// The constant magic number in the Java class file, this will preceed all other items in the file
  /// </summary>
  private const UInt32 JAVA_CLASS_MAGIC_NUMBER = 0xCAFEBABE;

  /// <summary>
  /// Is the magic number for a Java class file, and will always be 0xCAFEBABE
  /// </summary>
  public UInt32 MagicNumber;

  public UInt16 MinorVersion, MajorVersion;
  public List<E_JavaClassAccessFlags> AccessFlags;

  public UInt16 ThisClass, SuperClass;

  public UInt16 ConstantPoolCount;
  public IConstantPool[]? ConstantPool;

  public UInt16 InterfacesCount;
  public UInt16[]? Interfaces;

  public UInt16 FieldsCount;
  public FieldsInfo[]? Fields;

  public UInt16 MethodsCount;
  public MethodInfo[]? Methods; 

  public UInt16 AttributesCount;
  public AttributeInfo[]? Attributes;

  /// <summary>
  /// The constructor for a Java class file
  /// </summary>
  /// <param name="classFilePath"> The path to the class file being parsed </param>
  public JavaClass(string classFilePath) {
    Int32 pointer = 0;
    byte[] bytes = File.ReadAllBytes(classFilePath);

    // Should always be equal 0xCAFEBABE
    MagicNumber = Convertor.BytesToUInt32(bytes.Skip(pointer).Take(4));
    pointer += 4;
    if (MagicNumber != JAVA_CLASS_MAGIC_NUMBER)
      throw new InvalidDataException($"The data in the magic number does not match the know Java class file magic number.{System.Environment.NewLine}The parsor found: {MagicNumber}, but excpected: {JAVA_CLASS_MAGIC_NUMBER}");

    // Get both the minor and major versions of the file, used
    // to figure out if a function is supported later down the line
    MinorVersion = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    MajorVersion = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;

    // Gets the constant pool count, and inits the constant pool
    ConstantPoolCount = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    if (ConstantPoolCount > 0) {
      ConstantPool = new IConstantPool[ConstantPoolCount-1];
      // Fills the ConstantPool array with the files constants
      for (int i = 0; i < ConstantPoolCount-1; i++) {
        byte constantPoolTag = bytes.Skip(pointer).Take(1).First();
        pointer++;
        IConstantPool newConstant = ParseConstantPoolTag(constantPoolTag, ref pointer, ref bytes);
        ConstantPool[i] = newConstant;
      }
    }

    // Gets the AccessFlag from
    AccessFlags =
      GenerateAccessFlags(Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2)));
    pointer += 2;

    // Gets the this class and super class from the Java class file
    ThisClass = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;

    SuperClass = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;

    // Gets the InterfaceCount and sets the Interfaces array to be InterfaceCount-1
    InterfacesCount = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    Interfaces = new UInt16[InterfacesCount];
    for (int i = 0; i < InterfacesCount; ++i) {
      Interfaces[i] = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
      pointer += 2;
    }

    // Gets the FieldsCount variable and sets the Fields array to be of size FieldsCount-1
    FieldsCount = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    Fields = new FieldsInfo[FieldsCount];
    for (int i = 0; i < FieldsCount; ++i) {
      FieldsInfo info = GenerateFieldsInfo(ref pointer, ref bytes);
      Fields[i] = info;
    }

    // Gets the MethodsCount variable and sets the Methods array to be of size MethodsCount-1
    MethodsCount = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    Methods = new MethodInfo[MethodsCount];
    for (int i = 0; i < MethodsCount; i++) {
      MethodInfo newMethodInfo = GenerateMethodInfo(ref pointer, ref bytes);
      Methods[i] = newMethodInfo;
    }

    // Gets the AttributesCount variable and sets the Attributes array to be of size AttributesCount-1
    AttributesCount = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    Attributes = new AttributeInfo[AttributesCount];
    for (int i = 0; i < AttributesCount; ++i) {
      AttributeInfo attribute = GenerateAttributeInfo(ref pointer, ref bytes);
      Attributes[i] = attribute;
    }
  }

  private List<E_JavaClassAccessFlags> GenerateAccessFlags(UInt16 accessFlags) {
    List<E_JavaClassAccessFlags> result = new List<E_JavaClassAccessFlags>();

    const UInt16 visibilityMask     = 0x000F,
                 finalityStatusMask = 0x00F0,
                 definitionMask     = 0x0F00,
                 typeMask           = 0xF000;

    UInt16 visibility = (UInt16)(accessFlags & visibilityMask);
    if (visibility != 0x0000)
      result.Add((E_JavaClassAccessFlags)visibility);

    UInt16 finalityStatus = (UInt16)(accessFlags & finalityStatusMask);
    if (finalityStatus != 0x0000)
      result.Add((E_JavaClassAccessFlags)finalityStatus);

    UInt16 definition = (UInt16)(accessFlags & definitionMask);
    if (definition != 0x0000)
      result.Add((E_JavaClassAccessFlags)definition);

    UInt16 type = (UInt16)(accessFlags & typeMask);
    if (type != 0x0000)
      result.Add((E_JavaClassAccessFlags)type);

    return result;
  }

  private void ValidateSuperClass() {
    if (ConstantPool == null)
      throw new NullReferenceException("Cannot reference the ConstantPool as it is null");

    if (SuperClass == 0) {
      IConstantPool item = ConstantPool[ThisClass-1];
      if (item.GetTag() != E_ConstantPoolTag.CONSTANT_CLASS)
        throw new InvalidDataException("The item at the ThisClass pointer must be a CONSTANT_CLASS type or the class must have ACC_INTERFACE");

      ConstantPoolClass constantClass = (ConstantPoolClass)item;

      IConstantPool nameRef = ConstantPool[constantClass.NameIndex-1];

      if (nameRef.GetTag() != E_ConstantPoolTag.CONSTANT_UTF8)
        throw new InvalidDataException("The name index must point to a CONSTANT_UTF8 in the ConstantPool table");

      ConstantPoolUtf8Info info = (ConstantPoolUtf8Info)nameRef;

      if (info.GetStringRepresentation() != "java/lang/Object")
        throw new InvalidDataException("The only acceptable result for the ThisClass when super is 0, is \"java/lang/Object\"");
    }
  }


  /// <summary>
  /// Handels the parsing for the tags, this will incroment the pointer globaly
  /// </summary>
  /// <param name="tag"> The tag that is being parsed this round </param>
  /// <param name="pointer"> The current position of the array pointer </param>
  /// <param name="bytes"> The array of bites that is being parsed this round </param>
  private IConstantPool ParseConstantPoolTag(byte tag, ref int pointer, ref byte[] bytes) {
    IConstantPool? result = null;

    switch ((E_ConstantPoolTag)tag) {
      case E_ConstantPoolTag.CONSTANT_UTF8:
        UInt16 utf8Length = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolUtf8Info(tag, utf8Length);
        ConstantPoolUtf8Info holder = (ConstantPoolUtf8Info)result;
        GetUtf8Bytes(ref holder, ref pointer, ref bytes);
        result = holder;
      break;

      case E_ConstantPoolTag.CONSTANT_INTEGER:
      case E_ConstantPoolTag.CONSTANT_FLOAT:
        UInt32 numberBytes = Convertor.BytesToUInt32(bytes.Skip(pointer).Take(4));
        pointer += 4;
        result = new ConstantPoolNumberInfo(tag, numberBytes);
      break;

      case E_ConstantPoolTag.CONSTANT_LONG:
      case E_ConstantPoolTag.CONSTANT_DOUBLE:
        UInt32 highBytes = Convertor.BytesToUInt32(bytes.Skip(pointer).Take(4));
        pointer += 4;
        UInt32 lowBytes = Convertor.BytesToUInt32(bytes.Skip(pointer).Take(4));
        pointer += 4;
        result = new ConstantPoolLongDoubleInfo(tag, highBytes, lowBytes);
      break;

      case E_ConstantPoolTag.CONSTANT_CLASS:
        UInt16 classNameIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolClass(tag, classNameIndex);
      break;

      case E_ConstantPoolTag.CONSTANT_STRING:
        UInt16 stringIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolString(tag, stringIndex);
      break;

      case E_ConstantPoolTag.CONSTNAT_FIELDREF:
      case E_ConstantPoolTag.CONSTANT_METHODREF:
      case E_ConstantPoolTag.CONSTANT_INTERFACEMETHODREF:
        UInt16 classIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        UInt16 refNameAndTypeIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolRef(tag, classIndex, refNameAndTypeIndex);
      break;

      case E_ConstantPoolTag.CONSTANT_NAMEANDTYPE:
        UInt16 nameAndTypeNameIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        UInt16 nameAndTypeDescriptorIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolNameAndTypeInfo(tag, nameAndTypeNameIndex, nameAndTypeDescriptorIndex);
      break;

      case E_ConstantPoolTag.CONSTANT_METHODHANDLE:
        E_ReferenceKind referenceKind = (E_ReferenceKind)bytes.Skip(pointer).First();
        pointer++;
        UInt16 referenceIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolMethodHandleInfo((E_ConstantPoolTag)tag, referenceKind, referenceIndex);
      break;

      case E_ConstantPoolTag.CONSTANT_METHODTYPE:
        UInt16 methodTypeDescriptorIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolMethodTypeInfo(tag, methodTypeDescriptorIndex);
      break;

      case E_ConstantPoolTag.CONSTANT_DYNAMIC:
      case E_ConstantPoolTag.CONSTANT_INVOKEDYNAMIC:
        UInt16 bootstrapMethodAttrIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        UInt16 dynamicNameAndTypeIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolDynamicInfo(tag, bootstrapMethodAttrIndex, dynamicNameAndTypeIndex);
      break;

      case E_ConstantPoolTag.CONSTANT_MODULE:
      case E_ConstantPoolTag.CONSTANT_PACKAGE:
        UInt16 packageModuleNameIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
        pointer += 2;
        result = new ConstantPoolPackageModuleInfo(tag, packageModuleNameIndex);
      break;
      default:
        throw new Exception("Unrechable Code");
    }

    return result;
  }

  private FieldsInfo GenerateFieldsInfo(ref int pointer, ref byte[] bytes) {
    UInt16 accessFlags = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    UInt16 nameIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    UInt16 descriptorIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    UInt16 attributesCount = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;

    FieldsInfo result = new FieldsInfo(accessFlags, nameIndex, descriptorIndex, attributesCount);

    if (attributesCount > 0) {
      AttributeInfo attribute = GenerateAttributeInfo(ref pointer, ref bytes);
      result.AddAttributeToAttributeArray(attribute);
    }

    return result;
  }

  /// <summary>
  /// Gets all the bytes for an utf-8 constant pool object
  /// </summary>
  /// <param name="result"> A reference to the ConstantPoolUtf8Info object </param>
  /// <param name="pointer"> A reference to the global array poiner </param>
  /// <param name="bytes"> The array of bytes from the file </param>
  private void GetUtf8Bytes(ref ConstantPoolUtf8Info result, ref int pointer, ref byte[] bytes) {
    for (int i = 0; i < result.Length; i++) {
      byte b = bytes.Skip(pointer).First();
      pointer++;
      result.AddToByteArray(b);
    }
    return;
  }

  /// <summary>
  /// Constructs an interface, using the bytes and pointer from the
  /// Constructor
  /// </summary>
  /// <param name="pointer"> A reference to the current pointer into the bytes array </param>
  /// <param name="bytes"> A reference to the bytes array, that contains the bytes of the file </param>
  private UInt16 GenerateInterface(ref int pointer, ref byte[] bytes) {
    if (ConstantPool == null)
      throw new ArgumentNullException("ConstantPool is currently null");

    UInt16 interfaceIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;

    IConstantPool poolObject = ConstantPool.ElementAt(interfaceIndex);
    if (poolObject.GetTag() != E_ConstantPoolTag.CONSTANT_CLASS)
      throw new Exception($"Unexpected ConstantPool tag.\r\nExpected tag: E_ConstantPoolTag.CONSTANT_CLASS\r\nGot: {poolObject.GetType()}");


    return interfaceIndex;
  }

  /// <summary>
  /// Generates a new instance of the MethodInfo class.
  /// </summary>
  /// <param name="pointer"> A reference to the current array pointer </param>
  /// <param name="bytes"> A reference to the byte array </param>
  private MethodInfo GenerateMethodInfo(ref int pointer, ref byte[] bytes) {
    UInt16 accessFlags = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    UInt16 nameIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    UInt16 descriptorIndex = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    UInt16 attributesCount = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;

    MethodInfo result = new MethodInfo(accessFlags, nameIndex, descriptorIndex, attributesCount);

    for (int i = 0; i < attributesCount; i++) {
      AttributeInfo attributeInfo = GenerateAttributeInfo(ref pointer, ref bytes);
      result.AddAttributeToAttributeArray(attributeInfo);
    }

    return result;
  }

  /// <summary>
  /// Generates a new instance of the AttributeInfo class
  /// </summary>
  /// <param name="pointer"> A reference to the current array pointer </param>
  /// <param name="byte"> A reference to the byte array </param>
  private AttributeInfo GenerateAttributeInfo(ref int pointer, ref byte[] bytes) {
    if (ConstantPool == null) {
      throw new ArgumentNullException("The ConstantPool table is equal to null, there must be a ConstantPool table to get the AttributeInfo");
    }

    UInt16 nameIndexPointer = Convertor.BytesToUInt16(bytes.Skip(pointer).Take(2));
    pointer += 2;
    while (true) {
      IConstantPool item = ConstantPool[nameIndexPointer-1];
      if (item.GetTag() == E_ConstantPoolTag.CONSTANT_UTF8) break;
      else if (item.GetTag() == E_ConstantPoolTag.CONSTANT_CLASS) {
        ConstantPoolClass classItem = (ConstantPoolClass)item;
        nameIndexPointer = classItem.NameIndex;
      }
      else throw new ArgumentException("Could not find a name for the attribute, all attributes must have a pointer to the ConstantPool that results in a ConstantPool_UTF8");
    }

    UInt32 attributeLength = Convertor.BytesToUInt32(bytes.Skip(pointer).Take(4));
    pointer += 4;
    // WARNING: This is fucking stupid, as the non-negative value of UInt32
    // can be greater than the non-negative value of an normal int.
    IEnumerable<byte> info = bytes.Skip(pointer).Take((int)attributeLength);
    pointer += (int)attributeLength;

    AttributeInfo attributeInfo = new AttributeInfo(nameIndexPointer, attributeLength, info);

    return attributeInfo;
  }

  public override string ToString()
  {
    string sep = $",{Environment.NewLine}\t\t",
           prefix = sep.Substring(1);

    string constantPoolString =
        ConstantPool != null && ConstantPool.Length > 0 ? prefix + string.Join(sep, ConstantPool.AsEnumerable()) : "";
    string interfacesString =
        Interfaces != null && Interfaces.Length > 0 ? prefix + string.Join(sep, Interfaces.AsEnumerable()) : "";
    string fieldsInfoString =
        Fields != null && Fields.Length > 0 ? prefix + string.Join(sep, Fields.AsEnumerable()) : "";
    string methodsInfoString =
        Methods != null && Methods.Length > 0 ? prefix + string.Join(sep, Methods.AsEnumerable()) : "";
    string attributesString =
        Attributes != null && Attributes.Length > 0 ? prefix + string.Join(sep, Attributes.AsEnumerable()) : "";

    return $"JavaClass({Environment.NewLine}\tMagicNumber={MagicNumber}, AccessFlags=[{string.Join(", ", AccessFlags)}],{Environment.NewLine}\tMinorVersion={MinorVersion}, MajorVersion={MajorVersion}, ThisClass={ThisClass}, SuperClass={SuperClass},{Environment.NewLine}\tConstantPoolCount={ConstantPoolCount}, ConstantPool=[{constantPoolString}],{Environment.NewLine}\tInterfaceCount={InterfacesCount}, Interfaces=[{interfacesString}],{Environment.NewLine}\tFieldsCount={FieldsCount}, Fields=[{fieldsInfoString}],{Environment.NewLine}\tMethodsCount={MethodsCount}, Methods=[{methodsInfoString}],{Environment.NewLine}\tAttributesCount={AttributesCount}, Attributes=[{attributesString}])";
  }
}
