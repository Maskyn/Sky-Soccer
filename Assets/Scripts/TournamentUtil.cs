using UnityEngine;
using System.Collections;

public class TournamentUtil
{
		static TournamentUtil instance = null;
		private byte[] r16Ids, quarterIds, semifinalIds, finalIds;
		private byte[] r16Goals, quarterGoals, semifinalGoals, finalGoals;

		private TournamentUtil ()
		{
				r16Ids = new byte[16];
				quarterIds = new byte[8];
				semifinalIds = new byte[4];
				finalIds = new byte[2];
				r16Goals = new byte[16];
				quarterGoals = new byte[8];
				semifinalGoals = new byte[4];
				finalGoals = new byte[2];
		}

		public void Destroy ()
		{
				instance = null;
		}
	
		public static TournamentUtil Instance {
				get {
						if (instance == null)
								instance = new TournamentUtil ();
						return instance; 
				}
		}

		public bool IsInR16 {
				get;
				set;
		}

		public bool IsInQuarter {
				get;
				set;
		}

		public bool IsInSemiFinals {
				get;
				set;
		}

		public bool IsInFinals {
				get;
				set;
		}

		public byte[] R16Ids {
				get {
						return r16Ids;
				}
				set {
						r16Ids = value;
				}
		}

		public byte[] QuarterIds {
				get {
						return quarterIds;
				}
				set {
						quarterIds = value;
				}
		}

		public byte[] SemiFinalsIds {
				get {
						return semifinalIds;
				}
				set {
						semifinalIds = value;
				}
		}

		public byte[] FinalsIds {
				get {
						return finalIds;
				}
				set {
						finalIds = value;
				}
		}

		public byte[] R16Goals {
				get {
						return r16Goals;
				}
				set {
						r16Goals = value;
				}
		}
	
		public byte[] QuarterGoals {
				get {
						return quarterGoals;
				}
				set {
						quarterGoals = value;
				}
		}
	
		public byte[] SemiFinalsGoals {
				get {
						return semifinalGoals;
				}
				set {
						semifinalGoals = value;
				}
		}
	
		public byte[] FinalsGoals {
				get {
						return finalGoals;
				}
				set {
						finalGoals = value;
				}
		}

		public int RealMatchGoal01 {
				get;
				set;
		}

		public int RealMatchGoal02 {
				get;
				set;
		}
}
