using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;

public class TournamentManager : MonoBehaviour
{
		public UI2DSprite[]  R16Flags, QuartiFlags, SemifinaleFlags, FInaleFlags;
		public UILabel[]  R16Goals, QuartiGoals, SemifinaleGoals, FInaleGoals;
		public UI2DSprite[]  R16Bg, QuartiBg, SemifinaleBg, FInaleBg;
		public Sprite bgRosso;
		public GameObject NextButton;
		public GameObject DialogTournamentWon;


		void Start ()
		{

				while (Game.Instance.Nation01 == Nationals.NONE) {
						System.Array A = System.Enum.GetValues (typeof(Nationals));
						Nationals V = (Nationals)A.GetValue (UnityEngine.Random.Range (0, A.Length));
						Game.Instance.Nation01 = V;
				}
				Game.Instance.TournamentMode = true;

				


				/*RealPlayerId = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
				TournamentUtil.Instance.R16Ids [0] = RealPlayerId;
				TournamentUtil.Instance.QuarterIds [0] = RealPlayerId;
				TournamentUtil.Instance.SemiFinalsIds [0] = RealPlayerId;
				TournamentUtil.Instance.FinalsIds [0] = RealPlayerId;*/
				
				NextRound ();
		}

		private void NextRound ()
		{

				if (TournamentUtil.Instance.IsInFinals) {
						SetWinner ();
						NextButton.SetActive (false);
						DialogTournamentWon.SetActive (true);
						TournamentUtil.Instance.Destroy ();
						if (PlayGamesPlatform.Instance.IsAuthenticated ()) {
								Social.ReportProgress (Constants.ACH_TOURNAMENT_WON, 100.0f, (bool success) => {
										// handle success or failure
								});
						}
				} else if (TournamentUtil.Instance.IsInSemiFinals) {
						SetFinals ();
				} else if (TournamentUtil.Instance.IsInQuarter) {
						SetSemiFinals ();
				} else if (TournamentUtil.Instance.IsInR16) {
						SetQuarter ();
				} else {
						SetR16 ();
				}

		}

		public void Next ()
		{
				if (TournamentUtil.Instance.IsInFinals) {
						Game.Instance.Nation02 = NationalSuits.getNationById (TournamentUtil.Instance.FinalsIds [1]);
				} else if (TournamentUtil.Instance.IsInSemiFinals) {
						Game.Instance.Nation02 = NationalSuits.getNationById (TournamentUtil.Instance.SemiFinalsIds [1]);
				} else if (TournamentUtil.Instance.IsInQuarter) {
						Game.Instance.Nation02 = NationalSuits.getNationById (TournamentUtil.Instance.QuarterIds [1]);
				} else if (TournamentUtil.Instance.IsInR16) {
						Game.Instance.Nation02 = NationalSuits.getNationById (TournamentUtil.Instance.R16Ids [1]);
				}
				Game.Instance.LandscapeId = 255;
				Game.Instance.PlayerScore01 = 0;
				Game.Instance.PlayerScore02 = 0;
				Game.Instance.RemainingPlayers01 = Game.TEAM_PLAYERS;
				Game.Instance.RemainingPlayers02 = Game.TEAM_PLAYERS;
				Game.Instance.TimerActive = true;
				Game.Instance.TimeRemained = 120.2f;
				Application.LoadLevelAsync (Constants.LEVEL_GAMEPLAY);
		}
	
		public void Reload ()
		{
				Application.LoadLevel (Application.loadedLevel);
		}

		public void Exit ()
		{
				TournamentUtil.Instance.Destroy ();
				Application.LoadLevelAsync (Constants.LEVEL_MENU);
		}

		

		byte getId (byte[] ids)
		{
				int id = Random.Range (0, 35);
				int i = 0;
				int lenght = ids.Length;
				while (i < lenght) {
						if (id == ids [i]) {
								id = Random.Range (0, 35);
								i = -1; // perche se si mette 0 riparte da 1
						}
						i++;
				}
				return (byte)id;
		}

		private int getGoal02 (int goal01)
		{
				int goal02;
				do {
						goal02 = (byte)Random.Range (0, 5);
				} while (goal02 == goal01);
				return goal02;
		}

		Partita CosaGoal (int i, byte[] ids, byte[] goals)
		{
				Partita partita = new Partita ();

				if (i == 0) {
						partita.Id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						partita.Goal01 = Game.Instance.PlayerScore01;
						partita.Goal02 = Game.Instance.PlayerScore02;
				
				} else {
						partita.Goal01 = (byte)Random.Range (0, 5);
						partita.Goal02 = (byte)getGoal02 (partita.Goal01);
						partita.Id = partita.Goal01 > partita.Goal02 ? ids [i * 2] : ids [i * 2 + 1];
				}
				return partita;
		}

