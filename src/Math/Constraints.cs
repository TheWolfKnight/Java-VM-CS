
using System;
using System.Numerics;

namespace CS_Java_VM.Src.Maths.Constraints;

public static class Constraints<T>
where T : INumber<T>
{
  /// <summary>
  /// Clamps the input <paramref name="num"/> between the numbers <paramref name="low"/> and <paramref name="high"/>
  /// </summary>
  /// <param name="num"> The number that is clamped </param>
  /// <param name="low"> The lowest number acceptable to the function </param>
  /// <param name="high"> The highest number acceptable to the function </param>
  public static T Clamp(T num, T low, T high) {
    if (num.CompareTo(low) <= 0) return low;
    return num.CompareTo(high) <= 0 ? num : high;
  }
}
