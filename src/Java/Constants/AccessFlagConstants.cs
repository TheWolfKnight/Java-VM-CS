
using System;

namespace CS_Java_VM.Src.Java.Constants;

public enum E_JavaClassAccessFlags {
  ACC_PUBLIC      = 0x0001,
  ACC_FINAL       = 0x0010,
  ACC_SUPER       = 0x0020,
  ACC_INTERFACE   = 0x0200,
  ACC_ABSTRACT    = 0x0400,
  ACC_SYNTHETIC   = 0x1000,
  ACC_ANNOTATION  = 0x2000,
  ACC_ENUM        = 0x4000,
}

public enum E_MethodAccessFlags {
  ACC_PUBLIC      = 0x0001,
  ACC_PRIVAT      = 0x0002,
  ACC_PROTECTED   = 0x0004,
  ACC_STATIC      = 0x0008,
  ACC_FINAL       = 0x0010,
  ACC_SYNCRONIZED = 0x0020,
  ACC_BRIDGE      = 0x0040,
  ACC_VARARGS     = 0x0080,
  ACC_NATIVE      = 0x0100,
  ACC_ABSTRACT    = 0x0400,
  ACC_STRICT      = 0x0800,
  ACC_SYNTHETIC   = 0x1000,
}

public enum E_FieldAccessFlags {
  ACC_PUBLIC      = 0x0001,
  ACC_PRIVAT      = 0x0002,
  ACC_PROTECTED   = 0x0004,
  ACC_STATIC      = 0x0008,
  ACC_FINAL       = 0x0010,
  ACC_VOLATILE    = 0x0040,
  ACC_TRANSIENT   = 0x0080,
  ACC_SYNTHETIC   = 0x1000,
  ACC_ENUM        = 0x4000,
}
