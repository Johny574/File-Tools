using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CopyToolMenu : ToolMenu
{
    public UnityEngine.Object[] Directories = new UnityEngine.Object[0];
    private CopyTool _copyTool;
    private SerializedObject _sO;

    public CopyToolMenu(SerializedObject sO)
    {
        _sO = sO;
        _copyTool = new();
    }

    public override void OnGUI()
    {
        // Heading.
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Settings"));

        // Output directories.
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(_sO.FindProperty("CopyTool.Directories"), true);

        // Start button.
        EditorGUILayout.Space(5);
        if (GUILayout.Button("Start"))
        {
            _copyTool.Start(Directories, Selection.objects);
        }
        
        EditorGUILayout.EndVertical();
        _sO.ApplyModifiedProperties();
    } 

   
}