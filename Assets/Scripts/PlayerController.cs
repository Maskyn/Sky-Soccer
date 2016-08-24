using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
		anim.enabled = false;
		GetComponent<Rigidbody2D>().gravityScale = 1;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == Constants.TAG_BALL) {
			anim.enabled = true;
			GetComponent<Rigidbody2D>().gravityScale = 2;
		}
		
	}
}
