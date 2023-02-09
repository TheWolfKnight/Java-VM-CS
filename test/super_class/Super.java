package test.super_class;

public class Super<Tkey, Tvalue> {
  public int calcFib(int n) {
    if (n == 1) return 1;
    return calcFib(n-1) + calcFib(n - 2);
  }

  public boolean KeyIsSame(Tkey a, Tvalue b) {
    return a == b;
  }

}
