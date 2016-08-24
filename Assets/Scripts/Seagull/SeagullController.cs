using UnityEngine;
using System.Collections;

public class SeagullController : MonoBehaviour {

	private float velocityX = -1f;

	public void SetVelocityX(float x) {
		this.velocityX = x;
		GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, 0f);
	}
}
