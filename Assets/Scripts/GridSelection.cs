using UnityEngine;
using System.Collections;

public class GridSelection : MonoBehaviour
{

		public byte nationId;

		void OnClick ()
		{
				//UIGrid panel = NGUITools.FindInParents<UIGrid> (gameObject);
				//byte index = (byte)panel.GetIndex (this.transform);

				//byte nationId = (byte)(index + groupId * 4);
				if (Game.Instance.EditingNation01)
						Game.Instance.Nation01 = NationalSuits.getNationById (nationId);
				else if (Game.Instance.EditingNation02)
						Game.Instance.Nation02 = NationalSuits.getNationById (nationId);

				Game.Instance.EditingNation01 = false;
				Game.Instance.EditingNation02 = false;
		}
}
