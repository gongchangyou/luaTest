using UnityEngine;
using System.Collections;
using LuaInterface;
using System.IO;
public class LuaScriptsFromFile : MonoBehaviour {
	private string luaFileName = "lua.unity3d";
	public static string luaAssetsPath = Application.temporaryCachePath + "/LuaAssets/";
    //public TextAsset scriptFile;

	// Use this for initialization
	void Start () {
		string filePath = Path.Combine (luaAssetsPath, luaFileName);
		System.Action<AssetBundle> onComplete = (AssetBundle luaAsset) => {
			//TextAsset scriptText = obj.Load ("04_ScriptsFromFile.lua.txt") as TextAsset;
			Debug.LogError ("luaState: " + (3 == 4));
			LuaState l = new LuaState();
			
			TextAsset luaText = luaAsset.Load("04_ScriptsFromFile") as TextAsset;
			if(luaText != null)
			l.DoString(luaText.text);
			
			TextAsset luaText1 = luaAsset.Load("041_ScriptsFromFile") as TextAsset;
			if(luaText1 != null)
			l.DoString(luaText1.text);
		};

		string path = "";
		filePath = Application.streamingAssetsPath + "/LuaAssets/" + luaFileName;
		if (File.Exists (filePath)) {
			path = "file://" + filePath;
		} else {
			path = "http://dev01.wcat.gumichina.com/" + filePath;
		}
		StartCoroutine (AsyncScript (path, onComplete));
	}

	public IEnumerator AsyncScript (string path, System.Action<AssetBundle> onComplete) {

		Debug.LogError ("path: " + path);
		WWW www = new WWW (path);

		while (!www.isDone)
			yield return new WaitForEndOfFrame ();

		if (!string.IsNullOrEmpty (www.error)) {
			Debug.LogError ("error: " + www.error);
			yield return null;   
		}

		Debug.LogError (www.assetBundle);
		onComplete (www.assetBundle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
