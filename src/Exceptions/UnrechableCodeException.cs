
namespace CS_Java_VM.Src.Exceptions;

public class UnrechableCodeException : Exception {
  public UnrechableCodeException() : base() {}
  public UnrechableCodeException(string? message) : base(message) {}
  public UnrechableCodeException(string? message, Exception? inner) : base(message, inner) {}
}
