using UnityEngine;

#if UNITY_ANDROID
public class BluetoothMultiplayerAndroidDemoGui : MonoBehaviour {
    public GameObject ActorPrefab; // Reference to out test actor

    private const string LocalIp = "127.0.0.1"; // An IP for Network.Connect(), must always be 127.0.0.1
    private const int Port = 28000; // Local server IP. Must be the same for client and server

    private bool _initResult;
    private BluetoothMultiplayerMode _desiredMode = BluetoothMultiplayerMode.None;

    void Awake() {
        // Setting the UUID. Must be unique for every application
        _initResult = BluetoothMultiplayerAndroid.Init("8ce255c0-200a-11e0-ac64-0800200c9a66");
        // Enabling verbose logging. See log cat!
        BluetoothMultiplayerAndroid.SetVerboseLog(true);

        // Registering the event delegates
        BluetoothMultiplayerAndroidManager.onBluetoothListeningStartedEvent += onBluetoothListeningStarted;
        BluetoothMultiplayerAndroidManager.onBluetoothListeningCanceledEvent += onBluetoothListeningCanceled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnabledEvent += onBluetoothAdapterEnabled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnableFailedEvent += onBluetoothAdapterEnableFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterDisabledEvent += onBluetoothAdapterDisabled;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectedToServerEvent += onBluetoothConnectedToServer;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectToServerFailedEvent += onBluetoothConnectToServerFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothDisconnectedFromServerEvent += onBluetoothDisconnectedFromServer;
        BluetoothMultiplayerAndroidManager.onBluetoothClientConnectedEvent += onBluetoothClientConnected;
        BluetoothMultiplayerAndroidManager.onBluetoothClientDisconnectedEvent += onBluetoothClientDisconnected;
        BluetoothMultiplayerAndroidManager.onBluetoothDevicePickedEvent += onBluetoothDevicePicked;
    }

