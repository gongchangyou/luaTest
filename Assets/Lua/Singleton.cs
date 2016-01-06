using UnityEngine;
using System.Collections;

public class Singleton<T> where T:new()
{
	private static T instance;
	public static T I {
		get {
			if (instance == null) {
				instance = new T();
 
				if (instance == null) {
					Debug.LogError (typeof(T) + " is nothing");
				}
			}
 
			return instance;
		}
	}
}