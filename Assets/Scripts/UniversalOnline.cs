using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class UniversalOnline
{

		// room setup progress
		private double mRoomSetupProgress = 0.0;
		
		// speed of the "fake progress" (to keep the player happy)
		// during room setup
		const float FakeProgressSpeed = 1;
		const float MaxFakeProgress = 30;
		//float mRoomSetupStartTime = 0;

		bool theOpponentIsReady;

		public enum OnlineTypes
		{
				PlayGames,
				Bluetooth,
		}

		public enum States
		{
				SettingUp,
				ReadyToPlay,
				GameEnded,
				Aborted,
				FailedConnect,
				Playing,
				ServerReady,

				BTJoin,
				BTCreate,
				ConnectedToServer,
				ListeningStarted,
				DevicePicked
		}
		;


		public States State {
				get;
				set;
		}

		public double Progress {
				get {
						/*double fakeProgress = (Time.time - mRoomSetupStartTime) * FakeProgressSpeed;
						if (fakeProgress > MaxFakeProgress) {
								fakeProgress = MaxFakeProgress;
						}
						double progress = mRoomSetupProgress + fakeProgress;
						return progress < 99.0 ? progress : 99.0;*/
						//if (mRoomSetupProgress >= 99.0)
						//		State = States.ReadyToPlay;
						return mRoomSetupProgress < 99.0 ? mRoomSetupProgress : 99.0;
				}
				set {
						mRoomSetupProgress = value;
				}
		}
		

		static UniversalOnline instance = null;

		public static UniversalOnline Instance {
				get {
						if (instance == null)
								instance = new UniversalOnline ();
						return instance; 
				}
		}

		public bool TheOpponentIsReady {
				get {
						return theOpponentIsReady; 
				}
		}

		private UniversalOnline ()
		{
				State = States.SettingUp;
		}

		private GameObject ball;
		private float posX, posY, cos, sin;
		private float ballVelX, ballVelY, posBallX, posBallY;
		public void OnMessageReceived (byte[] data)
		{
				if (ball == null) {
						ball = GameObject.FindGameObjectWithTag (Constants.TAG_BALL);
				}
		
				switch (data [0]) { // what to update
				case Constants.CODE_BALL_AND_SHOOT:
			
						posX = BitConverter.ToSingle (data, 1 + 4 * 0);
						posY = BitConverter.ToSingle (data, 1 + 4 * 1);
						cos = BitConverter.ToSingle (data, 1 + 4 * 2);
						sin = BitConverter.ToSingle (data, 1 + 4 * 3);
			
						posBallX = BitConverter.ToSingle (data, 1 + 4 * 4);
						posBallY = BitConverter.ToSingle (data, 1 + 4 * 5);
						ballVelX = BitConverter.ToSingle (data, 1 + 4 * 6);
						ballVelY = BitConverter.ToSingle (data, 1 + 4 * 7);
						PlayerShooter.Instance.Shoot (posX, posY, cos, sin);
						ball.transform.GetComponent<Rigidbody2D>().velocity = new Vector2 (ballVelX, ballVelY);
						ball.transform.position = new Vector3 (posBallX, posBallY, 0);
						break;
				case Constants.CODE_GOAL:
						Camera.main.GetComponent<GamePlaySetup> ().FakeGoal ();
						Game.Instance.PlayerScore01 = data [1];
						Game.Instance.PlayerScore02 = data [2];
						Camera.main.GetComponent<GamePlaySetup> ().UpdateScore ();
						break;
				case Constants.CODE_BALL:
						posBallX = BitConverter.ToSingle (data, 1 + 4 * 0);
						posBallY = BitConverter.ToSingle (data, 1 + 4 * 1);
						ballVelX = BitConverter.ToSingle (data, 1 + 4 * 2);
						ballVelY = BitConverter.ToSingle (data, 1 + 4 * 3);
						ball.transform.GetComponent<Rigidbody2D>().velocity = new Vector2 (ballVelX, ballVelY);
						ball.transform.position = new Vector3 (posBallX, posBallY, 0);
						break;
				case Constants.CODE_TIME:
						Game.Instance.TimeRemained = data [1];
						Camera.main.GetComponent<GamePlaySetup> ().UpdateGuiTime ();
						break;
				case Constants.CODE_REMAINING_PLAYERS:
						Game.Instance.RemainingPlayers01 = data [1];
						Game.Instance.RemainingPlayers02 = data [2];
						Camera.main.GetComponent<GamePlaySetup> ().UpdateLifeBar ();
						break;
				case Constants.CODE_CONFIGURE_MATCH:
						if (data [1] != Constants.CODE_NOTHING)
								Game.Instance.LandscapeId = data [1];
						if (data [2] != Constants.CODE_NOTHING)
								Game.Instance.Nation01 = NationalSuits.getNationById (data [2]);
						if (data [3] != Constants.CODE_NOTHING)
								Game.Instance.Nation02 = NationalSuits.getNationById (data [3]);
						theOpponentIsReady = true;
						break;

				case Constants.CODE_ALL_READY:
						UniversalOnline.Instance.Progress = 0;
						theOpponentIsReady = false;
			//Application.LoadLevel (Constants.LEVEL_GAMEPLAY);
			#if UNITY_PRO_LICENSE
						Application.LoadLevelAsync (Constants.LEVEL_GAMEPLAY);
			#else
			Application.LoadLevel(Constants.LEVEL_GAMEPLAY);
			#endif
						break;
				}
		}

		public void CleanUp ()
		{
				if (Game.Instance.OnlinePlay && OnlineManager.Instance != null) {
						PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
						UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
						OnlineManager.Instance = null;
				}

				#if UNITY_ANDROID
		if (Game.Instance.BluetoothPlay && BluetoothManager.Instance != null) {
						BluetoothManager.Instance.Destroy ();
						UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
						BluetoothManager.Instance = null;
				}
#endif
				Progress = 0;

		}
}
