using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundChooser : MonoBehaviour
{

		public GameObject[] bgs;
		public int SPACE_ID;
		public static bool isSpace;
		/*public Sprite skyDay;
	public Sprite skyNight;
	public Sprite buildingsDay;
	public Sprite buildingsNight;*/

		void Awake ()
		{
				byte lanscapeId = Game.Instance.LandscapeId;
				if (lanscapeId > bgs.Length)
						lanscapeId = (byte)Random.Range (0, bgs.Length);
				bgs [lanscapeId].SetActive (true);

				isSpace = lanscapeId == SPACE_ID;
		}

		public static bool isDay ()
		{
				byte hour = (byte)System.DateTime.Now.Hour;
		
				if (isSummer ()) {
						if (hour >= 19 || hour < 7) {
								return false;
						} else {
								return true;
						}
				} else {
						if (hour >= 17 || hour < 8) {
								return false;
						} else {
								return true;
						}
				}
		}

		public static bool isSummer ()
		{
				byte month = (byte)System.DateTime.Now.Month;
				const byte NOVEMBER = 11 , APRIL= 4;
				if (month >= NOVEMBER || month <= APRIL) {
						return false;
				} else {
						return true;
				}
		}
}
