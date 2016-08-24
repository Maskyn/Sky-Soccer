using UnityEngine;
using System.Collections;

public class PlayerEnumUtils
{

		public static PlayerEnum GetCurrentPlayer ()
		{
				PlayerEnum currentPlayer = PlayerEnum.LEFT;
				// if we play online
				if (Game.Instance.OnlinePlay == true) {
						// and we are the first participiant
						if (OnlineManager.Instance.GetSelf () == OnlineManager.Instance.GetParticipants () [0]) {
								currentPlayer = PlayerEnum.LEFT;
						} else {
								currentPlayer = PlayerEnum.RIGHT;
						}
				} else if (Game.Instance.BluetoothPlay == true) {
						#if UNITY_ANDROID
						if (BluetoothManager.Instance._desiredMode == BluetoothMultiplayerMode.Server) {
								currentPlayer = PlayerEnum.LEFT;
						} else {
								currentPlayer = PlayerEnum.RIGHT;
						}
#endif
				}
				// if we play and offline match in single player
				else if (Game.Instance.LocalPlay) {
						currentPlayer = PlayerEnum.LEFT;
				}
				return currentPlayer;
		}
}
