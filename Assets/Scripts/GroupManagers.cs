using UnityEngine;
using System.Collections;

public class GroupManagers : MonoBehaviour
{

		public UILabel title;

		void Start ()
		{
				title.text = getTitle (0);
		}

		public void ShowNext ()
		{
				int i;
				for (i = 0; i < transform.childCount; i++) {

						if (transform.GetChild (i).gameObject.activeSelf == true) {
								if (i == transform.childCount - 1) {
										title.text = getTitle (0); // first group
										GameObject first, second;
										first = transform.GetChild (i).gameObject;
										second = transform.GetChild (0).gameObject;
										DoTheAnimation (first, second);			
										break;
								} else {
										title.text = getTitle (i + 1);
										GameObject first, second;
										first = transform.GetChild (i).gameObject;
										second = transform.GetChild (i + 1).gameObject;
										DoTheAnimation (first, second);
										break;
								}
								
						}
				}
				
		}

		public void ShowPrevious ()
		{
				int i;
				for (i = 0; i < transform.childCount; i++) {
						if (transform.GetChild (i).gameObject.activeSelf == true) {
								if (i == 0) {
										title.text = getTitle (transform.childCount - 1); // last group
										GameObject first, second;
										first = transform.GetChild (i).gameObject;
										second = transform.GetChild (transform.childCount - 1).gameObject;
										DoTheAnimation (first, second);	
										break;
								} else {
										title.text = getTitle (i - 1);
										GameObject first, second;
										first = transform.GetChild (i).gameObject;
										second = transform.GetChild (i - 1).gameObject;
										DoTheAnimation (first, second);
										break;
								}
								
						}
				}
		}

		public string getTitle (int index)
		{
				string letter;
				switch (index) {
				case 0:
						letter = "A";
						break;
				case 1:
						letter = "B";
						break;
				case 2:
						letter = "C";
						break;
				case 3:
						letter = "D";
						break;
				case 4:
						letter = "E";
						break;
				case 5:
						letter = "F";
						break;
				case 6:
						letter = "G";
						break;
				case 7:
						letter = "H";
						break;
				case 8:
						letter = "Special";
						break;
				default:
						letter = "Z";
						break;
				}
				if (letter.Equals ("Special"))
						return "Special";
				else
						return string.Format (Localization.Get ("GroupsUniversal"), letter);
		}

		public void DoTheAnimation (GameObject first, GameObject second)
		{
				first.SetActive (false);
				second.SetActive (true);
				//second.transform.localScale = new Vector3 (0.85f, 0.85f, 1f);
				//iTween.ScaleTo (second, iTween.Hash ("x", 1, "y", 1, "easetype", iTween.EaseType.linear, "time", 0.05f));
		}
}
