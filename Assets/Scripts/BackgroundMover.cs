using UnityEngine;
using System.Collections;

public class BackgroundMover : MonoBehaviour {

	void Start () {
		iTween.MoveTo(this.gameObject, 		iTween.Hash("y", 0, "easetype", iTween.EaseType.linear, "time", 240));
	}
}
