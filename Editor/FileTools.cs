using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FileTools : EditorWindow 
{
    private int _currentStateIndex = 0;
    GUIStyle _headingStyle;
    private Dictionary<string, FileToolsState> _states;
    public SerializedObject SO;
    public FileToolsCopyTool CopyState;

    void Init()
    {
        if (_states != null)
        {
            return;
        }

        _states = new(){
            {"Copy Tool", new FileToolsCopyTool(this)},
            {"Delete Tool", new FileToolsDeleteTool()},
            {"Rename Tool", new FileToolsRenameTool()},
            {"Folder Creator", new FileToolsFolderCreator()}
        };

        CopyState = _states["Copy Tool"] as FileToolsCopyTool;
    }

    void OnEnable()
    {
        SO = new SerializedObject(this);
        Init();

        _headingStyle = new GUIStyle(EditorStyles.label)
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white }
        };
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("File Tools"), _headingStyle);

        EditorGUILayout.Space(5);
        _currentStateIndex = EditorGUILayout.Popup("Tool :", _currentStateIndex, _states.Keys.ToArray());

        EditorGUILayout.Space(5);
        _states.ElementAt(_currentStateIndex).Value.OnGUI();

        EditorGUILayout.EndVertical();
    }


    [MenuItem("Tools/File Tools")]
    public static void CreateFileToolMenu()
    {
        FileTools menu = GetWindow<FileTools>();
        menu.titleContent = new GUIContent("File Tools");
    }
}