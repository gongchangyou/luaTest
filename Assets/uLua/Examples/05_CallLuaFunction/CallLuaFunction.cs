using UnityEngine;
using System.Collections;
using LuaInterface;

public class CallLuaFunction : MonoBehaviour {

    private string script = @"
            function luaFunc(message)
                print(message)
                return 42
            end
        ";

	// Use this for initialization
	void Start () {
		//test
		/*
		A a1 = new A ();
		a1.age = 5;
		A a2 = new A ();
		a2 = a1;
		a2.age = 15;
		Debug.Log ("a1.age=" + a1.age);

		int x = new int ();
		x = 3;
		int y = new int ();
		y = x;
		y = 4;
		Debug.Log ("x=" + x);
		Debug.Log( string.Format ("_DECk {0:F2} {0:F2}", 1243.234f,345.234f));
		Debug.Log( string.Format ("{0:D2}", 1));
		*/
        LuaState l = new LuaState();

        // First run the script so the function is created
        l.DoString(script);

        // Get the function object
        LuaFunction f = l.GetFunction("luaFunc");

        // Call it, takes a variable number of object parameters and attempts to interpet them appropriately
        object[] r = f.Call("I called a lua function!");

        // Lua functions can have variable returns, so we again store those as a C# object array, and in this case prinSt the first one
        print(r[0]);
//		SendMessage ("test");



	}

	// Update is called once per frame
	void Update () {

	}
}
