using System;
using UnityEngine;

#if UNITY_ANDROID
public class BluetoothMultiplayerAndroid {
    internal const string Delimiter = "~~~#$%~~~";
    internal const string ZeroBluetoothAddress = "00:00:00:00:00:00";
    private const string PluginClassName = "biz.zimm.unity.bluetoothmediator.BluetoothMediator";

    private static readonly AndroidJavaObject Plugin;
    private static readonly bool PluginAvailable;
    
    private static void CheckIsBluetoothEnabled() {
        if (!IsBluetoothEnabled())
            throw new BluetoothNotEnabledException("Bluetooth not enabled while calling Bluetooth-requiring method");
    }

    static BluetoothMultiplayerAndroid() {
        Plugin = null; 
        PluginAvailable = false;

        #if !UNITY_ANDROID
            Debug.LogError("You are trying to use BluetoothMultiplayerAndroid, but build platform is not set to Android. Please set build platform to Android first.");
            return;
        #else
            #if !UNITY_EDITOR
                if (Application.platform == RuntimePlatform.Android) {
                    try {
                        using (AndroidJavaClass mediatorClass = new AndroidJavaClass(PluginClassName)) {
                            if (!IsAndroidJavaClassNull(mediatorClass)) {
                                Plugin = mediatorClass.CallStatic<AndroidJavaObject>("getSingleton");
                                PluginAvailable = !IsAndroidJavaObjectNull(Plugin);
                            } 
                        }
                    } catch (Exception) {
                        Debug.LogError("BluetoothMultiplayerAndroid initialization failed. Probably .jar not present?");
                    }
                }
            #endif
        #endif
    }

    #region Methods
    // Initialize the plugin and set the Bluetooth service UUID
    public static bool Init(string uuid) {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("initUuid", uuid);
    }

    // Start server, listening for incoming Bluetooth connections
    public static bool InitializeServer(ushort port) {
        if (!PluginAvailable)
            return false;

        CheckIsBluetoothEnabled();
        return Plugin.Call<bool>("startServer", "127.0.0.1", (int) port);
    }

    // Connect to a Bluetooth device
    public static bool Connect(string hostDeviceAddress, ushort port) {
        if (!PluginAvailable)
            return false;
        
        CheckIsBluetoothEnabled();
        return Plugin.Call<bool>("startClient", "127.0.0.1", (int) port, hostDeviceAddress);
    }

    // Stop all Bluetooth connectivity
    public static bool Disconnect() {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("stop");
    }

    // Stop listening for new incoming connections
    public static bool StopListen() {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("stopListen");
    }

    // Return the current plugin mode
    public static BluetoothMultiplayerMode CurrentMode() {
        if (!PluginAvailable)
            return BluetoothMultiplayerMode.None;

       return (BluetoothMultiplayerMode) Plugin.Call<byte>("getCurrentMode");
    }

    // Open a dialog asking user to make device discoverable on Bluetooth
    // Will also request the user to turn on Bluetooth if it is not currently enabled
    public static bool RequestDiscoverable(int discoverableDuration) {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("requestDiscoverable", discoverableDuration);
    }

    // Open a dialog asking user to enable Bluetooth
    public static bool RequestBluetoothEnable() {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("requestBluetoothEnable");
    }

    // Enable the Bluetooth adapter, if possible
    public static bool BluetoothEnable() {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("bluetoothEnable");
    }

    // Disable the Bluetooth adapter, if possible
    public static bool BluetoothDisable() {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("bluetoothDisable");
    }

    // Return true of Bluetooth is available on the device
    public static bool IsBluetoothAvailable() {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("isBluetoothAvailable");
    }

    // Return true if Bluetooth is currently enabled 
    public static bool IsBluetoothEnabled() {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("isBluetoothEnabled");
    }

    // Show the Bluetooth device picker dialog
    public static bool ShowDeviceList() {
        if (!PluginAvailable)
            return false;

        CheckIsBluetoothEnabled();
        return Plugin.Call<bool>("showDeviceList");
    }

    // Return current Bluetooth device
    public static BluetoothDevice GetCurrentDevice() {
        if (!PluginAvailable)
            return null;

        BluetoothDevice currentDevice;
        try {
            currentDevice = new BluetoothDevice(Plugin.Call<String>("getCurrentDevice"));
        } catch (Exception) {
            return null;
        }

        return currentDevice;
    }

    // Start discovery of nearby discoverable Bluetooth devices
    public static bool StartDiscovery() {
        if (!PluginAvailable)
            return false;

        CheckIsBluetoothEnabled();
        return Plugin.Call<bool>("startDiscovery");
    }

    // Stop discovery of nearby discoverable Bluetooth devices
    public static bool StopDiscovery() {
        if (!PluginAvailable)
            return false;

        CheckIsBluetoothEnabled();
        return Plugin.Call<bool>("stopDiscovery");
    }

    // Return true if Bluetooth device discovery is going on
    public static bool IsDiscovering() {
        if (!PluginAvailable)
            return false;

        CheckIsBluetoothEnabled();
        return Plugin.Call<bool>("isDiscovering");
    }

