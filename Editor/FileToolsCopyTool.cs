using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class FileToolsCopyTool : FileToolsState
{
    public UnityEngine.Object[] Directories = new UnityEngine.Object[0];
    UnityEngine.Object[] _selection;
    private FileTools _window;

    public FileToolsCopyTool(FileTools window)
    {
        _window = window;
    }

    public override void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Settings"));

        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(_window.SO.FindProperty("CopyState.Directories"), true);

        EditorGUILayout.Space(5);
        if (GUILayout.Button("Start"))
        {
            Start();
        }
        EditorGUILayout.EndVertical();
        _window.SO.ApplyModifiedProperties();
    }

    public override void Start()
    {
        _selection = Selection.objects;
        AssetDatabase.StartAssetEditing();
        foreach (var dir in Directories)
        {
            foreach (var obj in _selection)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                var splitPath = path.Split("/");
                var newPath = Path.Join(AssetDatabase.GetAssetPath(dir), splitPath[splitPath.Length-1]);
                AssetDatabase.CopyAsset(path, newPath);
                Debug.Log(newPath);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.StopAssetEditing();
        AssetDatabase.Refresh();
    }
}