		public class Partita
		{
				public byte Id {
						get;
						set;
				}
				public byte Goal01 {
						get;
						set;
				}
				public byte Goal02 {
						get;
						set;
				}
		}

		void SetR16 ()
		{
				TournamentUtil.Instance.IsInR16 = true;
				for (int i = 0; i<TournamentUtil.Instance.R16Ids.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = getId (TournamentUtil.Instance.R16Ids);
						}
						TournamentUtil.Instance.R16Ids [i] = id;
						R16Flags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
				}
		}

		void SetQuarter ()
		{

				for (int i = 0; i<TournamentUtil.Instance.R16Ids.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.R16Ids [i];
						}
						R16Flags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
				}
		
				TournamentUtil.Instance.IsInQuarter = true;
				for (int i = 0; i<TournamentUtil.Instance.QuarterIds.Length; i++) {
						Partita partita = CosaGoal (i, TournamentUtil.Instance.R16Ids, TournamentUtil.Instance.R16Goals);
						QuartiFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (partita.Id));
						R16Goals [i * 2].text = "" + partita.Goal01;
						R16Goals [i * 2 + 1].text = "" + partita.Goal02;
						if (partita.Goal01 > partita.Goal02)
								R16Bg [i * 2].sprite2D = bgRosso;
						else
								R16Bg [i * 2 + 1].sprite2D = bgRosso;
						TournamentUtil.Instance.QuarterIds [i] = partita.Id;
						TournamentUtil.Instance.R16Goals [i * 2] = partita.Goal01;
						TournamentUtil.Instance.R16Goals [i * 2 + 1] = partita.Goal02;
				}

			
		}

		void SetSemiFinals ()
		{

				for (int i = 0; i<TournamentUtil.Instance.R16Ids.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.R16Ids [i];
						}
						R16Flags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
				}
		
				for (int i = 0; i<TournamentUtil.Instance.QuarterIds.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.QuarterIds [i];
						}
						QuartiFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
						R16Goals [i * 2].text = "" + TournamentUtil.Instance.R16Goals [i * 2];
						R16Goals [i * 2 + 1].text = "" + TournamentUtil.Instance.R16Goals [i * 2 + 1];
						if (TournamentUtil.Instance.R16Goals [i * 2] > TournamentUtil.Instance.R16Goals [i * 2 + 1])
								R16Bg [i * 2].sprite2D = bgRosso;
						else
								R16Bg [i * 2 + 1].sprite2D = bgRosso;
				}

				TournamentUtil.Instance.IsInSemiFinals = true;
				for (int i = 0; i<TournamentUtil.Instance.SemiFinalsIds.Length; i++) {
						Partita partita = CosaGoal (i, TournamentUtil.Instance.QuarterIds, TournamentUtil.Instance.QuarterGoals);
						SemifinaleFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (partita.Id));
						QuartiGoals [i * 2].text = "" + partita.Goal01;
						QuartiGoals [i * 2 + 1].text = "" + partita.Goal02;
						if (partita.Goal01 > partita.Goal02)
								QuartiBg [i * 2].sprite2D = bgRosso;
						else
								QuartiBg [i * 2 + 1].sprite2D = bgRosso;
						TournamentUtil.Instance.SemiFinalsIds [i] = partita.Id;
						TournamentUtil.Instance.QuarterGoals [i * 2] = partita.Goal01;
						TournamentUtil.Instance.QuarterGoals [i * 2 + 1] = partita.Goal02;
				}
		}

		void SetFinals ()
		{

				for (int i = 0; i<TournamentUtil.Instance.R16Ids.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.R16Ids [i];
						}
						R16Flags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
				}
		
				for (int i = 0; i<TournamentUtil.Instance.QuarterIds.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.QuarterIds [i];
						}
						QuartiFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
						R16Goals [i * 2].text = "" + TournamentUtil.Instance.R16Goals [i * 2];
						R16Goals [i * 2 + 1].text = "" + TournamentUtil.Instance.R16Goals [i * 2 + 1];
						if (TournamentUtil.Instance.R16Goals [i * 2] > TournamentUtil.Instance.R16Goals [i * 2 + 1])
								R16Bg [i * 2].sprite2D = bgRosso;
						else
								R16Bg [i * 2 + 1].sprite2D = bgRosso;
				}

				for (int i = 0; i<TournamentUtil.Instance.SemiFinalsIds.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.SemiFinalsIds [i];
						}
						SemifinaleFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
						QuartiGoals [i * 2].text = "" + TournamentUtil.Instance.QuarterGoals [i * 2];
						QuartiGoals [i * 2 + 1].text = "" + TournamentUtil.Instance.QuarterGoals [i * 2 + 1];

						if (TournamentUtil.Instance.QuarterGoals [i * 2] > TournamentUtil.Instance.QuarterGoals [i * 2 + 1])
								QuartiBg [i * 2].sprite2D = bgRosso;
						else
								QuartiBg [i * 2 + 1].sprite2D = bgRosso;
				}

				TournamentUtil.Instance.IsInFinals = true;
				for (int i = 0; i<TournamentUtil.Instance.FinalsIds.Length; i++) {
						Partita partita = CosaGoal (i, TournamentUtil.Instance.SemiFinalsIds, TournamentUtil.Instance.SemiFinalsGoals);
						FInaleFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (partita.Id));
						SemifinaleGoals [i * 2].text = "" + partita.Goal01;
						SemifinaleGoals [i * 2 + 1].text = "" + partita.Goal02;
						if (partita.Goal01 > partita.Goal02)
								SemifinaleBg [i * 2].sprite2D = bgRosso;
						else
								SemifinaleBg [i * 2 + 1].sprite2D = bgRosso;
						TournamentUtil.Instance.FinalsIds [i] = partita.Id;
						TournamentUtil.Instance.SemiFinalsGoals [i * 2] = partita.Goal01;
						TournamentUtil.Instance.SemiFinalsGoals [i * 2 + 1] = partita.Goal02;
				}
		}

		void SetWinner ()
		{
				for (int i = 0; i<TournamentUtil.Instance.R16Ids.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.R16Ids [i];
						}
						R16Flags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
				}
		
				for (int i = 0; i<TournamentUtil.Instance.QuarterIds.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.QuarterIds [i];
						}
						QuartiFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
						R16Goals [i * 2].text = "" + TournamentUtil.Instance.R16Goals [i * 2];
						R16Goals [i * 2 + 1].text = "" + TournamentUtil.Instance.R16Goals [i * 2 + 1];
						if (TournamentUtil.Instance.R16Goals [i * 2] > TournamentUtil.Instance.R16Goals [i * 2 + 1])
								R16Bg [i * 2].sprite2D = bgRosso;
						else
								R16Bg [i * 2 + 1].sprite2D = bgRosso;
				}
		
				for (int i = 0; i<TournamentUtil.Instance.SemiFinalsIds.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.SemiFinalsIds [i];
						}
						SemifinaleFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
						QuartiGoals [i * 2].text = "" + TournamentUtil.Instance.QuarterGoals [i * 2];
						QuartiGoals [i * 2 + 1].text = "" + TournamentUtil.Instance.QuarterGoals [i * 2 + 1];
						if (TournamentUtil.Instance.QuarterGoals [i * 2] > TournamentUtil.Instance.QuarterGoals [i * 2 + 1])
								QuartiBg [i * 2].sprite2D = bgRosso;
						else
								QuartiBg [i * 2 + 1].sprite2D = bgRosso;
				}
		
				for (int i = 0; i<TournamentUtil.Instance.FinalsIds.Length; i++) {
						byte id;
						if (i == 0) {
								id = (byte)NationalSuits.getNationId (Game.Instance.Nation01);
						} else {
								id = TournamentUtil.Instance.FinalsIds [i];
						}
						FInaleFlags [i].sprite2D = NationalSuits.getNationFlag (NationalSuits.getNationById (id));
						SemifinaleGoals [i * 2].text = "" + TournamentUtil.Instance.SemiFinalsGoals [i * 2];
						SemifinaleGoals [i * 2 + 1].text = "" + TournamentUtil.Instance.SemiFinalsGoals [i * 2 + 1];
						if (TournamentUtil.Instance.SemiFinalsGoals [i * 2] > TournamentUtil.Instance.SemiFinalsGoals [i * 2 + 1])
								SemifinaleBg [i * 2].sprite2D = bgRosso;
						else
								SemifinaleBg [i * 2 + 1].sprite2D = bgRosso;
				}	

				Partita partita = CosaGoal (0, TournamentUtil.Instance.FinalsIds, TournamentUtil.Instance.FinalsIds);
				FInaleGoals [0].text = "" + partita.Goal01;
				FInaleGoals [1].text = "" + partita.Goal02;
				if (partita.Goal01 > partita.Goal02)
						FInaleBg [0].sprite2D = bgRosso;
				else
						FInaleBg [1].sprite2D = bgRosso;
		}
}
