using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public sealed class Game
{

		// number of players in a team
		public const int TEAM_PLAYERS = 11;

		// Instance
		static Game instance;

		private byte landScapeId;

		private Game ()
		{
				SetupFromZero ();
		}
	
		public static Game Instance {
				get {
						if (instance == null)
								instance = new Game ();
						return instance; 
				}
		}

		// setup the variables
		public void SetupFromZero ()
		{
				Nation01 = Nationals.NONE;
				Nation02 = Nationals.NONE;
				OnlinePlay = false;
				BluetoothPlay = false;
				LocalMultiPlayer = false;
				LandscapeId = 255;
				PlayerScore01 = 0;
				PlayerScore02 = 0;
				RemainingPlayers01 = TEAM_PLAYERS;
				RemainingPlayers02 = TEAM_PLAYERS;
				TimerActive = true;
				TimeRemained = 120.2f;
				DifficultyManager.Instance.Reset ();
		}

		public void ResetGameVariables ()
		{
				PlayerScore01 = 0;
				PlayerScore02 = 0;
				LandscapeId = 255;
				RemainingPlayers01 = TEAM_PLAYERS;
				RemainingPlayers02 = TEAM_PLAYERS;
				TimerActive = true;
				TimeRemained = 120.2f;
		}

		public bool OnlinePlay {
				get;
				set;
		}

		public bool BluetoothPlay {
				get;
				set;
		}

		public bool LocalMultiPlayer {
				get;
				set;
		}

		public bool LocalPlay {
				get {
						return OnlinePlay == false && BluetoothPlay == false && LocalMultiPlayer == false;
				}
		}

		public bool TournamentMode {
				get;
				set;
		}

		public Nationals Nation01 {
				get;
				set;
		}

		public Nationals Nation02 {
				get;
				set;
		}

		public byte LandscapeId {
				get {
						return landScapeId;
				}
				set {
						landScapeId = value;
				}
		}

		public byte PlayerScore01 {
				get;
				set;
		}

		public byte PlayerScore02 {
				get;
				set;
		}

		public byte RemainingPlayers01 {
				get;
				set;
		}
	
		public byte RemainingPlayers02 {
				get;
				set;
		}

		public bool TimerActive {
				get;
				set;
		}

		public void SetGameActive (bool active)
		{
				if (active) {
						Time.timeScale = 1;
				} else {
						Time.timeScale = 0;
				}
		}

		public bool IsGameActive ()
		{
				return Time.timeScale == 1;
		}
	
		public bool GameEnded {
				get {
						return TimeRemained < Mathf.Epsilon;
				}
		}
	
		public float TimeRemained {
				get;
				set;
		}
	
		public bool EditingNation01 {
				get;
				set;
		}
	
		public bool EditingNation02 {
				get;
				set;
		}

		public void ResetRemainingPlayers ()
		{
				RemainingPlayers01 = TEAM_PLAYERS;
				RemainingPlayers02 = TEAM_PLAYERS;
		}
}
