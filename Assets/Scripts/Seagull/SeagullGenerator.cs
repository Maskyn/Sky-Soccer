using UnityEngine;
using System.Collections;

public class SeagullGenerator : MonoBehaviour {

	public GameObject seagull;
	public float velocityX;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Create", 0f, 5f);
	}

	void Create() {
		
		GameObject go = Instantiate(seagull, new Vector3(transform.position.x, Random.Range(-2f, 2f), transform.position.z), Quaternion.identity) as GameObject;
		go.GetComponent<SeagullController>().SetVelocityX(velocityX);
	}
}
