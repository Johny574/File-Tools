using System;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CopyTool 
{
    
    /// <summary>
    /// Iterates over directories and creates a copy of all selected objects in each directory.
    /// </summary>
    public void Start(UnityEngine.Object[] directories, UnityEngine.Object[] objects)
    {
        if (directories == null || directories.Length == 0)
        {
            throw new Exception("Directories empty.");
        }

        if (objects == null || objects.Length == 0)
        {
            throw new Exception("Objects empty."); 
        }

        AssetDatabase.StartAssetEditing();
        foreach (var dir in directories)
        {
            foreach (var obj in objects)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                var splitPath = path.Split("/");
                var newPath = Path.Join(AssetDatabase.GetAssetPath(dir), splitPath[^1]);
                File.Copy(path, newPath, overwrite: true);
                AssetDatabase.ImportAsset(newPath);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.StopAssetEditing();
        AssetDatabase.Refresh();

        
    }
}