    // Return array of bonded (paired) Bluetooth devices
    public static BluetoothDevice[] GetBondedDevices() {
        if (!PluginAvailable)
            return null;

        AndroidJavaObject deviceJavaSet = Plugin.Call<AndroidJavaObject>("getBondedDevices");
        BluetoothDevice[] deviceArray = ConvertJavaBluetoothDeviceSet(deviceJavaSet);

        return deviceArray;
    }

    // Return array of Bluetooth devices discovered during 
    // current discovery session
    public static BluetoothDevice[] GetNewDiscoveredDevices() {
        if (!PluginAvailable)
            return null;

        AndroidJavaObject deviceJavaSet = Plugin.Call<AndroidJavaObject>("getNewDiscoveredDevices");
        BluetoothDevice[] deviceArray = ConvertJavaBluetoothDeviceSet(deviceJavaSet);

        return deviceArray;
    }

    // Return array of bonded (paired) Bluetooth devices and
    // Bluetooth devices discovered during current discovery session
    public static BluetoothDevice[] GetDiscoveredDevices() {
        if (!PluginAvailable)
            return null;

        AndroidJavaObject deviceJavaSet = Plugin.Call<AndroidJavaObject>("getDiscoveredDevices");
        BluetoothDevice[] deviceArray = ConvertJavaBluetoothDeviceSet(deviceJavaSet);

        return deviceArray;
    }

    // Enable or disable raw packets. Use only if you know what you are doing
    public static bool SetRawPackets(bool doEnable) {
        if (!PluginAvailable)
            return false;

        return Plugin.Call<bool>("setRawPackets", doEnable);
    }

    // Enable or disable verbose logging. Useful for debugging
    public static void SetVerboseLog(bool doEnable) {
        if (!PluginAvailable)
            return;

        Plugin.CallStatic("setVerboseLog", doEnable);
    }
    #endregion

    #region Helper methods
    private static BluetoothDevice[] ConvertJavaBluetoothDeviceSet(AndroidJavaObject bluetoothDeviceJavaSet) {
        try {
            if (IsAndroidJavaObjectNull(bluetoothDeviceJavaSet))
                return null;

            AndroidJavaObject[] deviceJavaArray = bluetoothDeviceJavaSet.Call<AndroidJavaObject[]>("toArray");

            BluetoothDevice[] deviceArray = new BluetoothDevice[deviceJavaArray.Length];
            for (int i = 0; i < deviceJavaArray.Length; i++) {
                deviceArray[i] = new BluetoothDevice(deviceJavaArray[i]);
            }

            return deviceArray;
        } catch (Exception) {
            Debug.LogError("Error while converting BluetoothDevice Set");
            return null;
        }
    }

    private static bool IsAndroidJavaObjectNull(AndroidJavaObject androidJavaObject) {
        return androidJavaObject == null || androidJavaObject.GetRawClass().ToInt32() == 0;
    }
    private static bool IsAndroidJavaClassNull(AndroidJavaClass androidJavaClass) {
        return androidJavaClass == null || androidJavaClass.GetRawClass().ToInt32() == 0;
    }
    #endregion
}

public class BluetoothDevice {
    public enum BondState : byte  {
        None = 10,
        Bonding = 11,
        Bonded = 12
    }

    public readonly string name;
    public readonly string address;
    public readonly BondState bondState;

    internal BluetoothDevice(string deviceString) {
        try {
            string[] tokens = deviceString.Split(new[] {BluetoothMultiplayerAndroid.Delimiter},
                                                 StringSplitOptions.None);

            name = tokens[0].Trim();
            address = tokens[1];
            bondState = (BondState) UInt16.Parse(tokens[2]);

            if (name == "")
                name = address;
        } catch {
            name = "";
            address = BluetoothMultiplayerAndroid.ZeroBluetoothAddress;
            bondState = BondState.None;
        }
    }

    internal BluetoothDevice(AndroidJavaObject bluetoothDeviceJavaObject) {
        if (IsAndroidJavaObjectNull(bluetoothDeviceJavaObject)) {
            name = "";
            address = BluetoothMultiplayerAndroid.ZeroBluetoothAddress;
            bondState = BondState.None;
            return;
        }
        name = bluetoothDeviceJavaObject.Call<string>("getName").Trim();
        address = bluetoothDeviceJavaObject.Call<string>("getAddress");
        bondState = (BondState) bluetoothDeviceJavaObject.Call<int>("getBondState");

        if (name == "")
            name = address;
    }

    private static bool IsAndroidJavaObjectNull(AndroidJavaObject androidJavaObject) {
        return androidJavaObject == null || androidJavaObject.GetRawClass().ToInt32() == 0;
    }

    public override string ToString() {
        return address;
    }
}

public enum BluetoothMultiplayerMode : byte {
    None = 0, 
    Server = 1, 
    Client = 2
}

public class BluetoothNotEnabledException : Exception {
    public BluetoothNotEnabledException() : base() { }
    public BluetoothNotEnabledException(string message) : base(message) { }
}
#endif