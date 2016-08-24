using System;
using UnityEngine;

#if UNITY_ANDROID
public class BluetoothMultiplayerAndroidManager : MonoBehaviour {
    private static BluetoothMultiplayerAndroidManager _instance = null;
    public static BluetoothMultiplayerAndroidManager Instance {
        get {
            UpdateInstance();
            return _instance;
        }
    }

    static void UpdateInstance() {
        if (_instance == null) {
            // Trying to find an existing instance in the scene
            _instance = (BluetoothMultiplayerAndroidManager) FindObjectOfType(typeof(BluetoothMultiplayerAndroidManager));

            // Creating a new instance in case there are no instances present in the scene
            if (_instance == null) {
                GameObject go = new GameObject(typeof(BluetoothMultiplayerAndroidManager).ToString());
                _instance = go.AddComponent<BluetoothMultiplayerAndroidManager>();
            }
        }
    }

    static BluetoothMultiplayerAndroidManager() {
        try {
            UpdateInstance();
        } catch (Exception) {
            // Happens when this static constructor is called from a GameObject being created.
            // Just ignoring this, as this is intended
        }
    }

    void Awake() {
        // Kill other instances
        if (FindObjectsOfType(typeof (BluetoothMultiplayerAndroidManager)).Length > 1) {
            DestroyImmediate(gameObject);
            Debug.LogError("Multiple BluetoothMultiplayerAndroidManager instance found, destroying");
            return;
        }

        _instance = this;
        // Set the GameObject name to the class name for UnitySendMessage
        gameObject.name = GetType().ToString();
        // We want this object to persist across scenes
        DontDestroyOnLoad(this);
    }

    #region Events
        // Fired when server is started and waiting for incoming connections
        public static event Action                  onBluetoothListeningStartedEvent;
        // Fired when listening for incoming connections 
        // was stopped by BluetoothMultiplayerAndroid.StopListening()
        public static event Action                  onBluetoothListeningCanceledEvent;
        // Fired when Bluetooth was enabled
        public static event Action                  onBluetoothAdapterEnabledEvent;
        // Fired when request to enabled Bluetooth failed for some reason
        // (user did not authorized to enable Bluetooth or an error occured)
        public static event Action                  onBluetoothAdapterEnableFailedEvent;
        // Fired when Bluetooth was disabled
        public static event Action                  onBluetoothAdapterDisabledEvent;
        // Fired when Bluetooth client successfully connected to the Bluetooth server
        // Provides BluetoothDevice of server device
        public static event Action<BluetoothDevice> onBluetoothConnectedToServerEvent;
        // Fired when Bluetooth client failed to connect to the Bluetooth server
        // Provides BluetoothDevice of server device
        public static event Action<BluetoothDevice> onBluetoothConnectToServerFailedEvent;
        // Fired when Bluetooth client disconnected from the Bluetooth server
        // Provides BluetoothDevice of server device which was disconnected from
        public static event Action<BluetoothDevice> onBluetoothDisconnectedFromServerEvent;
        // Fired on Bluetooth server when an incoming Bluetooth client connection was accepted
        // Provides BluetoothDevice of connected client device
        public static event Action<BluetoothDevice> onBluetoothClientConnectedEvent;
        // Fired on Bluetooth server when an Bluetooth client had disconnected
        // Provides BluetoothDevice of disconnected client device
        public static event Action<BluetoothDevice> onBluetoothClientDisconnectedEvent;
        // Fired when user selects a device in the device picker dialog
        // Provides BluetoothDevice of picked device
        public static event Action<BluetoothDevice> onBluetoothDevicePickedEvent;
        // Fired when Bluetooth discovery is actually started
        public static event Action onBluetoothDiscoveryStartedEvent;
        // Fired when Bluetooth discovery is finished
        public static event Action onBluetoothDiscoveryFinishedEvent;
        // Fired when a new device was found during Bluetooth discovery procedure
        // Provides BluetoothDevice of found device
        public static event Action<BluetoothDevice> onBluetoothDiscoveryDeviceFoundEvent;
    #endregion

