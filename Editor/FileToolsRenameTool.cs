using UnityEditor;
using UnityEngine;

public class FileToolsRenameTool : FileToolsState
{
    private string _key = "";
    private string _value = "";

    private string _inputPrefix = "";
    private string _preffix;

    private string _inputSuffix = "";
    private string _suffix;

    private bool _useParentNameAsValue = false;
    private bool _addParentNameToPreffix = false;
    private bool _addParentNameToSuffix= false;

    public override void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Settings"));

        EditorGUILayout.Space(5);
        GUILayout.Label("Key");
        _key = GUILayout.TextField(_key);
        
        GUILayout.Label("Value");
        _value = GUILayout.TextField(_value);

        GUILayout.Label(new GUIContent("Prefix", "Prefix to be added to filename."));
        _inputPrefix = GUILayout.TextField(_inputPrefix);
        
        GUILayout.Label(new GUIContent("Suffix", "Suffix to be added to filename."));
        _inputSuffix = GUILayout.TextField(_inputSuffix);

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("Additional settings."));
        
        EditorGUILayout.Space(5);
        _useParentNameAsValue = GUILayout.Toggle(_useParentNameAsValue, new GUIContent(" Use parent directory name for value field.", "Use the parent directory name for the value field."));
        _addParentNameToPreffix = GUILayout.Toggle(_addParentNameToPreffix, new GUIContent(" Add parent directory name to preffix field.", "Add the parent directory name to the preffix field."));
        _addParentNameToSuffix = GUILayout.Toggle(_addParentNameToSuffix, new GUIContent(" Add parent directory name to suffix field.", "Add the parent directory name to the suffix field."));
        
        EditorGUILayout.Space(5);
        if (GUILayout.Button("Start"))
        {
            Start();
        }
        EditorGUILayout.EndVertical();
    }

    public override void Start()
    {
        AssetDatabase.StartAssetEditing();
        
        string _newName;

        foreach (var obj in Selection.objects)
        {
            _newName = obj.name;
            _preffix = _inputPrefix;
            var objectPath = AssetDatabase.GetAssetPath(obj);

            ProcessObjectData(objectPath);

            // Replace key with value
            if (_key != "" && _key != null)
            {
                _newName = _newName.Replace(_key, _value);   
            } 

            // Add prefix and suffix
            _newName = _preffix + _newName + _inputSuffix;

            // Rename
            RenameAsset(objectPath, objectPath.Replace(obj.name, _newName), _newName);     
        }

        AssetDatabase.StopAssetEditing();
    }

    void ProcessObjectData(string objectPath)
    {
        var parentDir = GetParentDirectory(objectPath).Split("/");
        if (_useParentNameAsValue)
        {
            _value = parentDir[^1];
        }

        if (_addParentNameToPreffix)
        {
            _preffix = parentDir[^1] + _preffix;
        }

        if (_addParentNameToSuffix)
        {
            _suffix = _suffix + parentDir[^1];
        }
    }

    void RenameAsset(string path, string newPath, string newName)
    {
        AssetDatabase.MoveAsset(path, newPath);
        AssetDatabase.RenameAsset(path, newName);
    }

    public static string GetParentDirectory(string path, string delimiter = "/") => string.Join(delimiter, path.Split(delimiter)[..^1]);
}