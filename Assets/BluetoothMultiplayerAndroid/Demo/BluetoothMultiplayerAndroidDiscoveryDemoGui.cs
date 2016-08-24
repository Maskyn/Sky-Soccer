using UnityEngine;

#if UNITY_ANDROID
public class BluetoothMultiplayerAndroidDiscoveryDemoGui : MonoBehaviour {
    private bool _initResult;
    private static string _log;

    public static void Log(string text) {
        _log += " " + text + "\r\n";
        Debug.Log(text);
    }

    void Awake() {
        Log("This demo shows some available methods and how to discover nearby Bluetooth devices.\r\n");
        // Setting the UUID. Must be unique for every application
        _initResult = BluetoothMultiplayerAndroid.Init("8ce255c0-200a-11e0-ac64-0800200c9a66");
        // Enabling verbose logging. See logcat!
        BluetoothMultiplayerAndroid.SetVerboseLog(true);

        // Registering the event delegates
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnabledEvent += onBluetoothAdapterEnabled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnableFailedEvent += onBluetoothAdapterEnableFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterDisabledEvent += onBluetoothAdapterDisabled;
        BluetoothMultiplayerAndroidManager.onBluetoothDiscoveryStartedEvent += onBluetoothDiscoveryStarted;
        BluetoothMultiplayerAndroidManager.onBluetoothDiscoveryFinishedEvent += onBluetoothDiscoveryFinished;
        BluetoothMultiplayerAndroidManager.onBluetoothDiscoveryDeviceFoundEvent += onBluetoothDiscoveryDeviceFound;
    }

    // Don't forget to unregister the event delegates!
    void OnDestroy() {
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnabledEvent -= onBluetoothAdapterEnabled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnableFailedEvent -= onBluetoothAdapterEnableFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterDisabledEvent -= onBluetoothAdapterDisabled;
        BluetoothMultiplayerAndroidManager.onBluetoothDiscoveryStartedEvent -= onBluetoothDiscoveryStarted;
        BluetoothMultiplayerAndroidManager.onBluetoothDiscoveryFinishedEvent -= onBluetoothDiscoveryFinished;
        BluetoothMultiplayerAndroidManager.onBluetoothDiscoveryDeviceFoundEvent -= onBluetoothDiscoveryDeviceFound;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Gracefully closing all Bluetooth connectivity and loading the menu
            try {
                BluetoothMultiplayerAndroid.StopDiscovery();
                BluetoothMultiplayerAndroid.Disconnect();
            } catch {
                //
            }
            Application.LoadLevel("BluetoothMultiplayerAndroidDemoMenu");
        }
    }

    private Vector2 _logScrollPosition;
	void OnGUI() {
        // If initialization was successfull, showing the buttons
        if (_initResult) {
            // simple text log view
            GUILayout.Space(190f);
            _logScrollPosition = GUILayout.BeginScrollView(_logScrollPosition, GUILayout.MaxHeight(Screen.height - 190f), GUILayout.MinWidth(Screen.width - 10f), GUILayout.ExpandHeight(false));
            GUILayout.Label(_log, GUILayout.ExpandHeight (true));
            GUILayout.EndScrollView();

            // Generic GUI for calling the methods
            if (GUI.Button(new Rect(10, 10, 140, 50), "Request enable\nBluetooth")) {
                BluetoothMultiplayerAndroid.RequestBluetoothEnable();
            }

            if (GUI.Button(new Rect(160, 10, 140, 50), "Disable Bluetooth")) {
                BluetoothMultiplayerAndroid.BluetoothDisable();
            }

            if (GUI.Button(new Rect(310, 10, 150, 50), "Request discoverability")) {
                BluetoothMultiplayerAndroid.RequestDiscoverable(120);
            }

            if (GUI.Button(new Rect(10, 70, 140, 50), "Start discovery")) {
                BluetoothMultiplayerAndroid.StartDiscovery();
            }

            if (GUI.Button(new Rect(160, 70, 140, 50), "Stop discovery")) {
                BluetoothMultiplayerAndroid.StopDiscovery();
            }

            if (GUI.Button(new Rect(310, 70, 150, 50), "Get current\ndevice")) {
                Log("Current device:");
                BluetoothDevice device = BluetoothMultiplayerAndroid.GetCurrentDevice();
                if (device != null) { // result can be null on error or if Bluetooth is not available
                    Log("Device: " + device.name + " [" + device.address + "]");
                } else {
                    Log("Error while retrieving current device");
                }
            }

            // Just get the device lists and prints them
            if (GUI.Button(new Rect(10, 130, 140, 50), "Show bonded\ndevice list")) {
                Log("Listing known bonded (paired) devices");
                BluetoothDevice[] list = BluetoothMultiplayerAndroid.GetBondedDevices();

                if (list != null) { // result can be null on error or if Bluetooth is not available
                    foreach (BluetoothDevice device in list) {
                        Log("Device: " + device.name + " [" + device.address + "]");
                    }
                } else {
                    Log("Error while retrieving GetBondedDevices()");
                }
            }

            if (GUI.Button(new Rect(160, 130, 140, 50), "Show new discovered\ndevice list")) {
                Log("Listing devices discovered during last discovery session...");
                BluetoothDevice[] list = BluetoothMultiplayerAndroid.GetNewDiscoveredDevices();

                if (list != null) { // result can be null on error or if Bluetooth is not available
                    foreach (BluetoothDevice device in list) {
                        Log("Device: " + device.name + " [" + device.address + "]");
                    }
                } else {
                    Log("Error while retrieving GetNewDiscoveredDevices()");
                }
            }

            if (GUI.Button(new Rect(310, 130, 150, 50), "Show full\ndevice list")) {
                Log("Listing all known or discovered devices...");
                BluetoothDevice[] list = BluetoothMultiplayerAndroid.GetDiscoveredDevices();

                if (list != null) { // result can be null on error or if Bluetooth is not available
                    foreach (BluetoothDevice device in list) {
                        Log("Device: " + device.name + " [" + device.address + "]");
                    }
                } else {
                    Log("Error while retrieving GetDiscoveredDevices()");
                }
            }
        // Showing message if initialization failed for some reason
        } else {
            GUI.Label(new Rect(10, 10, Screen.width, 50), "Bluetooth not available. Are you running this on Bluetooth-capable Android device and AndroidManifest.xml is set up correctly?");
        }

        if (GUI.Button(new Rect(Screen.width - 100f - 15f, Screen.height - 40f - 15f, 100f, 40f), "Back")) {
            try {
                BluetoothMultiplayerAndroid.StopDiscovery();
                BluetoothMultiplayerAndroid.Disconnect();
            } catch {
                //
            }
            Application.LoadLevel("BluetoothMultiplayerAndroidDemoMenu");
        }
            
	}

    private void onBluetoothDiscoveryDeviceFound(BluetoothDevice device) {
        // For demo purposes, display the device name and address when a device was found
        Log("onBluetoothDiscoveryDeviceFound(): " + device.name + " [" + device.address + "]");
    }

    private void onBluetoothAdapterDisabled() {
        Log("onBluetoothAdapterDisabled()");
    }

    private void onBluetoothAdapterEnableFailed() {
        Log("onBluetoothAdapterEnableFailed()");
    }

    private void onBluetoothAdapterEnabled() {
        Log("onBluetoothAdapterEnabled()");
    }

    private void onBluetoothDiscoveryFinished() {
        Log("onBluetoothDiscoveryFinished()");
    }

    private void onBluetoothDiscoveryStarted() {
        Log("onBluetoothDiscoveryStarted()");
    }

}
#endif