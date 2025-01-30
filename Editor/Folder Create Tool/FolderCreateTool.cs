using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class FolderCreateTool
{
    private string _path;

    /// <summary>
    /// Creates folders at path with all the selected objects.
    /// </summary>
    public void Start(UnityEngine.Object[] objects, string outPath, string stripString)
    {
        if (objects == null || objects.Length == 0)
        {
            throw new System.Exception("Search pattern empty.");
        }
        
        if (outPath == "" || outPath == null)
        {
            throw new System.Exception("Outpath empty.");
        }

        foreach (var obj in objects)
        {
            _path = Path.Join(outPath, obj.name);

            if (stripString != null && stripString != "")
            {
                _path = outPath.Replace(stripString, "");
                Assert.IsTrue(!_path.Contains(stripString));
            }


            CreateDirectory(_path);
        }        
    }

    /// <summary>
    /// Create a new directory
    /// </summary>
    /// <param name="path">Directory path.</param>
    public static void CreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return;
        }

        Directory.CreateDirectory(path);
        AssetDatabase.ImportAsset(path);
        AssetDatabase.Refresh();
        
        Assert.IsTrue(Directory.Exists(path));
    }
}