using UnityEditor;
using UnityEngine;

class FolderCreateToolMenu : ToolMenu
{
    private FolderCreateTool _folderCreateTool;
    string _stripString, _outputPath;

    public FolderCreateToolMenu()
    {
        _folderCreateTool = new();
    }

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
            _folderCreateTool.Start(Selection.objects, _outputPath, _stripString);
        }

        EditorGUILayout.EndVertical();
    }
}