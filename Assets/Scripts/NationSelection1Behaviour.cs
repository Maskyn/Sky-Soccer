using UnityEngine;
using System.Collections;

public class NationSelection1Behaviour : MonoBehaviour
{

		public float x;
		public GameObject nationSelection01, nationSelection02;
		private Vector3 sx, center;

		void Start ()
		{
				sx = new Vector3 (x, nationSelection01.transform.localPosition.y, nationSelection01.transform.localPosition.z);
				center = new Vector3 (0f, nationSelection01.transform.localPosition.y, nationSelection01.transform.localPosition.z);
		}

		void Update ()
		{
				
				if (nationSelection02.activeSelf && isOnLeft () == false)
						nationSelection01.transform.localPosition = sx;
				else if (nationSelection02.activeSelf == false && isOnLeft () == true)
						nationSelection01.transform.localPosition = center;
						
		}

		private bool isOnLeft ()
		{
				return nationSelection01.transform.localPosition.x < 0;
		}
}
