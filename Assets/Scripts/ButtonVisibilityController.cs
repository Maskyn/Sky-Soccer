using UnityEngine;
using System.Collections;

public class ButtonVisibilityController : MonoBehaviour
{

		public GameObject window;

		public enum Actions
		{
				TOOGLE,
				ACTIVATE, 
				DEACTIVATE
		}

		public ButtonVisibilityController.Actions action;

		void OnClick ()
		{
				switch (action) {
				case Actions.TOOGLE:
						window.SetActive (!window.activeInHierarchy);
						break;
				case Actions.ACTIVATE:
						window.SetActive (true);
						break;
				case Actions.DEACTIVATE:
						window.SetActive (false);
						break;
				}
		}
}