    // Fired when server is started and waiting for incoming connections
    private void onBluetoothListeningStarted(string empty) {
        Action handler = onBluetoothListeningStartedEvent;
        if (handler != null) handler();
    }

    // Fired when listening for incoming connections 
    // was stopped by BluetoothMultiplayerAndroid.StopListening()
    private void onBluetoothListeningCanceled(string empty) {
        Action handler = onBluetoothListeningCanceledEvent;
        if (handler != null) handler();
    }

    // Fired when Bluetooth was enabled
    private void onBluetoothAdapterEnabled(string empty) {
        Action handler = onBluetoothAdapterEnabledEvent;
        if (handler != null) handler();
    }

    // Fired when request to enabled Bluetooth failed for some reason
    // (user did not authorized to enable Bluetooth or an error occured)
    private void onBluetoothAdapterEnableFailed(string empty) {
        Action handler = onBluetoothAdapterEnableFailedEvent;
        if (handler != null) handler();
    }

    // Fired when Bluetooth was disabled
    private void onBluetoothAdapterDisabled(string empty) {
        Action handler = onBluetoothAdapterDisabledEvent;
        if (handler != null) handler();
    }

    // Fired when Bluetooth client successfully connected to the Bluetooth server
    // Provides BluetoothDevice of server device
    private void onBluetoothConnectedToServer(string deviceInfoString) {
        Action<BluetoothDevice> handler = onBluetoothConnectedToServerEvent;
        if (handler != null) handler(new BluetoothDevice(deviceInfoString));
    }

    // Fired when Bluetooth client failed to connect to the Bluetooth server
    // Provides BluetoothDevice of server device
    private void onBluetoothConnectToServerFailed(string deviceInfoString) {
        Action<BluetoothDevice> handler = onBluetoothConnectToServerFailedEvent;
        if (handler != null) handler(new BluetoothDevice(deviceInfoString));
    }

    // Fired when Bluetooth client disconnected from the Bluetooth server
    // Provides BluetoothDevice of server device disconnected from
    private void onBluetoothDisconnectedFromServer(string deviceInfoString) {
        Action<BluetoothDevice> handler = onBluetoothDisconnectedFromServerEvent;
        if (handler != null) handler(new BluetoothDevice(deviceInfoString));
    }

    // Fired on Bluetooth server when an incoming Bluetooth client connection
    // was accepted
    // Provides BluetoothDevice of connected client device
    private void onBluetoothClientConnected(string deviceInfoString) {
        Action<BluetoothDevice> handler = onBluetoothClientConnectedEvent;
        if (handler != null) handler(new BluetoothDevice(deviceInfoString));
    }

    // Fired on Bluetooth server when a Bluetooth client had disconnected
    // Provides BluetoothDevice of disconnected client device
    private void onBluetoothClientDisconnected(string deviceInfoString) {
        Action<BluetoothDevice> handler = onBluetoothClientDisconnectedEvent;
        if (handler != null) handler(new BluetoothDevice(deviceInfoString));
    }

    // Fired when user selects a device in the device picker dialog. 
    // Provides BluetoothDevice of picked device
    private void onBluetoothDevicePicked(string deviceInfoString) {
        Action<BluetoothDevice> handler = onBluetoothDevicePickedEvent;
        if (handler != null) handler(new BluetoothDevice(deviceInfoString));
    }

    // Fired when Bluetooth discovery is actually started
    // after call to BluetoothMultiplayerAndroid.StartListening()
    private void onBluetoothDiscoveryStarted(string empty) {
        Action handler = onBluetoothDiscoveryStartedEvent;
        if (handler != null) handler();
    }

    // Fired when Bluetooth discovery is finished
    private void onBluetoothDiscoveryFinished(string empty) {
        Action handler = onBluetoothDiscoveryFinishedEvent;
        if (handler != null) handler();
    }

    // Fired when a new device was found during
    // Bluetooth discovery procedure
    private void onBluetoothDiscoveryDeviceFound(string deviceInfoString) {
        Action<BluetoothDevice> handler = onBluetoothDiscoveryDeviceFoundEvent;
        if (handler != null) handler(new BluetoothDevice(deviceInfoString));
    }
}
#endif
