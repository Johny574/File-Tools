using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class DeleteTool
{
    /// <summary>
    /// Logs all the files found.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Log(string searchPattern, string directory)
    {
        if (searchPattern == "" || searchPattern == null)
        {
            throw new System.Exception("Search pattern empty.");
        }
        
        if (directory == "" || directory == null)
        {
            throw new System.Exception("Directory empty.");
        }

        // Clear the console.
        Assembly.GetAssembly(typeof(SceneView)).GetType("UnityEditor.LogEntries").GetMethod("Clear").Invoke(new object(), null);
        
        string[] files = SearchFiles(searchPattern, directory);

        if (files == null || files.Length == 0)
        {
            throw new System.Exception("Couldn't find any files.");
        }

        for (int i = 0; i < files.Length; i++)
        {
            Debug.Log(files[i]);
        }
    }

    /// <summary>
    /// Delete's all the files found.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Start(string searchPattern, string directory)
    {
        if (searchPattern == "" || searchPattern == null)
        {
            throw new System.Exception("Search pattern empty.");
        }
        
        if (directory == "" || directory == null)
        {
            throw new System.Exception("Directory empty.");
        }

        // Clear the console.
        Assembly.GetAssembly(typeof(SceneView)).GetType("UnityEditor.LogEntries").GetMethod("Clear").Invoke(new object(), null);
        
        string[] files = SearchFiles(searchPattern, directory);

        if (files == null || files.Length == 0)
        {
            throw new System.Exception("Couldn't find any files.");
        }

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
            Debug.Log($"Removing {files[i]}");

            Assert.IsTrue(!File.Exists(files[i]));
        }   
    }

    /// <summary>
    /// Searches for files by given search pattern.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string[] SearchFiles(string searchPattern, string directory)
    {
        if (searchPattern == "")
        {
            throw new System.Exception("Search pattern can not be empty.");
        }

        if (directory == "")
        {
            throw new System.Exception("No directory given.");
        }

        string[] files = Directory.GetFiles(directory, searchPattern, SearchOption.AllDirectories);

        if (files.Length <= 0)
        {
            throw new System.Exception("Couldn't find any files.");
        }

        return files;
    }  
}