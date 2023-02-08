package test.super_class;

public class Super {
  public int calcFib(int n) {
    if (n == 1) return 1;
    return calcFib(n-1) + calcFib(n - 2);
  }
}
