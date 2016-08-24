using System;
using UnityEditor;
using UnityEngine;

public class UuidGenerator : EditorWindow
{

    [MenuItem("Component/Lost Polygon/Android Bluetooth Multiplayer/UUID generator")]
    static void ShowWindow()
    {
    #if UNITY_ANDROID
        Vector2 size = new Vector2(300f, 80f);
        Rect rect = new Rect(Screen.width / 2f - size.x, Screen.height / 2f - size.y, size.x, size.y); 
        EditorWindow window = EditorWindow.GetWindowWithRect<UuidGenerator>(rect, true);
        window.minSize = window.maxSize = size;
        window.title = "UUID Generator";
    #else
        EditorUtility.DisplayDialog("Wrong build platform", "Build platform was not set to Android. Please choose Android as build Platform in File - Build Settings...", "OK");
    #endif
    }

#if UNITY_ANDROID
    private string _uuid = "";

    void OnGUI()  {
        EditorGUILayout.LabelField("Random generated UUID: ");
        _uuid = EditorGUILayout.TextField(_uuid);

        if (GUILayout.Button("Generate new UUID") || _uuid == "") {
            _uuid = Guid.NewGuid().ToString();
            Repaint();
        }
    }
#endif
}

