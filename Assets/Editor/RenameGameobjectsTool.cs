using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RenameGameobjectsTool : EditorWindow
{
    string newName;
    private static EditorWindow window;

    [MenuItem("Tools/Rename Selected Gameobjects")]
    private static void RenameSelectedItems()
    {
        window = CreateInstance<RenameGameobjectsTool>();
        var position = window.position;
        position.center = new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;
        window.position = position;
        window.Show();
    }
    
    private void OnGUI()
    {
        GUILayout.Space(10);
        newName = EditorGUILayout.TextField("Name", newName);

        if (GUILayout.Button("Set Name"))
        {
            RenameSelectedGameObjects();
            window.Close();
        }
    }

    private void RenameSelectedGameObjects()
    {
        GameObject[] selectedGameobjects = Selection.gameObjects;

        foreach(GameObject go in selectedGameobjects)
        {
            go.name = newName;
        }

        newName = "";
    }
}
