using UnityEngine;
using System.Collections;

public class TimeUtils {

	public static string GetStringedTime(float doubleTime) {
		string stringTime = "";
		// minutes
		stringTime += (byte) doubleTime / Constants.SECOND_IN_MINUTE; 
		// separator
		stringTime += ":";
		// 1, 2, 3 becomes 01, 02, 03
		if (((byte) Game.Instance.TimeRemained % Constants.SECOND_IN_MINUTE) < 10) 
			stringTime += "0"; 
		// real seconds
		stringTime += (byte) Game.Instance.TimeRemained % Constants.SECOND_IN_MINUTE;
		// 0 becomes 00
		if(stringTime.EndsWith(":0")){
			stringTime += "0";
		}
		return stringTime;
	} 

}
