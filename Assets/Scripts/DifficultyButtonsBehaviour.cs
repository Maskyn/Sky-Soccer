using UnityEngine;
using System.Collections;

public class DifficultyButtonsBehaviour : MonoBehaviour
{

		public GameObject[] buttons;
	
		void Update ()
		{
				if (Game.Instance.LocalPlay)
						foreach (GameObject obj in buttons)
								obj.SetActive (true);
				else
						foreach (GameObject obj in buttons)
								obj.SetActive (false);
		}
}
