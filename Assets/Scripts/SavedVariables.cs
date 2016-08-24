using UnityEngine;
using System.Collections;

public class SavedVariables
{
		public static readonly string KEY_SOUND_ENABLED = "sound_enabled";

		public static readonly string KEY_FIRST_PLAY = "first_play";

		public static readonly string KEY_FIRST_OPENING = "first_opening";

		public static readonly string KEY_LANGUAGE = "lingua";
	
		public static readonly string LEADERBOARD_GOALS = "leaderboard_goals";
		public static readonly string LEADERBOARD_POINTS = "leaderboard_points";
		public static readonly string LEADERBOARD_PLAYED_MATCHES = "leaderboard_played_matches";
		public static readonly string LEADERBOARD_WON_MATCHES = "leaderboard_won_matches";
	
		public static readonly int TRUE_INT = 1;
		public static readonly int FALSE_INT = 0;

		public static bool SoundEnabled {
				get {
						if (PlayerPrefs.HasKey (KEY_SOUND_ENABLED) == false)
								return true;
						else 
								return PlayerPrefs.GetInt (KEY_SOUND_ENABLED) == TRUE_INT;
				}
				set {
						if (value == true) {
								PlayerPrefs.SetInt (KEY_SOUND_ENABLED, TRUE_INT);
						} else {
								PlayerPrefs.SetInt (KEY_SOUND_ENABLED, FALSE_INT);
						}
						PlayerPrefs.Save ();
				}
		}

		public static bool FirstPlay {
				get {
						if (PlayerPrefs.HasKey (KEY_FIRST_PLAY) == false)
								return true;
						else 
								return PlayerPrefs.GetInt (KEY_FIRST_PLAY) == TRUE_INT;
				}
				set {
						if (value == true) {
								PlayerPrefs.SetInt (KEY_FIRST_PLAY, TRUE_INT);
						} else {
								PlayerPrefs.SetInt (KEY_FIRST_PLAY, FALSE_INT);
						}
						PlayerPrefs.Save ();
				}
		}

		public static bool FirstOpening {
				get {
						if (PlayerPrefs.HasKey (KEY_FIRST_OPENING) == false)
								return true;
						else 
								return PlayerPrefs.GetInt (KEY_FIRST_OPENING) == TRUE_INT;
				}
				set {
						if (value == true) {
								PlayerPrefs.SetInt (KEY_FIRST_OPENING, TRUE_INT);
						} else {
								PlayerPrefs.SetInt (KEY_FIRST_OPENING, FALSE_INT);
						}
						PlayerPrefs.Save ();
				}
		}

		public static string[] languages = new string[] {
		"English", "Italiano", "Francese", "Tedesco", "Spagnolo", "Russo"
	};

		public static string Language {
				get {
						if (PlayerPrefs.HasKey (KEY_LANGUAGE) == false) {
								string lang = "";
								switch (Application.systemLanguage) {
								case SystemLanguage.Italian:
										lang = "Italiano";
										break;
								case SystemLanguage.French:
										lang = "Francese";
										break;
								case SystemLanguage.German:
										lang = "Tedesco";
										break;
								case SystemLanguage.Spanish:
										lang = "Spagnolo";
										break;
								case SystemLanguage.Russian:
										lang = "Russo";
										break;
								default:
										lang = "English";
										break;
								}
								PlayerPrefs.SetString (KEY_LANGUAGE, lang);
								PlayerPrefs.Save ();
								return lang;
						} else {
								bool found = false;
								int i = 0;
								string currentLang = PlayerPrefs.GetString (KEY_LANGUAGE);
								while (found == false && i < languages.Length) {
										if (languages [i].Equals (currentLang))
												found = true;
										i++;
								} 
								if (found == false) {
										currentLang = "English";
										PlayerPrefs.SetString (KEY_LANGUAGE, currentLang);
										PlayerPrefs.Save ();
								}
								return currentLang;
						}
				}				
				set {
						PlayerPrefs.SetString (KEY_LANGUAGE, value);
						PlayerPrefs.Save ();
				}
		}

		public static int LeaderBoardPoints {
				get {
						if (PlayerPrefs.HasKey (LEADERBOARD_POINTS) == false)
								return 0;
						else 
								return PlayerPrefs.GetInt (LEADERBOARD_POINTS);
				}
				set {
						PlayerPrefs.SetInt (LEADERBOARD_POINTS, value);
						PlayerPrefs.Save ();
				}
		}

		public static int LeaderBoardGoals {
				get {
						if (PlayerPrefs.HasKey (LEADERBOARD_GOALS) == false)
								return 0;
						else 
								return PlayerPrefs.GetInt (LEADERBOARD_GOALS);
				}
				set {
						PlayerPrefs.SetInt (LEADERBOARD_GOALS, value);
						PlayerPrefs.Save ();
				}
		}

		public static int LeaderBoardPlayedMatches {
				get {
						if (PlayerPrefs.HasKey (LEADERBOARD_PLAYED_MATCHES) == false)
								return 0;
						else 
								return PlayerPrefs.GetInt (LEADERBOARD_PLAYED_MATCHES);
				}
				set {
						PlayerPrefs.SetInt (LEADERBOARD_PLAYED_MATCHES, value);
						PlayerPrefs.Save ();
				}
		}

		public static int LeaderBoardWonMatches {
				get {
						if (PlayerPrefs.HasKey (LEADERBOARD_WON_MATCHES) == false)
								return 0;
						else 
								return PlayerPrefs.GetInt (LEADERBOARD_WON_MATCHES);
				}
				set {
						PlayerPrefs.SetInt (LEADERBOARD_WON_MATCHES, value);
						PlayerPrefs.Save ();
				}
		}


}
