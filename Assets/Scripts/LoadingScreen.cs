using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	public UISlider progress;
	private int loadProgress;

	void Start () {
		StartCoroutine(DisplayLoadingScreen(Constants.LEVEL_GAMEPLAY));
	}

	void Update() {
		loadProgress = (int) Time.timeSinceLevelLoad;

	}

	IEnumerator DisplayLoadingScreen(string levelName) {

			AsyncOperation async = Application.LoadLevelAsync(levelName);
			async.allowSceneActivation = false;
			//while(!async.isDone) {
			while(loadProgress < 100){
				//progress.value = async.progress;
				progress.value = (float) (loadProgress / 100f);
			Debug.Log(progress.value);
				yield return null;
			}

	}
}
