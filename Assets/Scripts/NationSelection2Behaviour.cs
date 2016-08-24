using UnityEngine;
using System.Collections;

public class NationSelection2Behaviour : MonoBehaviour
{

		public GameObject selection02;

		void Update ()
		{
				if (((Game.Instance.LocalPlay && Game.Instance.TournamentMode == false) || Game.Instance.LocalMultiPlayer))
						selection02.SetActive (true);
				else
						selection02.SetActive (false);
		}
}
