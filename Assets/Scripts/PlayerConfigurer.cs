using UnityEngine;
using System.Collections;

public class PlayerConfigurer
{

		public static void ConfigurePlayer (GameObject player, NationalSuit suit)
		{

				if (BackgroundChooser.isSpace)
						player.GetComponent<Rigidbody2D>().gravityScale = 0f;
		
				Transform[] bodyDecorations = new Transform[5];
				Transform[] thighDecorations = new Transform[5];
				Transform[] shoulderDecorations = new Transform[5];
		
		
				// foreach child change the sprite color
				foreach (Transform child in player.transform) {
						//child is your child transform
						switch (child.name) {
				
						// HEAD
						case NationalSuit.NM_HAIR:
								child.GetComponent<SpriteRenderer> ().color = NationalSuits.HAIR_COLOR;
								break;
						case NationalSuit.NM_MOUTH:
								child.GetComponent<SpriteRenderer> ().color = NationalSuits.MOUTH_COLOR;
								break;
						case NationalSuit.NM_EAR:
						case NationalSuit.NM_EYEBROWS:
						case NationalSuit.NM_NOSE:
								child.GetComponent<SpriteRenderer> ().color = NationalSuits.getSkinColor (suit.Nation, false);
								break;
						case NationalSuit.NM_HEAD:
						case NationalSuit.NM_NECK:
						case NationalSuit.NM_KNEE_UP_1:
						case NationalSuit.NM_KNEE_UP_2:
						case NationalSuit.NM_KNEE_MIDDLE_1:
						case NationalSuit.NM_KNEE_MIDDLE_2:
						case NationalSuit.NM_KNEE_DOWN_1:
						case NationalSuit.NM_KNEE_DOWN_2:
								child.GetComponent<SpriteRenderer> ().color = NationalSuits.getSkinColor (suit.Nation, true);
								break;
						case NationalSuit.NM_COLLAR:
								child.GetComponent<SpriteRenderer> ().color = suit.NationalColors.Collar;
								break;
				
						case NationalSuit.NM_THIGH_UP:
								child.GetComponent<SpriteRenderer> ().color = suit.NationalColors.Pants;
								break;
						case NationalSuit.NM_THIGH_DOWN_1:
								child.GetComponent<SpriteRenderer> ().color = suit.NationalColors.Pants;
								foreach (Transform child2 in child) {
										switch (child2.name) {
										case NationalSuit.NM_THIGH_DC_1_1:
												thighDecorations [0] = child2;
												break;
										}
								}
								break;
						case NationalSuit.NM_THIGH_DOWN_2:
								child.GetComponent<SpriteRenderer> ().color = suit.NationalColors.Pants;
								foreach (Transform child2 in child) {
										switch (child2.name) {
										case NationalSuit.NM_THIGH_DC_1_2:
												thighDecorations [1] = child2;
												break;
										}
								}
								break;
				
						// OTHER
						case NationalSuit.NM_BODY:
								child.GetComponent<SpriteRenderer> ().color = suit.NationalColors.Jersey;
								foreach (Transform child2 in child) {
										switch (child2.name) {
										case NationalSuit.NM_BODY_DC_1:
												bodyDecorations [0] = child2;
												break;
										case NationalSuit.NM_BODY_DC_2:
												bodyDecorations [1] = child2;
												break;
										case NationalSuit.NM_BODY_DC_3:
												bodyDecorations [2] = child2;
												break;
										case NationalSuit.NM_BODY_DC_4:
												bodyDecorations [3] = child2;
												break;
										case NationalSuit.NM_BODY_DC_5:
												bodyDecorations [4] = child2;
												break;
										}
								}
								break;
						case NationalSuit.NM_SHOULDER:
								child.GetComponent<SpriteRenderer> ().color = suit.NationalColors.Jersey;
								foreach (Transform child2 in child) {
										switch (child2.name) {
										case NationalSuit.NM_SHOULDER_DC_1:
												shoulderDecorations [0] = child2;
												break;
										case NationalSuit.NM_SHOULDER_DC_2:
												shoulderDecorations [1] = child2;
												break;
										case NationalSuit.NM_SHOULDER_DC_3:
												shoulderDecorations [2] = child2;
												break;
										case NationalSuit.NM_SHOULDER_DC_4:
												shoulderDecorations [3] = child2;
												break;
										case NationalSuit.NM_SHOULDER_DC_5:
												shoulderDecorations [4] = child2;
												break;
										case NationalSuit.NM_ELBOW_UP:
										case NationalSuit.NM_ELBOW_MIDDLE:
										case NationalSuit.NM_ELBOW_DOWN:
												child2.GetComponent<SpriteRenderer> ().color = NationalSuits.getSkinColor (suit.Nation, true);
												break;
										}
								}
								break;
			
			
						}
			
						int thighDecoration, bodyDecoration, shoulderDecoration;
						byte i;
			
						bodyDecoration = NationalSuits.getDecorations (suit.Nation) [0];
						shoulderDecoration = NationalSuits.getDecorations (suit.Nation) [1];
						thighDecoration = NationalSuits.getDecorations (suit.Nation) [2];
			
			
			
						// disable not selected decorations
						for (i = 0; i < thighDecorations.Length; i++) {
								if (thighDecorations [i] == null)
										continue;
				
								if (thighDecoration == 0) {
										thighDecorations [i].gameObject.SetActive (false);
								} else {
										thighDecorations [i].gameObject.GetComponent<SpriteRenderer> ().color = suit.NationalColors.ThighDc;
								}
						}
						for (i = 0; i < bodyDecorations.Length; i++) {
								if (bodyDecorations [i] == null)
										continue;
				
								if (i != bodyDecoration - 1) {
										bodyDecorations [i].gameObject.SetActive (false);
								} else if (bodyDecoration == 5) {
										// the fifth is the german decoration, we don't change color
								} else {
										bodyDecorations [i].gameObject.GetComponent<SpriteRenderer> ().color = suit.NationalColors.BodyDc;
								}
						}
						for (i = 0; i < shoulderDecorations.Length; i++) {
								if (shoulderDecorations [i] == null)
										continue;
				
								if (i != shoulderDecoration - 1) {
										shoulderDecorations [i].gameObject.SetActive (false);
								} else {
										shoulderDecorations [i].gameObject.GetComponent<SpriteRenderer> ().color = suit.NationalColors.ShoulderDc;
								}
						}
				}
		}

}
