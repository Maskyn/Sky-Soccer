using UnityEngine;
using System.Collections;

public class CenterX : MonoBehaviour
{
		public float x;

		public void Center ()
		{
				transform.localPosition = new Vector3 (0f, transform.localPosition.y, transform.localPosition.z);
		}

		public void ReturnToPreviousX ()
		{
				transform.localPosition = new Vector3 (x, transform.localPosition.y, transform.localPosition.z);
		}
}
