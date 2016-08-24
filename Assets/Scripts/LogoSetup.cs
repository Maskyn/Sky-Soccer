using UnityEngine;
using System.Collections;

public class LogoSetup : MonoBehaviour
{
	
		// Use this for initialization
		public void LoadScene ()
		{
				Application.LoadLevelAsync (Constants.LEVEL_MENU);
		}
}
