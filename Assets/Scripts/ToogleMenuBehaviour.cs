using UnityEngine;
using System.Collections;

public class ToogleMenuBehaviour : MonoBehaviour
{

		public void ToggleActive ()
		{
				NGUITools.SetActive (gameObject, !NGUITools.GetActive (gameObject));
		}

		public void DeActive ()
		{
				NGUITools.SetActive (gameObject, false);
		}

		public void Active ()
		{
				NGUITools.SetActive (gameObject, true);
		}
}
