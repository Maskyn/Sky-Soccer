using UnityEngine;
using System.Collections;

public class TotalPointsMainMenu : MonoBehaviour
{

		int totalPoints;
		public UILabel label;
		private string language;

		void Start ()
		{
				totalPoints = SavedVariables.LeaderBoardPoints;
				label.text = Localization.Get ("Total Points") + "\n" + totalPoints;
				language = Localization.language;
		}
	
		void Update ()
		{
				if (totalPoints != SavedVariables.LeaderBoardPoints || language != Localization.language) {
						totalPoints = SavedVariables.LeaderBoardPoints;
						label.text = Localization.Get ("Total Points") + "\n" + totalPoints;
				}
		}
}
