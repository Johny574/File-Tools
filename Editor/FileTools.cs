using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class FileTools : EditorWindow 
{
    private int _currentStateIndex = 0;
    private GUIStyle _headingStyle;
    private Dictionary<string, ToolMenu> _menus;
    public CopyToolMenu CopyTool;
    
    void OnEnable()
    {
        Init();

        _headingStyle = new GUIStyle(EditorStyles.label)
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white }
        };
    }

    void Init()
    {
        _menus = new()
        {
            {"Copy Tool", new CopyToolMenu(new SerializedObject(this))},
            {"Delete Tool", new DeleteToolMenu()},
            {"Rename Tool", new RenameToolMenu()},
            {"Folder Creator", new FolderCreateToolMenu()}
        };

        CopyTool = _menus["Copy Tool"] as CopyToolMenu;
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(new GUIContent("File Tools"), _headingStyle);

        EditorGUILayout.Space(5);
        _currentStateIndex = EditorGUILayout.Popup("Tool :", _currentStateIndex, _menus.Keys.ToArray());

        EditorGUILayout.Space(5);
        _menus.ElementAt(_currentStateIndex).Value.OnGUI();

        EditorGUILayout.EndVertical();
    }


    [MenuItem("Tools/File Tools")]
    public static void CreateFileToolMenu()
    {
        FileTools menu = GetWindow<FileTools>();
        menu.titleContent = new GUIContent("File Tools");
    }
}