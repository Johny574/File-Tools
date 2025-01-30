using System;
using NUnit.Framework;
using UnityEditor;

public class RenameTool
{
    private RenameToolSettings _settings;
    private string _prefix;
    private string _suffix;

    public RenameTool(RenameToolSettings settings, string prefix, string suffix)
    {
        _settings = settings;
        _prefix = prefix;
        _suffix = suffix;
    }

    /// <summary>
    /// Renames all files.
    /// </summary>
    public void Start(UnityEngine.Object[] objects)
    {
        if (objects == null)
        {
            throw new Exception("Objects empty.");
        }

        AssetDatabase.StartAssetEditing();
        
        string name;
        foreach (var obj in objects)
        {
            // Set name, grab reference to path.
            name = obj.name;
            var objectPath = AssetDatabase.GetAssetPath(obj);
            
            // Set the suffixes.
            _prefix = _settings.Prefix;
            _suffix = _settings.Suffix;
            ReplaceFixes(objectPath);

            // Perform needle in haystack operation.
            name = ReplaceNeedleHaystack(name);
            name = _prefix + name + _suffix;

            // Rename
            RenameAsset(objectPath, objectPath.Replace(obj.name, name), name);     
        }

        // Save
        AssetDatabase.SaveAssets();
        AssetDatabase.StopAssetEditing();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// Apply changes to prefix and suffix.
    /// </summary>
    /// <param name="filePath"></param>
    void ReplaceFixes(string filePath)
    {
        var parentDirectory = filePath.Split("/")[^2];
        
        // Use parent directory for the replace value.
        if (_settings.UseParentNameAsValue)
        {
            _settings.Value = parentDirectory;
            
            Assert.AreEqual(_settings.Value, parentDirectory);
        }

        // Add the parent directory before the prefix.
        if (_settings.AppendParentNameToPrefix)
        {
            _prefix = parentDirectory + _prefix;

            Assert.IsTrue(_prefix.StartsWith(parentDirectory));
        }

        // Add the parent directory after the suffix.
        if (_settings.AppendParentNameToSuffix)
        {
            _suffix = _suffix + parentDirectory;

            Assert.IsTrue(_suffix.EndsWith(parentDirectory));
        }
    }

    /// <summary>
    /// Perform needle in haystack replace operation.
    /// </summary>
    /// <param name="name">Name of file.</param>
    /// <returns></returns>
    public string ReplaceNeedleHaystack(string name)
    {
        string n = name;
        if (_settings.Key != "" && _settings.Key != null)
        {
            n = n.Replace(_settings.Key, _settings.Value);
            Assert.That(!n.Contains(_settings.Value));
        } 
        return n;
    }

    /// <summary>
    /// Rename the assets
    /// </summary>
    /// <param name="path">Current filepath.</param>
    /// <param name="newPath">New filepath.</param>
    /// <param name="newName">New filename.</param>
    void RenameAsset(string path, string newPath, string newName)
    {
        AssetDatabase.MoveAsset(path, newPath);
        AssetDatabase.RenameAsset(path, newName);
    }
}