using System.IO;
using UnityEditor;
using UnityEngine;

public class FileToolsFolderCreator : FileToolsState
{
    private string _stripString = "";
    private string _outputPath;
    private string _pathHandle = "";

    public override void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space(5);
        GUILayout.Label("Strip String");
        _stripString = GUILayout.TextField(_stripString);

        EditorGUILayout.Space(5);
        GUILayout.Label("Path");
        _outputPath = GUILayout.TextField(_outputPath);

        if (GUILayout.Button("Set path"))
        {
            _outputPath = EditorUtility.OpenFolderPanel("", "", "");
        }

        if (GUILayout.Button("Start"))
        {
            Start();
        }
        EditorGUILayout.EndVertical();
    }

    public override void Start()
    {
        foreach (var obj in Selection.objects)
        {
            _pathHandle = "";
            _pathHandle = Path.Join(_outputPath, obj.name.Replace(_stripString, ""));
            CreateDirectory(_pathHandle);
        }        
    }

    public static void CreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return;
        }

        Directory.CreateDirectory(path);
    }
}