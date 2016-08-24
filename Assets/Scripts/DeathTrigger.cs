using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		GameObject.Destroy (other.gameObject);
	}
}
