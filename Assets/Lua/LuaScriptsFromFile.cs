using UnityEngine;
using System.Collections;
using LuaInterface;
using System.IO;
public class LuaScriptsFromFile : MonoBehaviour {
	public static string luaAssetsPath = Application.streamingAssetsPath + "/LuaAssets/";
    //public TextAsset scriptFile;

	// Use this for initialization
	void Start () {

		StartCoroutine (AsyncScript ("lua.unity3d", delegate(AssetBundle luaAsset) {
			//TextAsset scriptText = obj.Load ("04_ScriptsFromFile.lua.txt") as TextAsset;
			Debug.LogError ("luaState: " + (3 == 4));
			LuaState l = new LuaState();

			TextAsset luaText = luaAsset.Load("04_ScriptsFromFile") as TextAsset;
			if(luaText != null)
				l.DoString(luaText.text);

			TextAsset luaText1 = luaAsset.Load("041_ScriptsFromFile") as TextAsset;
			if(luaText1 != null)
				l.DoString(luaText1.text);

		}));
	}

	public IEnumerator AsyncScript (string luaFileName, System.Action<AssetBundle> onComplete) {
		string path = "file://" + luaAssetsPath + luaFileName;
		Debug.LogError ("path: " + path);
		WWW www = new WWW (path);

		while (!www.isDone)
			yield return new WaitForEndOfFrame ();

		if (!string.IsNullOrEmpty (www.error))
			Debug.LogError ("error: " + www.error);

		Debug.LogError (www.assetBundle);
		onComplete (www.assetBundle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
