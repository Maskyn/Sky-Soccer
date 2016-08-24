using UnityEngine;
using System.Collections;

public class ScreenWallController : MonoBehaviour {

	private readonly float REJECT_FORCE = 40;

	private readonly string TOP = "Top";
	private readonly string BOTTOM = "Bottom";
	private readonly string LEFT = "Left";
	private readonly string RIGHT = "Right";

	void OnCollisionEnter2D(Collision2D coll) {
		float force = REJECT_FORCE; // small force
		if (coll.gameObject.tag == Constants.TAG_BALL) {
			if(name.Contains(TOP)) {
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -force));
			} else if(name.Contains(BOTTOM)) {
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
			} else if(name.Contains(LEFT)) {
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0));
			} else if(name.Contains(RIGHT)) {
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force, 0));
			}
		}
		
	}

	void OnTriggerExit2D(Collider2D other) { // if is trigger is the death
		if(gameObject.tag == Constants.TAG_DEATH_TRIGGER && other.tag == Constants.TAG_PLAYER) {
			//GameObject.Destroy (other.gameObject);
			other.gameObject.SetActive(false);
		}

		if(gameObject.tag == Constants.TAG_DEATH_TRIGGER && gameObject.name.Contains(TOP) && other.tag == Constants.TAG_WIND) {
			//GameObject.Destroy (other.gameObject);
			other.gameObject.SetActive(false);
		}
	}
}
