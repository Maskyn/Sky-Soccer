using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class MainMenuSetup : MonoBehaviour
{
	#region VARIABLES
		public GameObject MainMenu, ConfigureMatchMenu, ConfigureOnlineMenu;
		private System.Action<bool> mAuthCallback;
	#endregion
	
	#region MONO BEHAVIOUR METHODS
		void Start ()
		{
				Game.Instance.SetupFromZero ();
		
				Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
				Localization.language = SavedVariables.Language;
				#if UNITY_ANDROID
				InizializePlayGames ();
				#endif
		
				MainMenu.GetComponent<OneMenuPerScreenBehaviour> ().ActivateThisMenu ();
		}
	
		void Update ()
		{
				if (Input.GetKeyUp (KeyCode.Escape)) {
						Application.Quit ();
				}

				if (UniversalOnline.Instance.State == UniversalOnline.States.ReadyToPlay) {
						UniversalOnline.Instance.Progress = 0;
						MainMenu.SetActive (false);
						ConfigureOnlineMenu.SetActive (true);
				}
		
		}
	#endregion
	
	#region PRIVATE METHODS
		
	
		private void StartOnlineMatch ()
		{
				byte[] dataSmall = new byte[20];
				dataSmall [0] = Constants.CODE_ALL_READY;
				if (Game.Instance.OnlinePlay) {
						PlayGamesPlatform.Instance.RealTime.SendMessageToAll (true, dataSmall);
				} else if (Game.Instance.BluetoothPlay) {
						#if UNITY_ANDROID
						SendInfoToServerMainMenu (dataSmall);
						#endif
				}
				UniversalOnline.Instance.OnMessageReceived (dataSmall);
		}
	#endregion
	
	#region GOOGLE PLAY SERVICES
		public void ShowAchievements ()
		{
				#if UNITY_ANDROID
				if (!PlayGamesPlatform.Instance.IsAuthenticated ()) {
			
						mAuthCallback = (bool success) => {
								if (success) {
										PlayGamesPlatform.Instance.ShowAchievementsUI ();
										//Social.ShowAchievementsUI ();
								}
						};
						PlayGamesPlatform.Instance.Authenticate (mAuthCallback);
				} else {
						PlayGamesPlatform.Instance.ShowAchievementsUI ();
						//Social.ShowAchievementsUI ();
				}
				#elif UNITY_IPHONE
				#endif
		}
	
		public void ShowLeaderboards ()
		{
				#if UNITY_ANDROID
				if (!PlayGamesPlatform.Instance.IsAuthenticated ()) {
			
						mAuthCallback = (bool success) => {
								if (success) {
										PlayGamesPlatform.Instance.ShowLeaderboardUI ();
										//Social.ShowLeaderboardUI ();
								}
						};
						PlayGamesPlatform.Instance.Authenticate (mAuthCallback);
				} else {
						PlayGamesPlatform.Instance.ShowLeaderboardUI ();
						//Social.ShowLeaderboardUI ();
				}
				#elif UNITY_IPHONE
				#endif
		}

		public void InviteClick ()
		{
				#if UNITY_ANDROID
				OnlineManager.CreateWithInvitationScreen ();
				#elif UNITY_IPHONE
				#endif
		}
	
		private void InizializePlayGames ()
		{
				mAuthCallback = (bool success) => {
						if (success) {
						}
				};
		
				// make Play Games the default social implementation
				PlayGamesPlatform.Activate ();
		
				// enable debug logs (note: we do this because this is a sample; on your production
				// app, you probably don't want this turned on by default, as it will fill the user's
				// logs with debug info).
				PlayGamesPlatform.DebugLogEnabled = false;

				//SetStandBy("Please wait...");
				bool silent;
				if (SavedVariables.FirstOpening) {
						SavedVariables.FirstOpening = false;
						silent = false;
				} else {
						silent = true;
				}
				PlayGamesPlatform.Instance.Authenticate (mAuthCallback, silent);

		
		}
	
		public void SignInOut ()
		{
				#if UNITY_ANDROID
				if (!PlayGamesPlatform.Instance.IsAuthenticated ()) {
						mAuthCallback = (bool success) => {
								if (success) {
								}
						};
						PlayGamesPlatform.Instance.Authenticate (mAuthCallback, false);
				} else {
						PlayGamesPlatform.Instance.SignOut ();
				}
		
				#elif UNITY_IPHONE
				#endif
		}
	
		public void AbortConnections ()
		{
				UniversalOnline.Instance.CleanUp ();
		}
	#endregion
	
	#region GENERAL CLICKS
	
		public void ArcadeGame ()
		{
				Game.Instance.OnlinePlay = false;
				Game.Instance.BluetoothPlay = false;
				Game.Instance.LocalMultiPlayer = false;
				Game.Instance.TournamentMode = false;
		}
	
		public void TournamentGame ()
		{
				Game.Instance.OnlinePlay = false;
				Game.Instance.BluetoothPlay = false;
				Game.Instance.LocalMultiPlayer = false;
				Game.Instance.TournamentMode = true;
		}

		public void ChooseLanguage (string language)
		{
				SavedVariables.Language = language;
				Localization.language = language;
		}
	
		public void MultiPlayerOnline ()
		{
				Game.Instance.TournamentMode = false;
				Game.Instance.OnlinePlay = true;
				Game.Instance.BluetoothPlay = false;
				Game.Instance.LocalMultiPlayer = false;
				#if UNITY_ANDROID
				if (PlayGamesPlatform.Instance.IsAuthenticated ()) {
						InviteClick ();
				} else {
						mAuthCallback = (bool success) => {
								if (success) {
										InviteClick ();
								}
						};
						PlayGamesPlatform.Instance.Authenticate (mAuthCallback);
				}
				#elif UNITY_IPHONE
				#endif
		}
	
		public void MultiPlayerBluetooth ()
		{
				Game.Instance.TournamentMode = false;
				Game.Instance.OnlinePlay = false;
				Game.Instance.BluetoothPlay = true;
				Game.Instance.LocalMultiPlayer = false;
		}
	
		public void MultiPlayerLocal ()
		{
				Game.Instance.TournamentMode = false;
				Game.Instance.OnlinePlay = false;
				Game.Instance.BluetoothPlay = false;
				Game.Instance.LocalMultiPlayer = true;
		}
	
		public void StartGame ()
		{
				string level = Game.Instance.TournamentMode ? Constants.LEVEL_TOURNAMENT : Constants.LEVEL_GAMEPLAY;
				#if UNITY_PRO_LICENSE
				Application.LoadLevelAsync (level);
				#else
		Application.LoadLevel(level);
				#endif
		}
	
		public void ConfigureOnlineFinished ()
		{
				UniversalOnline.Instance.Progress = 90;
				byte[] dataSmall = new byte[20];
				PlayerEnum player = PlayerEnumUtils.GetCurrentPlayer ();
				dataSmall [0] = Constants.CODE_CONFIGURE_MATCH;
		
				if (player == PlayerEnum.RIGHT) {
						int nationId02 = NationalSuits.getNationId (Game.Instance.Nation02);
						Game.Instance.LandscapeId = (byte)Random.Range (0, (int)Constants.BACKGROUNDS);
						dataSmall [1] = Game.Instance.LandscapeId;
						dataSmall [2] = Constants.CODE_NOTHING;
						dataSmall [3] = (byte)nationId02;
						Game.Instance.Nation02 = NationalSuits.getNationById ((byte)nationId02);
				} else {
						int nationId01 = NationalSuits.getNationId (Game.Instance.Nation01);
						dataSmall [1] = Constants.CODE_NOTHING;
						dataSmall [2] = (byte)nationId01;
						dataSmall [3] = Constants.CODE_NOTHING;
			
						Game.Instance.Nation01 = NationalSuits.getNationById ((byte)nationId01);
				}
				if (Game.Instance.OnlinePlay) {
						PlayGamesPlatform.Instance.RealTime.SendMessageToAll (true, dataSmall);
				} else if (Game.Instance.BluetoothPlay) {
						#if UNITY_ANDROID
						SendInfoToServerMainMenu (dataSmall);
						#endif
				}
				UniversalOnline.Instance.State = UniversalOnline.States.Playing;
		
				if (UniversalOnline.Instance.TheOpponentIsReady) {
						Invoke ("StartOnlineMatch", 2.5f);
				}
		
		}
	#endregion
	
	#region CHOOSE_NATION_SCREEN
		public void EditNation01 ()
		{
				Game.Instance.EditingNation01 = true;
		}
	
		public void EditNation02 ()
		{
				Game.Instance.EditingNation02 = true;
		}

		public void EditSelfNation ()
		{
				if (PlayerEnumUtils.GetCurrentPlayer () == PlayerEnum.LEFT)
						Game.Instance.EditingNation01 = true;
				else
						Game.Instance.EditingNation02 = true;
		}
	
		public void NationSelected ()
		{
				if (Game.Instance.BluetoothPlay || Game.Instance.OnlinePlay)
						ConfigureOnlineMenu.SetActive (true);
				else
						ConfigureMatchMenu.SetActive (true);
		}
	#endregion
	
	#region BLUETOOTH_SCREEN
	#if UNITY_ANDROID
		public void BluetoothCreate ()
		{
				BluetoothManager.Instance.Create ();
		}
	
		public void BluetoothJoin ()
		{
				BluetoothManager.Instance.Join ();
		}
	#endif
           	#endregion
	
	#region RPC
	#if UNITY_ANDROID
		[RPC]
		void SendInfoToServerMainMenu (byte[] data)
		{
				networkView.RPC ("ReceiveInfoFromClientMainMenu", RPCMode.Others, data);
		}
		[RPC]
		void ReceiveInfoFromClientMainMenu (byte[] data)
		{
				UniversalOnline.Instance.OnMessageReceived (data);
		}
	#endif
           	#endregion
}
