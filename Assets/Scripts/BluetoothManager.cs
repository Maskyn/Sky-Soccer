using UnityEngine;
using System;

#if UNITY_ANDROID
public class BluetoothManager
{
		public const string LocalIp = "127.0.0.1"; // An IP for Network.Connect(), must always be 127.0.0.1
		public const int Port = 28000; // Local server IP. Must be the same for client and server
	
		public bool _initResult;
		public BluetoothMultiplayerMode _desiredMode = BluetoothMultiplayerMode.None;

		// Instance
		static BluetoothManager sInstance = null;
	
		private BluetoothManager ()
		{
				Setup ();
		}
	
		public static BluetoothManager Instance {
				get {
						if (sInstance == null)
								sInstance = new BluetoothManager ();
						return sInstance;
				}
				set {
						sInstance = value;
				}
		}

		public void Setup ()
		{
				// Setting the UUID. Must be unique for every application
				_initResult = BluetoothMultiplayerAndroid.Init ("8ce245c0-200a-11e2-ac64-0870200c9a66");
				// Enabling verbose logging. See log cat!
				BluetoothMultiplayerAndroid.SetVerboseLog (true);

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

				UniversalOnline.Instance.Progress = 10;
		}
	
		// Don't forget to unregister the event delegates!
		public void Destroy ()
		{
		
		
				_desiredMode = BluetoothMultiplayerMode.None;

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

				// Gracefully closing all Bluetooth connectivity and loading the menu
				try {
						BluetoothMultiplayerAndroid.StopDiscovery ();
						BluetoothMultiplayerAndroid.Disconnect ();
				} catch {
						//
				}
		}

		public void Create ()
		{
				if (_initResult) {
						// If there is no current Bluetooth connectivity
						if (BluetoothMultiplayerAndroid.CurrentMode () == BluetoothMultiplayerMode.None) {
								// If Bluetooth is enabled, then we can do something right on
								if (BluetoothMultiplayerAndroid.IsBluetoothEnabled ()) {
										Network.Disconnect (); // Just to be sure
										BluetoothMultiplayerAndroid.InitializeServer (BluetoothManager.Port);
										BluetoothManager.Instance._desiredMode = BluetoothMultiplayerMode.Server;
								}
				// Otherwise we have to enable Bluetooth first and wait for callback
				else {
										BluetoothManager.Instance._desiredMode = BluetoothMultiplayerMode.Server;
										BluetoothMultiplayerAndroid.RequestDiscoverable (60);
								}
				
						}
						UniversalOnline.Instance.Progress = 10;
						UniversalOnline.Instance.State = UniversalOnline.States.BTCreate;
				} else {
						UniversalOnline.Instance.Progress = 10;
						UniversalOnline.Instance.State = UniversalOnline.States.FailedConnect;
				}
		} 

		public void Join ()
		{
				// If initialization was successfull, showing the buttons
				if (_initResult) {
						// If there is no current Bluetooth connectivity
						if (BluetoothMultiplayerAndroid.CurrentMode () == BluetoothMultiplayerMode.None) {
								// If Bluetooth is enabled, then we can do something right on
								if (BluetoothMultiplayerAndroid.IsBluetoothEnabled ()) {
										Network.Disconnect (); // Just to be sure
										BluetoothMultiplayerAndroid.ShowDeviceList (); // Open device picker dialog
										BluetoothManager.Instance._desiredMode = BluetoothMultiplayerMode.Client;
								}
				// Otherwise we have to enable Bluetooth first and wait for callback
				else {
										BluetoothManager.Instance._desiredMode = BluetoothMultiplayerMode.Client;
										BluetoothMultiplayerAndroid.RequestBluetoothEnable ();
								}
						}
						UniversalOnline.Instance.Progress = 10;
						UniversalOnline.Instance.State = UniversalOnline.States.BTJoin;
				} else {
						UniversalOnline.Instance.Progress = 10;
						UniversalOnline.Instance.State = UniversalOnline.States.FailedConnect;
				}
		}
	
