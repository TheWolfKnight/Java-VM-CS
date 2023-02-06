package test;

import test.sub_folder.enum_class;

public class Test {
  public static void test1() {}
  private void test2() {}
  protected String returnHello() { return "Hello";}

  float n = .3f;
  static boolean yesno = true;
  public int a = 0;
  private static String b = "Hello, world";

  public static void main(String[] args) {
    System.out.println(b);
    enum_class test = enum_class.TEST;
  }
}
