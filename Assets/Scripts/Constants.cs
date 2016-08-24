using UnityEngine;
using System.Collections;

public sealed class Constants
{

		// how many seconds in a minute
		public const byte SECOND_IN_MINUTE = 60;
		public const byte BACKGROUNDS = 4;
	
		public const byte CODE_BALL = 0;
		public const byte CODE_GOAL = 1;
		public const byte CODE_TIME = 2;
		public const byte CODE_REMAINING_PLAYERS = 3;
		public const byte CODE_SHOOTED_PLAYERS = 4;
		public const byte CODE_BALL_NO_POSITION = 5;
		public const byte CODE_BALL_AND_SHOOT = 6;
		public const byte CODE_CONFIGURE_MATCH = 7;
		public const byte CODE_ALL_READY = 8;
		public const byte CODE_NOTHING = 255;

		public const string LEVEL_MENU = "Main";
		public const string LEVEL_GAMEPLAY = "GamePlay";
		public const string LEVEL_LOADING_SCREEN = "LoadingScreen";
		public const string LEVEL_CONFIGURE_BLUETOOTH = "ConfigureBluetooth";
		public const string LEVEL_TOURNAMENT = "Tournament";

		public const string TAG_CAMERA = "MainCamera";
		public const string TAG_BALL = "Ball";
		public const string TAG_MENU = "Menu";
		public const string TAG_WALL = "Wall";
		public const string TAG_PLAYER = "Player";
		public const string TAG_WIND = "Wind";
		public const string TAG_DEATH_TRIGGER = "Death Trigger";
		public const string TAG_DOOR = "Door";
		public const string TAG_PARENT_PLAYERS = "Players Parent";
		public const string TAG_PARENT_WINDS = "Winds Parent";
		public const string TAG_PLAYER_REFEREE = "Player Referee";

		public const string LB_POINTS = "CgkIlLHvy7YHEAIQAQ";
		public const string LB_GOALS = "CgkIlLHvy7YHEAIQAg";
		public const string LB_PLAYED_MATCHES = "CgkIlLHvy7YHEAIQBQ";
		public const string LB_WON_MATCHES = "CgkIlLHvy7YHEAIQAw";

		public const string ACH_POINTS_100_000 = "CgkIlLHvy7YHEAIQFg";
		public const string ACH_POINTS_500_000 = "CgkIlLHvy7YHEAIQFw";
		public const string ACH_POINTS_1_000_000 = "CgkIlLHvy7YHEAIQGA";
		public const string ACH_BEST_DEFENSE_EVER = "CgkIlLHvy7YHEAIQFQ";
		public const string ACH_GOLDEN_GOAL = "CgkIlLHvy7YHEAIQFA";

		public const string ACH_TOURNAMENT_WON = "CgkIlLHvy7YHEAIQEw";
		public const string ACH_GOOD_TACTIC = "CgkIlLHvy7YHEAIQBg";
		public const string ACH_EXCELLENT_TACTIC = "CgkIlLHvy7YHEAIQBw";
		public const string ACH_SUPREME_TACTIC = "CgkIlLHvy7YHEAIQCA";
		public const string ACH_ONE_SHOOT = "CgkIlLHvy7YHEAIQCQ";
		public const string ACH_SUPREME_DEFENSE = "CgkIlLHvy7YHEAIQCg";
		public const string ACH_GOAL_WITH_THE_KEEPER = "CgkIlLHvy7YHEAIQCw";
		public const string ACH_LOST_ALL_PLAYER = "CgkIlLHvy7YHEAIQDA";
		public const string ACH_WIN_A_MATCH = "CgkIlLHvy7YHEAIQDQ";
		public const string ACH_MAKE_AN_EVEN = "CgkIlLHvy7YHEAIQDg";
		public const string ACH_LOSE_A_MATCH = "CgkIlLHvy7YHEAIQDw";
		public const string ACH_WIN_AN_ONLINE_MATCH = "CgkIlLHvy7YHEAIQEA";
		public const string ACH_WIN_A_BLUETOOTH_MATCH = "CgkIlLHvy7YHEAIQEQ";
}
