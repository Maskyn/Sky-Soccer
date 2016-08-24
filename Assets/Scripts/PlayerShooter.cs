using UnityEngine;
using System.Collections;

/**
 * Class used to shoot player
 **/
public class PlayerShooter
{

		// there are two teams (array of players)
		// the first team is on the left
		private GameObject[] teamL; 
		// the other one is on the right
		private GameObject[] teamR;
		// the parent that will have the other players as child
		private GameObject playersParent;

		private int playerShootForce = 1400;

		// Singleton
		// Instance
		private static readonly PlayerShooter instance = new PlayerShooter ();
	
		private PlayerShooter ()
		{
		}
	
		public static PlayerShooter Instance {
				get {
						return instance; 
				}
		}

		// populates the array of players with the players
		public void SetupPlayers (GameObject player01, GameObject player02, GameObject goalKeeper)
		{
				// players parent isn't set we set it here
				if (playersParent == null) {
						playersParent = GameObject.FindGameObjectWithTag (Constants.TAG_PARENT_PLAYERS);
				}
				// create two empty arrays
				teamL = new GameObject[Game.TEAM_PLAYERS];
				teamR = new GameObject[Game.TEAM_PLAYERS];

				// for cycle
				for (byte i=0; i<Game.TEAM_PLAYERS; i++) {
						// the first player is the goal keeper
						if (i == 0) {
								teamL [i] = GameObject.Instantiate (goalKeeper, Vector3.zero, Quaternion.identity) as GameObject;
								teamR [i] = GameObject.Instantiate (goalKeeper, Vector3.zero, Quaternion.identity) as GameObject;
						}
			// the other ones are normal player
			else {
								teamL [i] = GameObject.Instantiate (player01, Vector3.zero, Quaternion.identity) as GameObject;
								teamR [i] = GameObject.Instantiate (player02, Vector3.zero, Quaternion.identity) as GameObject;
						}
						// we deactivate every player
						teamL [i].SetActive (false);
						teamR [i].SetActive (false);
						// and set the playersParent as the parent of every player
						teamL [i].transform.parent = playersParent.transform;
						teamR [i].transform.parent = playersParent.transform;
				}
		}

		// shoot a player
		public void Shoot (float posX, float posY, float cos, float sin)
		{
				PlayerEnum player;
				if (posX <= 0)
						player = PlayerEnum.LEFT;
				else
						player = PlayerEnum.RIGHT;
				// if the left player doesn't have players or the right player doesn't have any we return and don't do anything
				if ((player == PlayerEnum.LEFT && Game.Instance.RemainingPlayers01 <= 0)
						|| ((player == PlayerEnum.RIGHT) && Game.Instance.RemainingPlayers02 <= 0))
						return;

				// set the position of the player
				Vector3 instantiatePosition = new Vector3 (posX, posY, 0);

				// get how many degress from the cos. Note: it will be between 0 and 180
				float Degress = (Mathf.Acos (cos) * Mathf.Rad2Deg);
				// if sin is less than 0 we are in the 3rd or 4th quarter so degresses go from 180 to 360
				if (sin < 0) {
						Degress = 360 - Degress;
				}

				// rotate the player
				Quaternion rotation = Quaternion.Euler (new Vector3 (0, 0, Degress));

				// player number in list
				int playerId;
				// the player to shoot
				GameObject playerToShoot;
				// if the left team wants to shoot
				if (player == PlayerEnum.LEFT) {
						Game.Instance.RemainingPlayers01--;
						playerId = Game.Instance.RemainingPlayers01;
						playerToShoot = teamL [playerId];

				} 
				// if the right team wants to shoot
				else if (player == PlayerEnum.RIGHT) {
						Game.Instance.RemainingPlayers02--;
						playerId = Game.Instance.RemainingPlayers02;
						playerToShoot = teamR [playerId];

				} else {
						return;
				}

				// activate this player, move it to spawn point and set the rotation
				playerToShoot.SetActive (true);
				playerToShoot.transform.position = instantiatePosition;
				playerToShoot.transform.rotation = rotation;

				// the player scale
				Vector3 scale = playerToShoot.transform.localScale;
				// if the player direction is the left side we rotate on y
				if (posX > 0)
						scale.y = -1;
				else
						scale.y = +1;
				playerToShoot.transform.localScale = scale;

				float forceX = playerShootForce * cos;
				float forceY = playerShootForce * sin;

				// we give a force to the player
				playerToShoot.GetComponent<Rigidbody2D>().AddForce (new Vector2 (forceX, forceY));
		}


}
