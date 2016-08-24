using UnityEngine;
using System.Collections;

public class PlatformBehaviour : MonoBehaviour
{

		public RuntimePlatform platform;

		void Start ()
		{
				#if UNITY_ANDROID
				gameObject.SetActive (platform == RuntimePlatform.Android);
				#elif UNITY_WINRT
		gameObject.SetActive(platform == RuntimePlatform.WindowsPlayer ||
		                     platform == RuntimePlatform.MetroPlayerARM ||
		                     platform == RuntimePlatform.MetroPlayerX64 ||
		                     platform == RuntimePlatform.MetroPlayerX86 ||
		                     platform == RuntimePlatform.WP8Player);
				#endif
		}
}
