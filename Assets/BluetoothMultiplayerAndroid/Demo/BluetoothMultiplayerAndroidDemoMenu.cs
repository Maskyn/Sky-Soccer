using UnityEngine;
using System.Collections;

public class BluetoothMultiplayerAndroidDemoMenu : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void OnGUI() {
        const float width = 250f;
        const float buttonHeight = 35f;

        const float height = 100f + buttonHeight * 2f;

        GUI.color = Color.white;
        GUILayout.BeginArea(new Rect(Screen.width / 2f - width / 2f,
            Screen.height / 2f - height / 2f, width, height), "Android Bluetooth Multiplayer Demo", "Window");
        GUILayout.BeginVertical();

        GUILayout.Space(10);
        if (GUILayout.Button("Multiplayer demo", GUILayout.Height(buttonHeight)))
            Application.LoadLevel("BluetoothMultiplayerAndroidDemo");
        if (GUILayout.Button("Device discovery demo", GUILayout.Height(buttonHeight)))
            Application.LoadLevel("BluetoothMultiplayerAndroidDiscoveryDemo");

        GUILayout.Space(15);
        GUI.color = new Color(1f, 0.6f, 0.6f, 1f);
        if (GUILayout.Button("Quit", GUILayout.Height(buttonHeight))) {
            Application.Quit();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
