using UnityEngine;
using System.Collections;

public class OpponentStatus : MonoBehaviour
{

		bool opponentReady;
		public UILabel label;

		void Start ()
		{
				opponentReady = UniversalOnline.Instance.TheOpponentIsReady;
				Set ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (opponentReady = UniversalOnline.Instance.TheOpponentIsReady) {
						opponentReady = UniversalOnline.Instance.TheOpponentIsReady;
						Set ();
				}
		}

		void Set ()
		{
				if (opponentReady)
						label.text = Localization.Get ("Ready");
				else
						label.text = Localization.Get ("Is Selecting Team");
		}
}
