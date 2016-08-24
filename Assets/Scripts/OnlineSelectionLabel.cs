using UnityEngine;
using System.Collections;

public class OnlineSelectionLabel : MonoBehaviour
{

		public UILabel label;
		public bool leftLable;
		public GameObject SelectionWindows, WaitingWindow;

		void Start ()
		{
				PlayerEnum player = PlayerEnumUtils.GetCurrentPlayer ();
				bool leftPlayer = (player == PlayerEnum.LEFT);
				if (leftLable == leftPlayer) {
						label.text = Localization.Get ("Choose Team");
						SelectionWindows.SetActive (true);
						WaitingWindow.SetActive (false);
				} else {
						label.text = Localization.Get ("The Opponent");
						SelectionWindows.SetActive (false);
						WaitingWindow.SetActive (true);
				}
		}
}
