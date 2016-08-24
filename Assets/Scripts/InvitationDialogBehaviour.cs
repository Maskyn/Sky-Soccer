using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class InvitationDialogBehaviour : MonoBehaviour
{		
		public GameObject dialog;
	
		public UILabel InvitationLabel;

		private string inviterName;

		private Invitation inv;

		void Start ()
		{
				dialog.SetActive (false);
		}
	
		void Update ()
		{
				#if UNITY_ANDROID
				if (PlayGamesPlatform.Instance.IsAuthenticated () && InvitationManager.Instance.SetupDone == false)
						InvitationManager.Instance.Setup ();
				// if an invitation arrived, switch to the "invitation incoming" GUI
				// or directly to the game, if the invitation came from the notification
				inv = InvitationManager.Instance.Invitation;
				if (inv != null) {
						dialog.SetActive (true);
						inviterName = (inv.Inviter == null || inv.Inviter.DisplayName == null) ? Localization.Get ("Someone") :
				inv.Inviter.DisplayName;
						InvitationLabel.text = string.Format (Localization.Get ("InvitationText"), inviterName);
				}
				#endif
		}

		public void AcceptInvitation ()
		{
				Game.Instance.OnlinePlay = true;
				Game.Instance.BluetoothPlay = false;
				Game.Instance.LocalMultiPlayer = false;
				dialog.SetActive (false);
				#if UNITY_ANDROID
				InvitationManager.Instance.Clear ();
				OnlineManager.AcceptInvitation (inv.InvitationId);
				#elif UNITY_IPHONE
				#endif
		
		}
	
		public void DeclineInvitation ()
		{
				dialog.SetActive (false);
				#if UNITY_ANDROID
				InvitationManager.Instance.DeclineInvitation ();
				#elif UNITY_IPHONE
				#endif
		}
}
