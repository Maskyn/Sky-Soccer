using UnityEngine;
using System.Collections;

public class ConfigureOnlineMenu : MonoBehaviour
{

		public GameObject Selection01, Selection02;
		// Use this for initialization
		void OnEnable ()
		{
				PlayerEnum player = PlayerEnumUtils.GetCurrentPlayer ();
				if (player == PlayerEnum.LEFT) {
						Selection01.SetActive (true);
						Selection02.SetActive (false);
				} else {
						Selection01.SetActive (false);
						Selection02.SetActive (true);
				}
			
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
