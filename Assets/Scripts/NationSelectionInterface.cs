using UnityEngine;
using System.Collections;

public class NationSelectionInterface : MonoBehaviour
{

		public UILabel title;
		public UI2DSprite flag;
		public int id;
		private Nationals lastNation01, lastNation02;

		public void Set ()
		{
				switch (id) {
				case 0:
						title.text = NationalSuits.getNationNameLong (Game.Instance.Nation01);
						flag.sprite2D = NationalSuits.getNationFlag (Game.Instance.Nation01);
						break;
				case 1:
						title.text = NationalSuits.getNationNameLong (Game.Instance.Nation02);
						flag.sprite2D = NationalSuits.getNationFlag (Game.Instance.Nation02);
						break;
				}
		}

		void Start ()
		{
				lastNation01 = Game.Instance.Nation01;
				lastNation02 = Game.Instance.Nation02;
				if (id == -1) {
						if (PlayerEnumUtils.GetCurrentPlayer () == PlayerEnum.LEFT)
								id = 0;
						else
								id = 1;
				}
				//Set ();
		}

		void Update ()
		{
				if (lastNation01 != Game.Instance.Nation01 && id == 0) 
						Set ();
				else if (lastNation02 != Game.Instance.Nation02 && id == 1) 
						Set ();
		}
}