    // Don't forget to unregister the event delegates!
    void OnDestroy() {
        BluetoothMultiplayerAndroidManager.onBluetoothListeningStartedEvent -= onBluetoothListeningStarted;
        BluetoothMultiplayerAndroidManager.onBluetoothListeningCanceledEvent -= onBluetoothListeningCanceled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnabledEvent -= onBluetoothAdapterEnabled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnableFailedEvent -= onBluetoothAdapterEnableFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterDisabledEvent -= onBluetoothAdapterDisabled;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectedToServerEvent -= onBluetoothConnectedToServer;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectToServerFailedEvent -= onBluetoothConnectToServerFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothDisconnectedFromServerEvent -= onBluetoothDisconnectedFromServer;
        BluetoothMultiplayerAndroidManager.onBluetoothClientConnectedEvent -= onBluetoothClientConnected;
        BluetoothMultiplayerAndroidManager.onBluetoothClientDisconnectedEvent -= onBluetoothClientDisconnected;
        BluetoothMultiplayerAndroidManager.onBluetoothDevicePickedEvent -= onBluetoothDevicePicked;
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

	void OnGUI() {
        // If initialization was successfull, showing the buttons
        if (_initResult) {
            // If there is no current Bluetooth connectivity
            if (BluetoothMultiplayerAndroid.CurrentMode() == BluetoothMultiplayerMode.None) {
                if (GUI.Button(new Rect(10, 10, 150, 50), "Create server")) {
                    // If Bluetooth is enabled, then we can do something right on
                    if (BluetoothMultiplayerAndroid.IsBluetoothEnabled()) {
                        Network.Disconnect(); // Just to be sure
                        BluetoothMultiplayerAndroid.InitializeServer(Port);
                    }
                    // Otherwise we have to enable Bluetooth first and wait for callback
                    else {
                        _desiredMode = BluetoothMultiplayerMode.Server;
                        BluetoothMultiplayerAndroid.RequestBluetoothEnable();
                    }
                }

                if (GUI.Button(new Rect(170, 10, 150, 50), "Connect to server")) {
                    // If Bluetooth is enabled, then we can do something right on
                    if (BluetoothMultiplayerAndroid.IsBluetoothEnabled()) {
                        Network.Disconnect(); // Just to be sure
                        BluetoothMultiplayerAndroid.ShowDeviceList(); // Open device picker dialog
                    }
                    // Otherwise we have to enable Bluetooth first and wait for callback
                    else {
                        _desiredMode = BluetoothMultiplayerMode.Client;
                        BluetoothMultiplayerAndroid.RequestBluetoothEnable();
                    }
                }
            } else {
                // Stop all networking
                if (GUI.Button(new Rect(10, 10, 150, 50), "Disconnect")) {
                    if (Network.peerType != NetworkPeerType.Disconnected)
                        Network.Disconnect();
                }
            }
        // Showing message if initialization failed for some reason
        } else {
            GUI.Label(new Rect(10, 10, Screen.width, 100), "Bluetooth not available. Are you running this on Bluetooth-capable Android device and AndroidManifest.xml is set up correctly?");
        }

        if (GUI.Button(new Rect(Screen.width - 100f - 15f, Screen.height - 40f - 15f, 100f, 40f), "Back")) {
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

    void OnPlayerDisconnected(NetworkPlayer player) {
        Debug.Log("Player disconnected: " + player.GetHashCode());
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log("Can't connect to the networking server");

        BluetoothMultiplayerAndroid.Disconnect();
    }  

    void OnDisconnectedFromServer() {
        Debug.Log("Disconnected from server");

        // Stopping all Bluetooth connectivity on Unity networking disconnect event
        BluetoothMultiplayerAndroid.Disconnect();

        TestActor[] objects = FindObjectsOfType(typeof(TestActor)) as TestActor[];

        if (objects != null)
            foreach (TestActor obj in objects) {
                Destroy(obj.gameObject);
            }
    }

    void OnConnectedToServer() {
        // Instantiating a simple test actor
        Network.Instantiate(ActorPrefab, Vector3.zero, Quaternion.identity, 0); 
    }

    void OnServerInitialized() {
        Debug.Log("OnServerInitialized");
        // Instantiating a simple test actor
        if (Network.isServer) {
            Network.Instantiate(ActorPrefab, Vector3.zero, Quaternion.identity, 0); 
        }
    } 

    void onBluetoothListeningStarted() {
        Debug.Log("onBluetoothListeningStarted()");

        // Starting Unity networking server if Bluetooth listening started successfully
        Network.InitializeServer(4, Port, false);
    }

    private void onBluetoothListeningCanceled() {
        Debug.Log("onBluetoothListeningCanceled()");

        // For demo simplicity, stop server if listening was canceled
        BluetoothMultiplayerAndroid.Disconnect();
    }

    private void onBluetoothDevicePicked(BluetoothDevice device) {
        Debug.Log("onBluetoothDevicePicked(): " + device.name + " [" + device.address + "]");

        // Trying to connect to a device user had picked
        BluetoothMultiplayerAndroid.Connect(device.address, Port);
    }

    private void onBluetoothClientDisconnected(BluetoothDevice device) {
        Debug.Log("onBluetoothClientDisconnected(): " + device.name + " [" + device.address + "]");
    }

    private void onBluetoothClientConnected(BluetoothDevice device) {
        Debug.Log("onBluetoothClientConnected(): " + device.name + " [" + device.address + "]");
    }

    private void onBluetoothDisconnectedFromServer(BluetoothDevice device) {
        Debug.Log("onBluetoothDisconnectedFromServer(): " + device.name + " [" + device.address + "]");

        // Stopping Unity networking on Bluetooth failure
        Network.Disconnect();
    }

    private void onBluetoothConnectToServerFailed(BluetoothDevice device) {
        Debug.Log("onBluetoothConnectToServerFailed(): " + device.name + " [" + device.address + "]");
    }

    private void onBluetoothConnectedToServer(BluetoothDevice device) {
        Debug.Log("onBluetoothConnectedToServer(): " + device.name + " [" + device.address + "]");

        // Trying to negotiate a Unity networking connection, 
        // when Bluetooth client connected successfully
        Network.Connect(LocalIp, Port);
    }

    private void onBluetoothAdapterDisabled() {
        Debug.Log("onBluetoothAdapterDisabled()");
    }

    private void onBluetoothAdapterEnableFailed() {
        Debug.Log("onBluetoothAdapterEnableFailed()");
    }

    private void onBluetoothAdapterEnabled() {
        Debug.Log("onBluetoothAdapterEnabled()");

        // Resuming desired action after enabling the adapter
        switch (_desiredMode) {
            case BluetoothMultiplayerMode.Server:
                Network.Disconnect();
                BluetoothMultiplayerAndroid.InitializeServer(Port);
                break;
            case BluetoothMultiplayerMode.Client:
                Network.Disconnect();
                Debug.Log("BluetoothMultiplayerAndroid.ShowDeviceList()");
                BluetoothMultiplayerAndroid.ShowDeviceList();
                break;
        }

        _desiredMode = BluetoothMultiplayerMode.None;
    }

}
#endif