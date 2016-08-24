using UnityEngine;
using System.Collections;

public class DifficultPopup : MonoBehaviour
{

		public UIPopupList uiPopupList;
	
		public void SetDifficult ()
		{
				if (uiPopupList.isOpen) {
						if (uiPopupList.value == uiPopupList.items [0]) {
								DifficultyManager.Instance.Difficult = DifficultyManager.Difficulties.EASY;
						} else if (uiPopupList.value == uiPopupList.items [1]) {
								DifficultyManager.Instance.Difficult = DifficultyManager.Difficulties.NORMAL;
						} else if (uiPopupList.value == uiPopupList.items [2]) {
								DifficultyManager.Instance.Difficult = DifficultyManager.Difficulties.INSANE;
						}
				}
		}
}
