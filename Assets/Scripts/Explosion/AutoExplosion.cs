
using UnityEngine;
using System.Collections;

public class AutoExplosion : MonoBehaviour
{
	public float explosion_rate = 5f;
	public float explosion_max_size= 30f;
	public float speed = 5f;
	public float current_radius = 0;

	CircleCollider2D explosion_radius;
	bool exploded;

	// Use this for initialization
	void Start ()
	{
		explosion_radius = gameObject.GetComponent<CircleCollider2D>();
	}

	void OnCollisionEnter2D(Collision2D coll) {

		exploded = true;
	}

	void FixedUpdate() {
		if (exploded) {
			if (current_radius < explosion_max_size) {
				current_radius += explosion_rate;
			} else {
				Object.Destroy(gameObject);
			}
			
			explosion_radius.radius = current_radius;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (exploded && coll.GetComponent<Rigidbody2D>() != null) {
			Vector2 target = coll.transform.position;
			Vector2 bomb = transform.position;
			
			Vector2 direction = 500f * (target - bomb);
			
			coll.GetComponent<Rigidbody2D>().AddForce (direction);
			
			print ("exploded");
		}

	}
	
	
}