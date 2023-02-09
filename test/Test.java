package test;

import java.util.ArrayList;

import test.sub_folder.enum_class;
import test.super_class.Super;

class Test {
  public static void test1() {}
  private void test2() {}
  protected String returnHello() { return "Hello";}

  float n = .3f;
  static boolean yesno = true;
  public static int a = 7;
  private static String b = "Hello, world";
  ArrayList<Integer> t = new ArrayList<Integer>();
  static Super<Boolean, Float> s = new Super<Boolean, Float>();

  public static void main(String[] args) {
    s.calcFib(a);
    s.KeyIsSame(true, 0.1f);
    System.out.println(b);
    enum_class test = enum_class.TEST;
  }
}
