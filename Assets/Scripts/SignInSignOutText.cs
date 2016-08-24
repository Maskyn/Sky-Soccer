using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class SignInSignOutText : MonoBehaviour
{

		public UILabel label;

		void Start ()
		{
				UpdateText ();
		}

		void Update ()
		{

				UpdateText ();
				
		}

		void UpdateText ()
		{
				if (PlayGamesPlatform.Instance.IsAuthenticated ())
						label.text = Localization.Get ("SignOut");
				else
						label.text = Localization.Get ("SignIn");
		}
}
