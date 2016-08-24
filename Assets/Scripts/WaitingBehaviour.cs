using UnityEngine;
using System.Collections;

public class WaitingBehaviour : MonoBehaviour
{

		public GameObject WaitingDialog;
		public UILabel WaitingLabel;
	
		// Update is called once per frame
		void Update ()
		{
				if (UniversalOnline.Instance.Progress > 0) {
						WaitingDialog.SetActive (true);
						WaitingLabel.text = UniversalOnline.Instance.State.ToString () + " " + UniversalOnline.Instance.Progress;
				} else {
						WaitingDialog.SetActive (false);
				}
		}
}
