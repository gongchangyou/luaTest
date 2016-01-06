using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class AssetBundleTool
{
#if true
	// コマンド
	[MenuItem("Assets/Export Asset Bundle")]
	static void ExportSelection(){

		var assets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);//Assets/path/to/file
		var assetObjs = new List<Object> ();
		var explicitAssetNames = new List<string> ();
		foreach (var asset in assets) {
			var path = AssetDatabase.GetAssetPath(asset);

			var ext = System.IO.Path.GetExtension(path);
//			var exportName = path.Replace("/", "_").Replace(".", "_");
			var exportName = Path.GetFileName (path);
			exportName = exportName.Substring (0, exportName.IndexOf ("."));
			Debug.LogError ("Export Name:" + exportName);
			if(ext == ".meta")
				continue;
			try{
				var assetObj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
				assetObjs.Add(assetObj);
				explicitAssetNames.Add(exportName);
			}catch{
				Debug.LogError("load asset " + path + " fail");
			}	
		}

		var filePath = Application.streamingAssetsPath + "/LuaAssets/";
		var exportPath = Path.Combine( filePath, "lua.unity3d");
		
		BuildPipeline.PushAssetDependencies();
		BuildPipeline.BuildAssetBundleExplicitAssetNames(assetObjs.ToArray(), explicitAssetNames.ToArray(), exportPath,
		                                                 BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.iPhone);
		BuildPipeline.PopAssetDependencies();


//		AssetDatabase.Refresh();//这个要很久
		// AssetBundleが更新されたらローカルキャッシュをクリアする
		Caching.CleanCache();

		
//			string sourceVersionRoot = App.Path.ExternalResources + "_Version/" + NetworkManager.PLATFORM_SYMBOL;
//			string targetVersionRoot = App.Path.ProjectRoot + "Assets/App/Resources/_Version/" + NetworkManager.PLATFORM_SYMBOL;
//			FileUtil.DeleteFileOrDirectory(targetVersionRoot);
//			FileUtil.CopyFileOrDirectory(sourceVersionRoot, targetVersionRoot);

	}

	
	// ファイルを検索
	public static List<string> SearchFiles(string path, bool recursive = true)
	{
		List<string> ret = new List<string>();
		try
		{
			foreach(string file in System.IO.Directory.GetFiles(path))
			{
				ret.Add(file);
			}
			if (recursive)
			{
				foreach(string dir in System.IO.Directory.GetDirectories(path))
				{
					ret.AddRange(SearchFiles(dir, recursive));
				}
			}
		}
		catch
		{
		}
		return ret;
	}


#endif
}
