using UnityEditor;
using UnityEngine;

public class RenameToolMenu : ToolMenu
{
    private RenameToolSettings _settings;
    private RenameTool _renameTool;

    public RenameToolMenu()
    { 
        _settings = new RenameToolSettings("", "", "", "", false, false, false);
        _renameTool = new(_settings, "", "");
    }

    public override void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        
        // Heading.
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Settings"));

        // Key.
        EditorGUILayout.Space(5);
        GUILayout.Label("Key");
        _settings.Key = GUILayout.TextField(_settings.Key);
        
        // Value.
        GUILayout.Label("Value");
        _settings.Value = GUILayout.TextField(_settings.Value);

        // Prefix.
        GUILayout.Label(new GUIContent("Prefix", "Prefix to be added to filename."));
        _settings.Prefix = GUILayout.TextField(_settings.Prefix);
        
        // Suffix.
        GUILayout.Label(new GUIContent("Suffix", "Suffix to be added to filename."));
        _settings.Suffix = GUILayout.TextField(_settings.Suffix);

        // Additional settings label.
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Additional settings."));
        
        // Additional settings.
        EditorGUILayout.Space(5);
        _settings.UseParentNameAsValue = GUILayout.Toggle(_settings.UseParentNameAsValue, new GUIContent(" Use parent directory name for value field.", "Use the parent directory name for the value field."));
        _settings.AppendParentNameToPrefix = GUILayout.Toggle(_settings.AppendParentNameToPrefix, new GUIContent(" Add parent directory name to preffix field.", "Add the parent directory name to the preffix field."));
        _settings.AppendParentNameToSuffix = GUILayout.Toggle(_settings.AppendParentNameToSuffix, new GUIContent(" Add parent directory name to suffix field.", "Add the parent directory name to the suffix field."));
        
        // Start button.
        EditorGUILayout.Space(5);
        if (GUILayout.Button("Start"))
        {
            _renameTool.Start(Selection.objects);
        }

        EditorGUILayout.EndVertical();
    }

}