using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class FileToolsDeleteTool : FileToolsState
{
    private string _searchPattern = "";
    private string _directory = Application.dataPath;

    public override void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Settings"));

        EditorGUILayout.Space(5);
        GUILayout.Label(new GUIContent("Search Pattern"));
        _searchPattern = GUILayout.TextArea(_searchPattern);

        GUILayout.Label(new GUIContent("Directory"));
        _directory = GUILayout.TextArea(_directory);

        if (GUILayout.Button("Change directory"))
        {
            _directory = EditorUtility.OpenFolderPanel("Change Directory", _directory, "");
        }

        GUILayout.Space(5);
        if (GUILayout.Button("Log (Recommended)"))
        {
            Log();
        }

        GUILayout.Space(5);
        if (GUILayout.Button("Start"))
        {
            Start();
        }
        EditorGUILayout.EndVertical();
    }

    private void Log()
    {
        // Clear Console
        Assembly.GetAssembly(typeof(SceneView)).GetType("UnityEditor.LogEntries").GetMethod("Clear").Invoke(new object(), null);
        
        string[] files = SearchFiles();

        if (files == null)
        {
            return;
        }

        for (int i = 0; i < files.Length; i++)
        {
            Debug.Log(files[i]);
        }
    }

    public override void Start()
    {
        // Clear Console
        Assembly.GetAssembly(typeof(SceneView)).GetType("UnityEditor.LogEntries").GetMethod("Clear").Invoke(new object(), null);
        
        string[] files = SearchFiles();

        if (files == null)
        {
            return;
        }

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
            Debug.Log($"Removing {files[i]}");
        }   
    }

    public string[] SearchFiles()
    {
        if (_searchPattern == "")
        {
            Debug.Log("Search pattern can not be empty.");
            return null;
        }

        if (_directory == "")
        {
            Debug.Log("No directory given.");
            return null;
        }

        string[] files = Directory.GetFiles(_directory, _searchPattern, SearchOption.AllDirectories);

        if (files.Length <= 0)
        {
            Debug.Log("Couldn't find any files.");
            return null;
        }

        return files;
    }

  
}