using UnityEngine;
using System.Collections;

public class GridItemDistance : MonoBehaviour {

	void Awake () {
		for(byte i = 0; i < transform.parent.childCount; i++) {
			if(transform.parent.GetChild(i) == this.transform) {
				transform.localPosition = new Vector3(0, i*1150*-1, 0);
				// no need to have this script

			}
		}
	}
}
