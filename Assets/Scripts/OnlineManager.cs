/*
 * Copyright (C) 2014 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using System;

public class OnlineManager : RealTimeMultiplayerListener
{
		//const string RaceTrackName = "RaceTrack";
		//string[] CarNames = new string[] { "Car-Orange", "Car-Lime", "Car-Red", "Car-Cyan" };
		const byte QuickGameOpponents = 1;
		const byte GameVariant = 0;
		static OnlineManager sInstance = null;
		const byte MinOpponents = 1;
		const byte MaxOpponents = 1;		

		private OnlineManager ()
		{
		}

		public static void CreateQuickGame ()
		{
				sInstance = new OnlineManager ();
				PlayGamesPlatform.Instance.RealTime.CreateQuickGame (QuickGameOpponents, QuickGameOpponents,
                GameVariant, sInstance);
		}

		public static void CreateWithInvitationScreen ()
		{
				sInstance = new OnlineManager ();
				PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen (MinOpponents, MaxOpponents,
                GameVariant, sInstance);
		}

		public static void AcceptFromInbox ()
		{
				sInstance = new OnlineManager ();
				PlayGamesPlatform.Instance.RealTime.AcceptFromInbox (sInstance);
		}

		public static void AcceptInvitation (string invitationId)
		{
				sInstance = new OnlineManager ();
				PlayGamesPlatform.Instance.RealTime.AcceptInvitation (invitationId, sInstance);
		}

		public static OnlineManager Instance {
				get {
						if (sInstance == null)
								sInstance = new OnlineManager ();
						return sInstance;
				}
				set {
						sInstance = value;
				}
		}

		public void OnRoomConnected (bool success)
		{
				if (success) {
						UniversalOnline.Instance.State = UniversalOnline.States.ReadyToPlay;
				} else {
						UniversalOnline.Instance.State = UniversalOnline.States.FailedConnect;
				}
		}

		public void OnLeftRoom ()
		{
				if (UniversalOnline.Instance.State != UniversalOnline.States.GameEnded) {
						UniversalOnline.Instance.State = UniversalOnline.States.Aborted;
				}
				Application.LoadLevel (Constants.LEVEL_MENU);
		}

		public void OnPeersConnected (string[] peers)
		{
		}

		public void OnPeersDisconnected (string[] peers)
		{
				UniversalOnline.Instance.State = UniversalOnline.States.Aborted;

				Application.LoadLevel (Constants.LEVEL_MENU);
		}

		public void OnRoomSetupProgress (float percent)
		{
				UniversalOnline.Instance.Progress = percent;
		}

		public void OnRealTimeMessageReceived (bool isReliable, string senderId, byte[] data)
		{
				UniversalOnline.Instance.OnMessageReceived (data);
		}

		

		public Participant GetSelf ()
		{
				return PlayGamesPlatform.Instance.RealTime.GetSelf ();
		}

		public List<Participant> GetParticipants ()
		{
				return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
		}

		public Participant GetParticipant (string participantId)
		{
				return PlayGamesPlatform.Instance.RealTime.GetParticipant (participantId);
		}
}
