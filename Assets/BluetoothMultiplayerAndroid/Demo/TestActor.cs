using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
// A very simple object. Moves to the position of the touch
public class TestActor : MonoBehaviour {
    private Vector3 _destination;

	void Start () {
	    _destination = transform.position;

        renderer.material.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
	}
	
	void Update () {
        if (networkView.isMine) {
            _destination.y = 0f;
            Vector3 dir = -(transform.position - _destination);

            if (dir.magnitude > 1f)
                transform.Translate(dir.normalized * 70f * Time.deltaTime);

            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo)) {
                    _destination = hitInfo.point;
                }
            }
        }

	}
}
#endif
