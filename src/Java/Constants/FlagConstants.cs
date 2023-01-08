
using System;

namespace CS_Java_VM.Src.Java.Constants;

public enum E_AccessFlags {
  ACC_PUBLIC      = 0x001,
  ACC_FINAL       = 0x0010,
  ACC_SUPER       = 0x0020,
  ACC_INTERFACE   = 0x0200,
  ACC_ABSTRACT    = 0x0400,
  ACC_SYNTHETIC   = 0x1000,
  ACC_ANNOTATION  = 0x2000,
  ACC_ENUM        = 0x4000,
  ACC_MODULE      = 0x8000,
}