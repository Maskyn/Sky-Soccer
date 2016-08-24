using UnityEngine;
using System.Collections;

public class PointsManager
{

		private const float POINTS_GOAL = 1000;
		private const float POINTS_REMAINING_PLAYERS = 50;
		private const float POINTS_PENALTY_GOAL = 500;
		private const float POINTS_PENALTY_SAVED = 500;
		private const float POINTS_WALL_HIT_FOR_GOAL = 200;

		private int goalPoints;
		private int remPlayerPoints;
		private int otherPoints;

		private int penaltyDone;
		private int wallHits;

		private int pointsForLastGoal;
		private int pointsAlreadyShowed;


		public PointsManager ()
		{
				// points
				goalPoints = 0;
				remPlayerPoints = 0;
				otherPoints = 0;
				pointsForLastGoal = 0;
				pointsAlreadyShowed = 0;

				// how many penalty done 
				penaltyDone = 0;

				// how many times has the wall been hitted by player
				wallHits = 0;

		}

		public void Goal (bool penalty, byte remainedPlayers)
		{
				if (penalty) {

						otherPoints += (int)(POINTS_PENALTY_GOAL * DifficultyManager.Instance.PointsRatio);

						penaltyDone++;

						ResetWallHits ();
				} else {
						goalPoints += (int)(POINTS_GOAL * DifficultyManager.Instance.PointsRatio);

						remPlayerPoints += (int)(remainedPlayers * (POINTS_REMAINING_PLAYERS * DifficultyManager.Instance.PointsRatio));

						otherPoints += (int)(wallHits * (POINTS_WALL_HIT_FOR_GOAL * DifficultyManager.Instance.PointsRatio));

						ResetWallHits ();
				}
		}

		public void ResetWallHits ()
		{
				wallHits = 0;
		}

		public void WallHitted ()
		{
				wallHits++;
		}

		public int GoalPoints {
				get {
						return goalPoints;
				}
		}

		public int RemainedPlayerPoints {
				get {
						return remPlayerPoints;
				}
		}

		public int OtherPoints {
				get {
						return otherPoints;
				}
		}

		public int AllPoints {
				get {
						return GoalPoints + RemainedPlayerPoints + OtherPoints;
				}
		}

		public int PointsForLastGoal {
				get {
						pointsForLastGoal = AllPoints - pointsAlreadyShowed;
						pointsAlreadyShowed = AllPoints;
						return pointsForLastGoal;
				}
		}

		public int Goals {
				get {
						return (int)(GoalPoints / (POINTS_GOAL * DifficultyManager.Instance.PointsRatio));
				}
		}

		public int Stars {
				get {
						if (AllPoints <= 5000)
								return 0;
						else if (AllPoints <= 10000)
								return 1;
						else if (AllPoints <= 15000)
								return 2;
						else
								return 3;
				}
		}

}
