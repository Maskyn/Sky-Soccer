using UnityEngine;
using System.Collections;

public class NetworkViewBehaviour : MonoBehaviour
{

		void Start ()
		{
#if UNITY_ANDROID || UNITY_EDITOR
				NetworkView nv;
				nv = gameObject.AddComponent<NetworkView> ();
				nv.stateSynchronization = NetworkStateSynchronization.Off;
				//nv.observed = null;
				//nv.viewID = 1;
#endif
		}
}
