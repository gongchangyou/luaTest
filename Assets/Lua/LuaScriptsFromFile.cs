using UnityEngine;
using System.Collections;
using LuaInterface;
using System.IO;
public class LuaScriptsFromFile : MonoBehaviour {
	private string luaFileName = "lua.unity3d";
	public string luaAssetsPath;
	private string filePath;
    //public TextAsset scriptFile;

	// Use this for initialization
	void Start () {
		Debug.Log ("start");

	}

	void Awake(){
		luaAssetsPath = Application.temporaryCachePath + "/LuaAssets/";
		filePath = Path.Combine (luaAssetsPath, luaFileName);
		Debug.Log ("awake");

		System.Action<AssetBundle> onComplete = (AssetBundle luaAsset) => {
			//TextAsset scriptText = obj.Load ("04_ScriptsFromFile.lua.txt") as TextAsset;
			LuaState l = new LuaState();
			
			TextAsset luaText = luaAsset.Load("04_ScriptsFromFile") as TextAsset;
			if(luaText != null)
			l.DoString(luaText.text);
			
			TextAsset luaText1 = luaAsset.Load("041_ScriptsFromFile") as TextAsset;
			if(luaText1 != null)
			l.DoString(luaText1.text);
		};
		
		string path = "";
		bool isReplace = false;
		#if Local_Lua
		filePath = Application.streamingAssetsPath + "/LuaAssets/" + luaFileName;
		#endif
		if (File.Exists (filePath)) {
			path = "file://" + filePath;
		} else {
			path = "http://gongchangyou-download.stor.sinaapp.com/" + luaFileName;
			isReplace = true;
		}
		StartCoroutine (AsyncScript (path, isReplace, onComplete));
	}

	public IEnumerator AsyncScript (string path, bool isReplace, System.Action<AssetBundle> onComplete) {

		Debug.LogError ("path: " + path);
		WWW www = new WWW (path);

		while (!www.isDone)
			yield return new WaitForEndOfFrame ();

		if (!string.IsNullOrEmpty (www.error)) {
			Debug.LogError ("error: " + www.error);
			yield break;   
		}

		//save assetBundle TODO
		if (!Directory.Exists (luaAssetsPath)) {
			Directory.CreateDirectory(luaAssetsPath);
		}
		if (isReplace) {
			string tempPath = Path.Combine (luaAssetsPath, System.Convert.ToString (Time.time * 1000));

			File.WriteAllBytes (tempPath, www.bytes);
			if (File.Exists (filePath)) {
				File.Delete (filePath);
			}
			File.Move (tempPath, filePath);
		}

		Debug.LogError (www.assetBundle);
		onComplete (www.assetBundle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
