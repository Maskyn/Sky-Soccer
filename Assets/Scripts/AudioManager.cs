using UnityEngine;
using System.Collections;

public class AudioManager {

	public static void PlaySfx(AudioClip clip) {
		if (clip != null) {
			AudioSource.PlayClipAtPoint(clip, Vector3.zero);
		}
	}
}
