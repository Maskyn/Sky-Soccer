using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class BallController : MonoBehaviour
{
		public GamePlaySetup gamePlaySetup;
		public AudioClip hitBall;
		private float initialGravityScale;
		private float maxGoalVelocity = 1f;


		void Awake ()
		{
				initialGravityScale = GetComponent<Rigidbody2D>().gravityScale;
				GetComponent<Rigidbody2D>().gravityScale = 2 * initialGravityScale;
		}

		void OnCollisionEnter2D (Collision2D other)
		{
				if (other.gameObject.tag == Constants.TAG_PLAYER) {
						GetComponent<Rigidbody2D>().gravityScale = initialGravityScale;
						AudioManager.PlaySfx (hitBall);
						gamePlaySetup.BallTouchedPlayer ();
				} else if (other.gameObject.tag == Constants.TAG_WALL) {
						bool leftPart = transform.position.x <= 0;
						gamePlaySetup.BallTouchedWall (leftPart);
				}
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == Constants.TAG_DOOR && Game.Instance.TimerActive == true) {
						gamePlaySetup.Goal (true);
						Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
						float velX = rb2D.velocity.x;
						float ratio = maxGoalVelocity / velX;
						GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x * ratio, GetComponent<Rigidbody2D>().velocity.y * ratio);
						
				}
		}

		public void ResetBall ()
		{
				GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
				GetComponent<Rigidbody2D>().gravityScale = 2 * initialGravityScale;
				Vector3 pos = new Vector3 (UnityEngine.Random.Range (-2f, 2f), UnityEngine.Random.Range (-2f, 2f), 0f);
				transform.position = pos;
				/*Vector3 pos;
				if (Game.Instance.LocalPlay)
						pos = new Vector3 (xNearDoor, UnityEngine.Random.Range (-3.2f, 3.2f), 0f);
				else
						pos = new Vector3 (0f, UnityEngine.Random.Range (-2f, 2f), 0f);
				transform.position = pos;*/
				//iTween.MoveTo(gameObject, iTween.Hash("x", 0d, "y", bodyPosition.y, "easeType", iTween.EaseType.linear, "delay", 0.15d, "onComplete", "StopBall"));
		}
		/*void StopBall() {
		anim.enabled = true;
	}*/
}
