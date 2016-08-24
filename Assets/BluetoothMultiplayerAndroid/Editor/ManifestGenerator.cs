using System;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ManifestGenerator : EditorWindow {

    [MenuItem("Component/Lost Polygon/Android Bluetooth Multiplayer/Generate new AndroidManifest.xml")]
    public static void GenerateManifest() {
    #if UNITY_ANDROID
        string newManifestFilePath = GetManifestPath();

        if (!File.Exists(newManifestFilePath) || (File.Exists(newManifestFilePath) && EditorUtility.DisplayDialog("Overwrite existing AndroidManifest.xml",
                                        "An existing AndroidManifest.xml file is present. Are you sure you want to overwrite it?",
                                        "Overwrite", "Cancel"))) {
            try {
                string manifest = GetManifestOriginal();
                manifest = PatchManifest(manifest);

                string newManifestPath = Path.GetDirectoryName(newManifestFilePath);

                if (newManifestPath != null)
                    Directory.CreateDirectory(newManifestPath);

                File.WriteAllText(newManifestFilePath, manifest);
                AssetDatabase.Refresh();

                Debug.Log("AndroidManifest.xml generated successfully!");
            } catch (Exception) {
                throw new Exception("Can't generate AndroidManifest.xml!");
            }
        }
    #else
        EditorUtility.DisplayDialog("Wrong build platform", "Build platform is not set to Android. Please choose Android as build Platform in File - Build Settings...", "OK");
    #endif
    }

    [MenuItem("Component/Lost Polygon/Android Bluetooth Multiplayer/Patch existing AndroidManifest.xml")]
    public static void PatchManifest() {
    #if UNITY_ANDROID
        string manifestPath = GetManifestPath();

        if (File.Exists(manifestPath)) {
            try {
                string manifest = File.ReadAllText(manifestPath);
                manifest = PatchManifest(manifest);
                File.WriteAllText(manifestPath, manifest);

                AssetDatabase.Refresh();

                Debug.Log("AndroidManifest.xml patched");
            } catch (Exception) {
                throw new Exception("Can't patch AndroidManifest.xml!");
            }
        } else {
            Debug.LogError("No existing AndroidManifest.xml file was found.");
        }
    #else
        EditorUtility.DisplayDialog("Wrong build platform", "Build platform is not set to Android. Please choose Android as build Platform in File - Build Settings...", "OK");
    #endif
    }

    [MenuItem("Component/Lost Polygon/Android Bluetooth Multiplayer/Add Bluetooth permissions to AndroidManifest.xml")]
    public static void PatchManifestPermission() {
    #if UNITY_ANDROID
        string manifestPath = GetManifestPath();

        if (File.Exists(manifestPath)) {
            try {
                string manifest = File.ReadAllText(manifestPath);
                manifest = PatchManifestPermissions(manifest);
                File.WriteAllText(manifestPath, manifest);

                AssetDatabase.Refresh();

                Debug.Log("AndroidManifest.xml patched");
            } catch (Exception) {
                throw new Exception("Can't patch AndroidManifest.xml!");
            }
        }

    #else
        EditorUtility.DisplayDialog("Wrong build platform", "Build platform was not set to Android. Please choose Android as build Platform in File - Build Settings...", "OK");
    #endif
    }

    #if UNITY_ANDROID
    static string GetManifestOriginal() {
        try {
            string unityPath = EditorApplication.applicationContentsPath;
            char slash = Path.DirectorySeparatorChar;

            string manifestPath = unityPath + slash + "PlaybackEngines" + slash + "androidplayer" + slash +
                                  "AndroidManifest.xml";
            string manifest = File.ReadAllText(manifestPath);

            return manifest;
        } catch (Exception) {
            throw new Exception("Error getting default AndroidManifest.xml!");
        }
    }

    public static string GetManifestPath() {
        char slash = Path.DirectorySeparatorChar;
        string manifestPath = Application.dataPath + slash + "Plugins" + slash + "Android";
        string manifestFilePath = manifestPath + slash + "AndroidManifest.xml";

        return manifestFilePath;
    }

    private static string PatchManifest(string manifest) {
        string[] replaceFrom = new[] {
                                       "android:name=\"com.unity3d.player.UnityPlayerProxyActivity\"",
                                       "android:name=\"com.unity3d.player.UnityPlayerActivity\"",
                                       "android:name=\"com.unity3d.player.UnityPlayerNativeActivity\"",

                                       "<uses-permission android:name=\"android.permission.BLUETOOTH_ADMIN\"/>",
                                       "<uses-permission android:name=\"android.permission.BLUETOOTH\"/>",

                                       "</manifest>"
                                     };
        string[] replaceTo = new[] {
                                       "android:name=\"biz.zimm.unity.bluetoothmediator.player.BluetoothUnityPlayerProxyActivity\"",
                                       "android:name=\"biz.zimm.unity.bluetoothmediator.player.BluetoothUnityPlayerActivity\"",
                                       "android:name=\"biz.zimm.unity.bluetoothmediator.player.BluetoothUnityPlayerNativeActivity\"",

                                       "",
                                       "",
                                       
                                       "  <uses-permission android:name=\"android.permission.BLUETOOTH_ADMIN\"/>\r\n" +
                                       "  <uses-permission android:name=\"android.permission.BLUETOOTH\"/>\r\n" +
                                       "</manifest>"
                                     };

        for (int i = 0; i < replaceFrom.Length; i++) {
            manifest = manifest.Replace(replaceFrom[i], replaceTo[i]);
        }

        return manifest;
    }


    private static string PatchManifestPermissions(string manifest) {
        string[] replaceFrom = new[] {
                                       "<uses-permission android:name=\"android.permission.BLUETOOTH_ADMIN\"/>",
                                       "<uses-permission android:name=\"android.permission.BLUETOOTH\"/>",

                                       "</manifest>"
                                     };
        string[] replaceTo = new[] {
                                       "",
                                       "",
                                       
                                       "  <uses-permission android:name=\"android.permission.BLUETOOTH_ADMIN\"/>\r\n" +
                                       "  <uses-permission android:name=\"android.permission.BLUETOOTH\"/>\r\n" +
                                       "</manifest>"
                                     };

        for (int i = 0; i < replaceFrom.Length; i++) {
            manifest = manifest.Replace(replaceFrom[i], replaceTo[i]);
        }

        return manifest;
    }
    #endif

}