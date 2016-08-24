using UnityEngine;
using System.Collections;

public class SetDifficulty : MonoBehaviour
{

		public DifficultyManager.Difficulties difficulty;


		public void Set ()
		{
				DifficultyManager.Instance.Difficult = difficulty;
		}
}
