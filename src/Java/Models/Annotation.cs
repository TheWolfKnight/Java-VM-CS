
using System;

namespace CS_Java_VM.Src.Java.Models;


public class Annotation {
  public int ArrayPointer = 0;

  public UInt16 TypeIndex;
  public UInt16 NumberOfValuePairs;
  public ElementValue[] ElementValuePairs;

  public Annotation(UInt16 typeIndex, UInt16 numberOfValuePairs) {
    TypeIndex = typeIndex;
    NumberOfValuePairs = numberOfValuePairs;

    ElementValuePairs = new ElementValue[numberOfValuePairs];
  }

  public void AddElementValueToArray(ElementValue element) {
    if (ArrayPointer == NumberOfValuePairs)
      throw new Exception("Element out of index");

    ElementValuePairs[ArrayPointer] = element;
    ArrayPointer++;
  }
}


public struct ElementValue {
}
