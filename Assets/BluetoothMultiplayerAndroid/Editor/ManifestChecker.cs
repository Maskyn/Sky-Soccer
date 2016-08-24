#if UNITY_ANDROID
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

[InitializeOnLoad]
public class ManifestChecker : EditorWindow {

    static ManifestChecker() {
        EditorApplication.playmodeStateChanged += GenerateManifestIfAbsent;
        GenerateManifestIfAbsent();
    }

    [PostProcessScene]
    static void GenerateManifestIfAbsent() {
        if (!File.Exists(ManifestGenerator.GetManifestPath())) {
            ManifestGenerator.GenerateManifest();

            Debug.Log("AndroidManifest.xml was missing, generated new");
        }   
    }

}
#endif