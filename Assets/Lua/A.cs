//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
	public class A:MonoBehaviour
	{
//	public static A instance = new A ();
	public static int level;
	public int  age;
		static A ()
		{
		level = 3;
		Debug.Log ("A.static constructor");
			
		}
	public A(){
		age = 10;
		Debug.Log ("A constructor");
	}
	private void test(){
		Debug.Log ("ATest");
	}
}


 
