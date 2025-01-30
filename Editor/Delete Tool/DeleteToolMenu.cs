using UnityEditor;
using UnityEngine;

public class DeleteToolMenu : ToolMenu
{
    private string _directory = Application.dataPath;
    private DeleteTool _deleteTool;
    string _searchPattern;

    public DeleteToolMenu()
    {
        _deleteTool = new DeleteTool();
    }

    public override void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        // Heading.
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Settings"));

        // Search pattern.
        EditorGUILayout.Space(5);
        GUILayout.Label(new GUIContent("Search Pattern"));
        _searchPattern = GUILayout.TextArea(_searchPattern);

        // Directory display.
        EditorGUILayout.Space(5);
        GUILayout.Label(new GUIContent("Directory"));
        _directory = GUILayout.TextArea(_directory);

        // Change directory button.
        EditorGUILayout.Space(5);
        if (GUILayout.Button("Change directory"))
        {
            _directory = EditorUtility.OpenFolderPanel("Change Directory", _directory, "");
        }

        // Log files button.
        GUILayout.Space(5);
        if (GUILayout.Button("Log (Recommended)"))
        {
            _deleteTool.Log(_searchPattern, _directory);
        }

        // Start button.
        GUILayout.Space(5);
        if (GUILayout.Button("Start"))
        {
            _deleteTool.Start(_searchPattern, _directory);
        }

        EditorGUILayout.EndVertical();
    }
}