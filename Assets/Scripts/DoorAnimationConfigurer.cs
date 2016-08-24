using UnityEngine;
using System.Collections;

public class DoorAnimationConfigurer : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				
		}

		bool firstTime = false;
		void Update ()
		{
				if (firstTime == false) {
						firstTime = true;
						TweenPosition tween = this.GetComponent<TweenPosition> ();
						tween.from.x = transform.localPosition.x;
						tween.to.x = transform.localPosition.x;
						tween.PlayForward ();
				}
		}

}
	

