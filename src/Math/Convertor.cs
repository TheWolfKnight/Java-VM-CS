
using System;
using System.Diagnostics;

namespace CS_Java_VM.Src.Maths.Convertor;

public static class Convertor {
  private const int EXPECTED_BYTE_COUNT_UINT16 = 2;
  private const int EXPECTED_BYTE_COUNT_UINT32 = 4;
  private const int EXPECTED_BYTE_COUNT_UINT64 = 8;
  private const int EXPECTED_BYTE_COUNT_UINT128 = 16;

  #region Bytes to UInt convertors

  /// <summary>
  /// Converts an IEnumerable of 2 bytes to an UInt16
  /// </summary>
  /// <param name="input"> The IEnumerable to be converted into an UInt16 </param>
  public static UInt16 BytesToUInt16(IEnumerable<byte> input) {
    Debug.Assert(input.Count(_ => true) == EXPECTED_BYTE_COUNT_UINT16, $"Expected to get an array of 2 items, got one with {input.Count(_ => true)} items");

    UInt16 r = 0;
    for (int i = 0; i < EXPECTED_BYTE_COUNT_UINT16; i++) {
      r = (UInt16)(r << 8);
      r += input.ElementAt(i);
    }

    return r;
  }

  /// <summary>
  /// Converts an IEnumerable of 4 bytes to an UInt32
  /// </summary>
  /// <param name="input"> The IEnumerable to be converted into an UInt32 </param>
  public static UInt32 BytesToUInt32(IEnumerable<byte> input) {
    Debug.Assert(input.Count(_ => true) == EXPECTED_BYTE_COUNT_UINT32, $"Expected to get an array of 4 items, got one with {input.Count(_ => true)} items");

    UInt32 r = 0;
    for (int i = 0; i < EXPECTED_BYTE_COUNT_UINT32; i++) {
      r = r << 8;
      r += input.ElementAt(i);
    }

    return r;
  }

  public static UInt64 BytesToUInt64(IEnumerable<byte> input) {
    Debug.Assert(input.Count(_ => true) == EXPECTED_BYTE_COUNT_UINT64, $"Expected to get an array of 8 items, got one with {input.Count(_ => true)} items");


    UInt64 r = 0;
    for (int i = 0; i < EXPECTED_BYTE_COUNT_UINT64; i++) {
      r = r << 8;
      r += input.ElementAt(i);
    }
    return r;
  }

  public static UInt128 BytesToUInt128(IEnumerable<byte> input) {
    Debug.Assert(input.Count(_ => true) == EXPECTED_BYTE_COUNT_UINT128, $"Expected to get an array of 16 items, got one with {input.Count(_ => true)} items");

    UInt128 r = 0;
    for (int i = 0; i < EXPECTED_BYTE_COUNT_UINT128; i++) {
      r = r << 8;
      r += input.ElementAt(i);
    }
    return r;
  }

  #endregion

  #region Java number convertors

  public static float UInt32ToJavaFloat(UInt32 input) {
    int s = ((input >> 31) == 0) ? 1 : -1;
    int e = (int)((input >> 23) & 0xFF);
    int m = (int)((e == 0) ?
                    (input & 0x7FFFFF) << 1 :
                    (input & 0x7FFFFF) | 0x800000);

    int h = s * m;
    float h2 = (float)Math.Pow(2, (e-150));
    return h * h2;
  }


  #endregion

}
