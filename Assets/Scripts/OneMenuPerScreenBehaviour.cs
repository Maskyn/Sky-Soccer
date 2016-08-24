using UnityEngine;
using System.Collections;

public class OneMenuPerScreenBehaviour : MonoBehaviour {

	public void ActivateThisMenu() {
		GameObject[] menus = GameObject.FindGameObjectsWithTag(Constants.TAG_MENU);
		foreach(GameObject menu in menus) {
			if(!name.Equals(menu.name)) {
				NGUITools.SetActive(menu, false);
			}
		}
		NGUITools.SetActive(gameObject, true);
	}

	public void ActivateAndToogle() {
		GameObject[] menus = GameObject.FindGameObjectsWithTag(Constants.TAG_MENU);
		foreach(GameObject menu in menus) {
			if(!name.Equals(menu.name)) {
				NGUITools.SetActive(menu, false);
			}
		}
		NGUITools.SetActive(gameObject, !gameObject.activeSelf);
	}
}
