using UnityEngine;
using System.Collections;

public class SoundBehaviour : MonoBehaviour
{

		public GameObject SoundOffIcon;

		// Use this for initialization
		void Start ()
		{
				UpdateSound (false);
		}

		public void SoundClicked ()
		{
				UpdateSound (true);
		}

		private void UpdateSound (bool change)
		{
				if (change)
						SavedVariables.SoundEnabled = !SavedVariables.SoundEnabled;
		
				if (SavedVariables.SoundEnabled) {
						AudioListener.volume = 1.0f;
						SoundOffIcon.SetActive (false);
						
						if (AudioListener.pause)
								AudioListener.pause = false;
						if (!Camera.main.GetComponent<AudioSource>().isPlaying)
								Camera.main.GetComponent<AudioSource>().Play ();
				} else {
						AudioListener.volume = 0.0f;
						SoundOffIcon.SetActive (true);
						if (!AudioListener.pause)
								AudioListener.pause = true;
						if (Camera.main.GetComponent<AudioSource>().isPlaying)
								Camera.main.GetComponent<AudioSource>().Pause ();
				}
		}
}
