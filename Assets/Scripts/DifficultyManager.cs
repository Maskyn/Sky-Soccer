using UnityEngine;
using System.Collections;

public class DifficultyManager
{

		static DifficultyManager instance;

		public enum Difficulties
		{
				EASY,
				NORMAL, 
				INSANE
		}
	

		private DifficultyManager ()
		{
				Reset ();
		}
	
		public static DifficultyManager Instance {
				get {
						if (instance == null)
								instance = new DifficultyManager ();
						return instance; 
				}
		}

		public void Reset ()
		{
				Difficult = Difficulties.NORMAL;
		}

		public Difficulties Difficult {
				get;
				set;
		}

		public float DistanceBallEnemie {
				get {
						switch (Difficult) {
						case Difficulties.EASY:
								return 2.5f;
						case Difficulties.NORMAL:
								return 1.2f;
						case Difficulties.INSANE:
								return 0.8f;
						default: 
								return 1.2f;
						}
				}
		}

		public float PointsRatio {
				get {
						switch (Difficult) {
						case Difficulties.EASY:
								return 0.66f;
						case Difficulties.NORMAL:
								return 1f;
						case Difficulties.INSANE:
								return 1.33f;
						default: 
								return 1f;
						}
				}
		}

		public float EnemieLongPreparationTime {
				get {
						switch (Difficult) {
						case Difficulties.EASY:
								return 3f;
						case Difficulties.NORMAL:
								return 2.5f;
						case Difficulties.INSANE:
								return 1.2f;
						default: 
								return 2.5f;
						}
				}
		}

		public float EnemieFastPreparationTime {
				get {
						switch (Difficult) {
						case Difficulties.EASY:
								return 2f;
						case Difficulties.NORMAL:
								return 1.2f;
						case Difficulties.INSANE:
								return 0.4f;
						default: 
								return 1.2f;
						}
				}
		}
	
}