		void OnPlayerDisconnected (NetworkPlayer player)
		{
				UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
				Network.RemoveRPCs (player);
				Network.DestroyPlayerObjects (player);

		}
	
		void OnFailedToConnect (NetworkConnectionError error)
		{
				UniversalOnline.Instance.State = UniversalOnline.States.FailedConnect;
		
				BluetoothMultiplayerAndroid.Disconnect ();
		}  
	
		void OnDisconnectedFromServer ()
		{
				UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
		
				// Stopping all Bluetooth connectivity on Unity networking disconnect event
				BluetoothMultiplayerAndroid.Disconnect ();
		}
	
		void OnConnectedToServer ()
		{
				// Instantiating a simple test actor
				//Application.LoadLevel(Constants.LEVEL_GAMEPLAY);

				UniversalOnline.Instance.State = UniversalOnline.States.ConnectedToServer;

				UniversalOnline.Instance.Progress = 50;
		}
	
		void OnServerInitialized ()
		{
				UniversalOnline.Instance.State = UniversalOnline.States.ServerReady;

				UniversalOnline.Instance.Progress = 30;
				// Instantiating a simple test actor
				//if (Network.isServer) {
				//	Network.Instantiate(ActorPrefab, Vector3.zero, Quaternion.identity, 0); 
				//}
				//Application.LoadLevel(Constants.LEVEL_GAMEPLAY);
		} 
	
		void onBluetoothListeningStarted ()
		{
		
				// Starting Unity networking server if Bluetooth listening started successfully
				Network.InitializeServer (2, Port, false);

				UniversalOnline.Instance.Progress = 10;

				UniversalOnline.Instance.State = UniversalOnline.States.ListeningStarted;
		}
	
		private void onBluetoothListeningCanceled ()
		{
		
				// For demo simplicity, stop server if listening was canceled
				BluetoothMultiplayerAndroid.Disconnect ();

				UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
		}
	
		private void onBluetoothDevicePicked (BluetoothDevice device)
		{
				// Trying to connect to a device user had picked
				BluetoothMultiplayerAndroid.Connect (device.address, Port);

				UniversalOnline.Instance.Progress = 40;

				UniversalOnline.Instance.State = UniversalOnline.States.DevicePicked;
		}
	
		private void onBluetoothClientDisconnected (BluetoothDevice device)
		{
				UniversalOnline.Instance.State = UniversalOnline.States.Aborted;

				Application.LoadLevel (Constants.LEVEL_MENU);
		}
	
		private void onBluetoothClientConnected (BluetoothDevice device)
		{
				UniversalOnline.Instance.Progress = 90;
				UniversalOnline.Instance.State = UniversalOnline.States.ReadyToPlay;
		}
	
		private void onBluetoothDisconnectedFromServer (BluetoothDevice device)
		{
				// Stopping Unity networking on Bluetooth failure
				Network.Disconnect ();

				UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
				
		}
	
		private void onBluetoothConnectToServerFailed (BluetoothDevice device)
		{
				UniversalOnline.Instance.State = UniversalOnline.States.FailedConnect;
		}
	
		private void onBluetoothConnectedToServer (BluetoothDevice device)
		{
				// Trying to negotiate a Unity networking connection, 
				// when Bluetooth client connected successfully
				Network.Connect (LocalIp, Port);

				UniversalOnline.Instance.State = UniversalOnline.States.ReadyToPlay;
		}
	
		private void onBluetoothAdapterDisabled ()
		{
				UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
		}
	
		private void onBluetoothAdapterEnableFailed ()
		{
				UniversalOnline.Instance.State = UniversalOnline.States.FailedConnect;
		}
	
		private void onBluetoothAdapterEnabled ()
		{

				// Resuming desired action after enabling the adapter
				switch (_desiredMode) {
				case BluetoothMultiplayerMode.Server:
						Network.Disconnect ();
						BluetoothMultiplayerAndroid.InitializeServer (Port);
						break;
				case BluetoothMultiplayerMode.Client:
						Network.Disconnect ();
						BluetoothMultiplayerAndroid.ShowDeviceList ();
						break;
				}
		}
}
#endif