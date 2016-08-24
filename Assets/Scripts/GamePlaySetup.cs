using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using System;

public class GamePlaySetup : MonoBehaviour, IBall
{

	#region VARIABLES
		public AudioClip goalReaction;
		public AudioClip whistleReferee;
		//Reference the colliders we are going to adjust
		public BoxCollider2D bottomWall, leftWall, rightWall, topWall;
		public BoxCollider2D bottomDeath, leftDeath, rightDeath, topDeath;

		public GameObject playerBase;

		// goal letters
		public GameObject[] letters;

		// distance between letters
		private float[] lettersDistances = new float[5]; // five letters
		private const float lettersDelay = 0.15f;
		private bool 		canShoot;
		public GameObject 	ball, referee, PauseButton, EndMatchButton, GameEndedExitButton, GameEndedNextButton;
		public UIButton RedoButton;

		public GameObject 	doorLeft, doorRight;

		private Vector2 	touchPos1, touchPos2;
		public UILabel Score, TimeText, PlayerName01
					, PlayerName02
					,   Points01
					,   Points02;
			public UI2DSprite LifeBarContent01, LifeBarContent02, Flag01, Flag02; // TimerContent
	public GameObject MenuPause, MenuGameEnded, GameEndedRowOffline, GameEndedRowOnline, GameEndedTournament;
			private PointsManager points01, points02;
			private bool offlineMatch;
			private PlayerEnum currentPlayer;
			private PlayerEnum whoFinishedPlayers = PlayerEnum.UNSPECIFIED;

			private float x, y, ipotenusa, cos, sin, xDoor, yDoor, xBall, yBall, xShoot, yShoot;
			private float ballVelX, ballVelY, posBallX, posBallY;
			private float posX, posY;
			private PlayerEnum whoScores;
			private bool touchingUp = false, clickingUp = false, touchingDown = false, clickingDown = false;
			private byte[] data = new byte[50], dataSmall = new byte[20], bytesX, bytesY, bytesForceX, bytesForceY, bytesCos, bytesSin;
			private byte[] bytesBallX, bytesBallY, bytesBallVelX, bytesBallVelY;
			private byte i;
			public GameObject tutorial;

			private float addictionalTime = 20f;
			private bool isInGoldenGoal;
			#endregion

	#region UNITY EVENTS
	void Awake() {
		Points01.gameObject.GetComponent<TweenPosition>().from.x = Points01.gameObject.transform.localPosition.x;
		Points01.gameObject.GetComponent<TweenPosition>().to.x = Points01.gameObject.transform.localPosition.x;
		Points02.gameObject.GetComponent<TweenPosition>().from.x = Points02.gameObject.transform.localPosition.x;
		Points02.gameObject.GetComponent<TweenPosition>().to.x = Points02.gameObject.transform.localPosition.x;
	}

	void Start() {
		isInGoldenGoal = false;
		// stop timer, but play the game
		Game.Instance.TimerActive = false;
		CancelInvoke("PrepareEnemie");
		Game.Instance.SetGameActive(true);
		// distance from the screen
		float dFromScreen 	= 4f;
		points01 = new PointsManager();
		points02 = new PointsManager();
		canShoot = false;
		float doorPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;	
		currentPlayer = PlayerEnumUtils.GetCurrentPlayer();
		offlineMatch = Game.Instance.OnlinePlay == false && Game.Instance.BluetoothPlay == false && Game.Instance.LocalMultiPlayer == false;

		if(SavedVariables.FirstPlay){
			SavedVariables.FirstPlay = false;
			tutorial.SetActive(true);
		}

		// disable some components
		MenuPause.SetActive(false);
		MenuGameEnded.SetActive(false);
		if(Game.Instance.TournamentMode) {
			GameEndedRowOffline.SetActive(false);
			GameEndedRowOnline.SetActive(false);
			GameEndedTournament.SetActive(true);
			RedoButton.enabled = false;
			RedoButton.SetState(UIButton.State.Disabled, true);
		} else if(Game.Instance.OnlinePlay || Game.Instance.BluetoothPlay) {
			GameEndedRowOffline.SetActive(false);
			GameEndedRowOnline.SetActive(true);
			GameEndedTournament.SetActive(false);
		} else if(Game.Instance.LocalPlay || Game.Instance.LocalMultiPlayer) {
			GameEndedRowOffline.SetActive(true);
			GameEndedRowOnline.SetActive(false);
			GameEndedTournament.SetActive(false);
		}
		Points01.enabled = false;
		Points02.enabled = false;

		if(Game.Instance.BluetoothPlay || Game.Instance.OnlinePlay)
			PauseButton.SetActive(false);
		else
			EndMatchButton.SetActive(false);

		// get the letters distance
		for(byte i = 0; i < letters.Length; i++) { // count the distance from the letter on the left
			if(i == 0) {
				lettersDistances[i] = 0;
			} else {
				lettersDistances[i] = letters[i].transform.position.x - letters[i-1].transform.position.x;
			}
		}

		// SET WALLS POSITION
		{
			//Move each wall to its edge location:
			topDeath.size = new Vector2 (Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x  * 2 * 2, 1);
			topDeath.offset = new Vector2 (0, Camera.main.ScreenToWorldPoint (new Vector3 ( 0, Screen.height, 0)).y + 0.5f + dFromScreen);
			
			bottomDeath.size = new Vector2 (Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x * 2  * 2, 1);
			bottomDeath.offset = new Vector2 (0, Camera.main.ScreenToWorldPoint (new Vector3( 0, 0, 0)).y - 0.5f - dFromScreen);
			
			leftDeath.size = new Vector2(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y  * 2  * 2);;
			leftDeath.offset = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 0.5f  - dFromScreen, 0);
			
			rightDeath.size = new Vector2(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y  * 2  * 2);
			rightDeath.offset = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.5f + dFromScreen, 0);
			
			//Move each wind to its edge location:
			topWall.size = new Vector2 (Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x  * 2, 1);
			topWall.offset = new Vector2 (0, Camera.main.ScreenToWorldPoint (new Vector3 ( 0, Screen.height, 0)).y + 0.5f);
			
			bottomWall.size = new Vector2 (Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x * 2, 1);
			bottomWall.offset = new Vector2 (0, Camera.main.ScreenToWorldPoint (new Vector3( 0, 0, 0)).y - 0.5f);
			
			leftWall.size = new Vector2(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y  * 2);;
			leftWall.offset = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 0.5f, 0);
			
			rightWall.size = new Vector2(1, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y  * 2);
			rightWall.offset = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.5f, 0);
		}
		// SETUP BALL AND DOORS
		{
			// move down the referee
			Whistle(true);
			
			// doors

					doorLeft.transform.position = new Vector3(-doorPos, 0, 0);
				doorRight.transform.position = new Vector3(+doorPos, 0, 0);
		}

		GameObject player01 	= GameObject.Instantiate(playerBase, Vector3.zero, Quaternion.identity) as GameObject;
		GameObject player02 	= GameObject.Instantiate(playerBase, Vector3.zero, Quaternion.identity) as GameObject;
		GameObject goalKeeper 	= GameObject.Instantiate(playerBase, Vector3.zero, Quaternion.identity) as GameObject;

		while(Game.Instance.Nation01 == Nationals.NONE) {
			System.Array A = System.Enum.GetValues(typeof(Nationals));
			Nationals V = (Nationals)A.GetValue(UnityEngine.Random.Range(0,A.Length));
			Game.Instance.Nation01 = V;
		}
		while(Game.Instance.Nation02 == Nationals.NONE || Game.Instance.Nation02 == Game.Instance.Nation01) {
			System.Array A = System.Enum.GetValues(typeof(Nationals));
			Nationals V = (Nationals)A.GetValue(UnityEngine.Random.Range(0,A.Length));
			Game.Instance.Nation02 = V;
		}
		Flag01.sprite2D = NationalSuits.getNationFlag(Game.Instance.Nation01);
		Flag02.sprite2D = NationalSuits.getNationFlag(Game.Instance.Nation02);
		
		PlayerName01.text = NationalSuits.getNationNameShort(Game.Instance.Nation01);
		PlayerName02.text = NationalSuits.getNationNameShort(Game.Instance.Nation02);
		NationalSuit nationalSuit01 = NationalSuits.getSuitForNation(Game.Instance.Nation01);
		NationalSuit nationalSuit02 = NationalSuits.getSuitForNation(Game.Instance.Nation02);
		//LifeBarContent01.color = nationalSuit01.NationalColors.Jersey;
		//LifeBarContent02.color = nationalSuit02.NationalColors.Jersey;
		
		
		// SETUP PLAYERS
		{
			// set suit colors
			PlayerConfigurer.ConfigurePlayer(player01, 	nationalSuit01);
			PlayerConfigurer.ConfigurePlayer(player02, 	nationalSuit02);
			PlayerConfigurer.ConfigurePlayer(goalKeeper, NationalSuits.getGoalKeeperSuit());
			// create many copies
			
			PlayerShooter.Instance.SetupPlayers(player01, player02, goalKeeper);

			Destroy(player01);
			Destroy(player02);
			Destroy(goalKeeper);
		}
	}


	void Update () {

		if (Input.GetKeyUp(KeyCode.Escape)) {
				PauseButton.SendMessage("OnClick");
		}

		if (Game.Instance.TimerActive == true && Game.Instance.IsGameActive()) {

			touchingDown = Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
			clickingDown = Input.GetMouseButtonDown (0);
			
			touchingUp = Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended;
			clickingUp = Input.GetMouseButtonUp (0);
			
			
			if (touchingDown || clickingDown){
				if(tutorial.activeSelf == true && Time.timeSinceLevelLoad > 1)
					tutorial.SetActive(false);
				GetFirstTouch(touchingDown);
			}
			else if ((touchingUp || clickingUp) && canShoot){
				GetSecondTouch(touchingUp);
			}
			
		} else { // timer is pausing
			canShoot = false;
		}
	}
	#endregion

	#region SHOOT
	private void GetFirstTouch(bool touch){

		if(touch)
			touchPos1 = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
		else 
			touchPos1 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		
		if(
			(touchPos1.x <= Screen.width / 2 
		 	&& currentPlayer == PlayerEnum.LEFT
		 	&& Game.Instance.RemainingPlayers01 > 0) 
		   				|| (touchPos1.x >= Screen.width / 2 
		    			&& currentPlayer == PlayerEnum.RIGHT
		   				&& Game.Instance.RemainingPlayers02 > 0)
			)

			canShoot = true;

		else if(Game.Instance.LocalMultiPlayer 
		        && 
		        	((touchPos1.x <= Screen.width / 2 
		    		&& Game.Instance.RemainingPlayers01 > 0)
				        || (touchPos1.x >= Screen.width / 2 
				    	&& Game.Instance.RemainingPlayers02 > 0))
		        )
			canShoot = true;
		else
			canShoot = false;
	}

	private void GetSecondTouch(bool touch){

		if(canShoot == false)
			return;
		
		if(touch)
			touchPos2 = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
		else 
			touchPos2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		
		//deltaTime = time2 - time1;
		
		x = touchPos2.x - touchPos1.x;
		y = touchPos2.y - touchPos1.y;
		ipotenusa = Mathf.Sqrt((float) (x*x+y*y));
		
		cos = (float) x / ipotenusa;
		sin = (float) y / ipotenusa;

		if(ipotenusa != 0) // si è mosso
		{
			float xShoot, yShoot;
			xShoot = Camera.main.ScreenToWorldPoint(new Vector3(touchPos1.x, 0, 0)).x;
			yShoot = Camera.main.ScreenToWorldPoint(new Vector3(0, touchPos1.y, 0)).y;

			Shoot(xShoot, yShoot, cos, sin);
		}
	}

	private void PrepareEnemie() {

		if(!(Game.Instance.TimerActive == true 
		   && offlineMatch
		   && Game.Instance.RemainingPlayers02 > 0))
			return;
			xDoor = doorLeft.transform.position.x;
		yDoor = doorLeft.transform.position.y;

		
		xBall = ball.transform.position.x;
		yBall = ball.transform.position.y;

		if (xBall < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 4, 0, 0)).x && Game.Instance.RemainingPlayers01 > 0) {
			return;
		}

		x = xDoor - xBall;
		y = yDoor - yBall;
		ipotenusa = Mathf.Sqrt((float) (x*x+y*y));
		
		cos = (float) x / ipotenusa; // from 0 to 1
		sin = (float) y / ipotenusa; // from 0 to 1

		if (xBall >= 0) { // is on the right side
			xShoot = (float) (xBall - DifficultyManager.Instance.DistanceBallEnemie * cos);
			yShoot = (float) (yBall - DifficultyManager.Instance.DistanceBallEnemie * sin);
		} else {
			
			//xShoot = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x;
			xShoot = 0;
			xShoot += (float) (-DifficultyManager.Instance.DistanceBallEnemie * cos);
			//yShoot = mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height  / 2, 0)).y - 3f * senoAngolo;
			
			// lunghezza palla : altezza palla = meta campo : altezza giocatore
			yShoot = (float) ((sin * (Screen.width / 2)) / cos);
			yShoot = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height  / 2 + (float) yShoot, 0)).y;
			yShoot += (float) (-DifficultyManager.Instance.DistanceBallEnemie * sin);
		}
		
		if(ipotenusa != 0) // si è mosso
		{
			Shoot(xShoot, yShoot, cos, sin);
		}
	}

	private void Shoot( float x, float y, float cos, float sin){

		PlayerShooter.Instance.Shoot(x, y, cos, sin);

		if(Game.Instance.OnlinePlay || Game.Instance.BluetoothPlay){

			// Shoot online
			bytesX = BitConverter.GetBytes(x);
			bytesY = BitConverter.GetBytes(y);
			bytesCos = BitConverter.GetBytes(cos);
			bytesSin = BitConverter.GetBytes(sin);
			
			bytesBallX = BitConverter.GetBytes(ball.transform.position.x);
			bytesBallY = BitConverter.GetBytes(ball.transform.position.y);
			bytesBallVelX = BitConverter.GetBytes(ball.GetComponent<Rigidbody2D>().velocity.x);
			bytesBallVelY = BitConverter.GetBytes(ball.GetComponent<Rigidbody2D>().velocity.y);
			
			data[0] = Constants.CODE_BALL_AND_SHOOT;
			for(i=0; i<4; i++){
				data[i + 1 + 4*0] = bytesX[i];
			}
			for(i=0; i<4; i++){
				data[i + 1 + 4*1] = bytesY[i];
			}
			for(i=0; i<4; i++){
				data[i + 1 + 4*2] = bytesCos[i];
			}
			for(i=0; i<4; i++){
				data[i + 1 + 4*3] = bytesSin[i];
			}
			
			for(i=0; i<4; i++){
				data[i + 1 + 4*4] = bytesBallX[i];
			}
			for(i=0; i<4; i++){
				data[i + 1 + 4*5] = bytesBallY[i];
			}
			for(i=0; i<4; i++){
				data[i + 1 + 4*6] = bytesBallVelX[i];
			}
			for(i=0; i<4; i++){
				data[i + 1 + 4*7] = bytesBallVelY[i];
			}


			
			if(Game.Instance.OnlinePlay){
				PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, data);
			} else if(Game.Instance.BluetoothPlay){
				#if UNITY_ANDROID
				SendInfoToServer(data);
				#endif
			}

			if(currentPlayer == PlayerEnum.LEFT) {
				dataSmall[0] = Constants.CODE_REMAINING_PLAYERS;
				dataSmall[1] = Game.Instance.RemainingPlayers01;
				dataSmall[2] = Game.Instance.RemainingPlayers02;
				if(Game.Instance.OnlinePlay){
					PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, dataSmall);
				} else if(Game.Instance.BluetoothPlay){
					#if UNITY_ANDROID
					SendInfoToServer(dataSmall);
					#endif
				}
			}
		}
		UpdateLifeBar();

		if((Game.Instance.RemainingPlayers01 <= 0 || Game.Instance.RemainingPlayers02 <= 0) && whoFinishedPlayers == PlayerEnum.UNSPECIFIED) {
			if(Game.Instance.RemainingPlayers01 == 0)
				whoFinishedPlayers = PlayerEnum.LEFT;
			else
				whoFinishedPlayers = PlayerEnum.RIGHT;
			Invoke("PrepareToGiveGoal", 5f);

			if(whoFinishedPlayers == currentPlayer && PlayGamesPlatform.Instance.IsAuthenticated()){
				Social.ReportProgress(Constants.ACH_LOST_ALL_PLAYER, 100.0f, (bool success) => {
					// handle success or failure
				});
			}
			
		}
	}

	#endregion

	#region GOAL
	void PrepareToGiveGoal() {
		Game.Instance.TimerActive = false;
		// move down the referee
		Whistle(false);
		switch(whoFinishedPlayers) {
		case PlayerEnum.LEFT:
			whoScores = PlayerEnum.RIGHT;
			break;
		case PlayerEnum.RIGHT:
			whoScores = PlayerEnum.LEFT;
			break;
		default:
			whoScores = PlayerEnum.UNSPECIFIED;
			break;
		}
		whoFinishedPlayers = PlayerEnum.UNSPECIFIED;
		// prevent givint bonuses
		points01.ResetWallHits();
		points02.ResetWallHits();
		//
		Goal(whoScores, true, true);

		//Game.Instance.ResetRemainingPlayers();
		//ball.GetComponent<BallController>().ResetBall();
	}

	public void FakeGoal() {
		AudioManager.PlaySfx(goalReaction);
		
		// animating, stop the time
		Game.Instance.TimerActive = false;
		CancelInvoke("PrepareEnemie");
		CancelInvoke("PrepareToGiveGoal");
		// sound
		whoFinishedPlayers = PlayerEnum.UNSPECIFIED;
		Game.Instance.ResetRemainingPlayers();
		UpdateLifeBar();
		
		// To right
		for(byte i = 0; i<letters.Length; i++){
			letters[i].transform.position = new Vector3(+12, 0, 0);
		}
		
		// To centre
		float alreadyDoneDistance = 0;
		for(byte i = 0; i<letters.Length; i++){
			iTween.MoveTo(letters[i], 	iTween.Hash("x", -4 + lettersDistances[i] + alreadyDoneDistance, "y", 0, "delay", i * lettersDelay));
			alreadyDoneDistance += lettersDistances[i];
		}
		
		// To left
		for(byte i = 0; i<letters.Length; i++){
			if(i == 0){
				iTween.MoveTo(letters[i], 	iTween.Hash("x", -12, "y", 0, "delay", (i+10) * lettersDelay, "onComplete", "GoalAnimationEnded", "onCompleteTarget", gameObject));
			} else {
				iTween.MoveTo(letters[i], 		iTween.Hash("x", -12, "y", 0, "delay", (i+10) * lettersDelay));
			}
		}

		if(isInGoldenGoal)
			Game.Instance.TimeRemained = 0;
	}

	public void Goal(bool sendInfoToServer) {
		PlayerEnum player;
		if(ball.transform.position.x < 0)
			player = PlayerEnum.RIGHT;
		else
			player = PlayerEnum.LEFT;
		Goal(player, false, sendInfoToServer);
	}
	
	public void Goal(PlayerEnum who, bool penalty, bool sendInfoToServer) {
		/* POINTS */
		if(who == currentPlayer && ((currentPlayer == PlayerEnum.LEFT && Game.Instance.RemainingPlayers01 == 10) || (currentPlayer == PlayerEnum.RIGHT && Game.Instance.RemainingPlayers02 == 10)) && PlayGamesPlatform.Instance.IsAuthenticated()){
			Social.ReportProgress(Constants.ACH_ONE_SHOOT, 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		else if(who == currentPlayer && ((currentPlayer == PlayerEnum.LEFT && Game.Instance.RemainingPlayers01 == 0) || (currentPlayer == PlayerEnum.RIGHT && Game.Instance.RemainingPlayers02 == 0)) && PlayGamesPlatform.Instance.IsAuthenticated()){
			Social.ReportProgress(Constants.ACH_GOAL_WITH_THE_KEEPER, 100.0f, (bool success) => {
				// handle success or failure
			});
		}
		if(who == PlayerEnum.RIGHT) {
			Game.Instance.PlayerScore02++;
			points02.Goal(penalty, Game.Instance.RemainingPlayers02);
			points01.ResetWallHits();
			
			Points01.enabled = true;
			Points01.text = string.Format("+{0}", points02.PointsForLastGoal);
			Points01.gameObject.GetComponent<TweenPosition>().ResetToBeginning();
			Points01.gameObject.GetComponent<TweenPosition>().PlayForward();
			
			Points01.gameObject.GetComponent<TweenAlpha>().ResetToBeginning();
			Points01.gameObject.GetComponent<TweenAlpha>().PlayForward();
		} 
		else if(who == PlayerEnum.LEFT){
			Game.Instance.PlayerScore01++;
			points01.Goal(penalty, Game.Instance.RemainingPlayers01);
			points02.ResetWallHits();
			
			Points02.enabled = true;
			Points02.text = string.Format("+{0}", points01.PointsForLastGoal);
			Points02.gameObject.GetComponent<TweenPosition>().ResetToBeginning();
			Points02.gameObject.GetComponent<TweenPosition>().PlayForward();
			
			Points02.gameObject.GetComponent<TweenAlpha>().ResetToBeginning();
			Points02.gameObject.GetComponent<TweenAlpha>().PlayForward();
		} 
		
		UpdateScore();
		
		if(sendInfoToServer && (Game.Instance.OnlinePlay || Game.Instance.BluetoothPlay)){
			byte[] data = new byte[50];
			data[0] = Constants.CODE_GOAL;
			data[1] = Game.Instance.PlayerScore01;
			data[2] = Game.Instance.PlayerScore02;
			if(Game.Instance.OnlinePlay){
				PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
			}
			else if(Game.Instance.BluetoothPlay){
				#if UNITY_ANDROID
				SendInfoToServer(data);
				#endif
			}
		}
		
		AudioManager.PlaySfx(goalReaction);
		
		// animating, stop the time
		Game.Instance.TimerActive = false;
		CancelInvoke("PrepareEnemie");
		CancelInvoke("PrepareToGiveGoal");
		// sound
		whoFinishedPlayers = PlayerEnum.UNSPECIFIED;
		Game.Instance.ResetRemainingPlayers();
		UpdateLifeBar();
		
		// To right
		for(byte i = 0; i<letters.Length; i++){
			letters[i].transform.position = new Vector3(+12, 0, 0);
		}
		
		// To centre
		float alreadyDoneDistance = 0;
		for(byte i = 0; i<letters.Length; i++){
			iTween.MoveTo(letters[i], 	iTween.Hash("x", -4 + lettersDistances[i] + alreadyDoneDistance, "y", 0, "delay", i * lettersDelay));
			alreadyDoneDistance += lettersDistances[i];
		}
		
		// To left
		for(byte i = 0; i<letters.Length; i++){
			if(i == 0){
				iTween.MoveTo(letters[i], 	iTween.Hash("x", -12, "y", 0, "delay", (i+10) * lettersDelay, "onComplete", "GoalAnimationEnded", "onCompleteTarget", gameObject));
			} else {
				iTween.MoveTo(letters[i], 		iTween.Hash("x", -12, "y", 0, "delay", (i+10) * lettersDelay));
			}
		}

		if(isInGoldenGoal)
			Game.Instance.TimeRemained = 0;
	}
	
	public void BallTouchedPlayer() {
		points01.ResetWallHits();
		points02.ResetWallHits();
	}
	
	public void BallTouchedWall(bool leftPart) {
		if(leftPart) {
			points02.WallHitted();
		} else {
			points01.WallHitted();
		}
	}
	
	void GoalAnimationEnded () {
		Game.Instance.TimerActive = true;
		ball.GetComponent<BallController>().ResetBall();
		InvokeRepeating("PrepareEnemie", DifficultyManager.Instance.EnemieFastPreparationTime, DifficultyManager.Instance.EnemieLongPreparationTime);
	}
	#endregion

	#region GUI
	void UpdateTimer() {
			
			// decrease the seconds remained
			Game.Instance.TimeRemained -= 1;
			
			// less than zero seconds? let's show that there are zero seconds
			if(Game.Instance.TimeRemained < 0) Game.Instance.TimeRemained = 0;
					
			/* SHOW TIME */
			UpdateGuiTime();

		if(currentPlayer == PlayerEnum.LEFT) {
			dataSmall[0] = Constants.CODE_TIME;
			dataSmall[1] = (byte) ((int)Game.Instance.TimeRemained);
			
			if(Game.Instance.OnlinePlay){
				PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, dataSmall);
			} else if(Game.Instance.BluetoothPlay){
				#if UNITY_ANDROID
				SendInfoToServer(dataSmall);
#endif
			}
		}
			

			//TimerContent.fillAmount = (float) Game.Instance.TimeRemained / 120;

			/* GAME ENDED */
			
			if (Game.Instance.GameEnded == true) {


			if(Game.Instance.TournamentMode && !isInGoldenGoal && Game.Instance.PlayerScore01 == Game.Instance.PlayerScore02) {
					Game.Instance.TimeRemained = addictionalTime;
					isInGoldenGoal = true;
					Whistle(false);

					if(PlayGamesPlatform.Instance.IsAuthenticated()) {
						Social.ReportProgress(Constants.ACH_GOLDEN_GOAL, 100.0f, (bool success) => {
							// handle success or failure
						});
					} 
				} else {
				CancelInvoke("UpdateTimer");
				Game.Instance.SetGameActive(false);
				PauseButton.SetActive(false);
				PointsManager points = (currentPlayer == PlayerEnum.LEFT) ? points01 : points01;
				
				GameEndedExitButton.SetActive(Game.Instance.PlayerScore02 > Game.Instance.PlayerScore01);
				GameEndedNextButton.SetActive(GameEndedExitButton.activeSelf == false);
				
				
				MenuGameEnded.SetActive(true);
				
				foreach(Transform child in MenuGameEnded.transform) {
					switch(child.name) {
					case "Points":
						child.GetComponent<UILabel>().text = string.Format("{0}",points.AllPoints);
						break;
					case "Pallino1":
						foreach(Transform child2 in child) {
							switch(child2.name) {
							case "Star1":
								if(points.Stars < 1)
									child2.gameObject.SetActive(false);
								break;
							}
						}
						
						break;
					case "Pallino2":
						foreach(Transform child2 in child) {
							switch(child2.name) {
							case "Star2":
								if(points.Stars < 2)
									child2.gameObject.SetActive(false);
								break;
							}
						}
						break;
					case "Pallino3":
						foreach(Transform child2 in child) {
							switch(child2.name) {
							case "Star3":
								if(points.Stars < 3)
									child2.gameObject.SetActive(false);
								break;
							}
						}
						break;
					}
				}
				
				SavedVariables.LeaderBoardPoints += points.AllPoints;
				SavedVariables.LeaderBoardGoals += points.Goals;
				SavedVariables.LeaderBoardPlayedMatches += 1;
				//
				bool matchWon = false;
				bool matchEven = false;;
				bool matchLost = false;
				if(currentPlayer == PlayerEnum.LEFT){
					matchWon = Game.Instance.PlayerScore01 > Game.Instance.PlayerScore02;
				} else {
					matchWon = Game.Instance.PlayerScore01 < Game.Instance.PlayerScore02;
				}
				matchLost = !matchWon;
				matchEven = matchWon == false && matchLost == false;
				//
				bool supremeDefense = false;
				if(currentPlayer == PlayerEnum.LEFT){
					supremeDefense = Game.Instance.PlayerScore02 < 5;
				} else {
					supremeDefense = Game.Instance.PlayerScore01 < 5;
				}

				bool bestDefenseEver = false;
				if(currentPlayer == PlayerEnum.LEFT){
					bestDefenseEver = Game.Instance.PlayerScore02 == 0;
				} else {
					bestDefenseEver = Game.Instance.PlayerScore01 == 0;
				}
				//
				bool supremeTactic = false;
				if(currentPlayer == PlayerEnum.LEFT){
					supremeTactic = Game.Instance.PlayerScore01 > 25;
				} else {
					supremeTactic = Game.Instance.PlayerScore01 > 25;
				}
				//
				if(matchWon)
					SavedVariables.LeaderBoardWonMatches += 1;
				
				if(PlayGamesPlatform.Instance.IsAuthenticated() && Game.Instance.LocalMultiPlayer == false){
					
					Social.ReportScore(SavedVariables.LeaderBoardPoints, Constants.LB_POINTS, (bool success) => {
						// handle success or failure
					});
					Social.ReportScore(SavedVariables.LeaderBoardGoals, Constants.LB_GOALS, (bool success) => {
						// handle success or failure
					});
					Social.ReportScore(SavedVariables.LeaderBoardPlayedMatches, Constants.LB_PLAYED_MATCHES, (bool success) => {
						// handle success or failure
					});
					Social.ReportScore(SavedVariables.LeaderBoardWonMatches, Constants.LB_WON_MATCHES, (bool success) => {
						// handle success or failure
					});
					
					if(Game.Instance.OnlinePlay && matchWon) {
						Social.ReportProgress(Constants.ACH_WIN_AN_ONLINE_MATCH, 100.0f, (bool success) => {
							// handle success or failure
						});
					}
					
					if(Game.Instance.BluetoothPlay && matchWon) {
						Social.ReportProgress(Constants.ACH_WIN_A_BLUETOOTH_MATCH, 100.0f, (bool success) => {
							// handle success or failure
						});
					}
					
					if(matchWon) {
						Social.ReportProgress(Constants.ACH_WIN_A_MATCH, 100.0f, (bool success) => {
							// handle success or failure
						});
					}
					else if(matchEven) {
						Social.ReportProgress(Constants.ACH_MAKE_AN_EVEN, 100.0f, (bool success) => {
							// handle success or failure
						});
					}
					else if(matchLost) {
						Social.ReportProgress(Constants.ACH_LOSE_A_MATCH, 100.0f, (bool success) => {
							// handle success or failure
						});
					}
					if(supremeDefense) {
						Social.ReportProgress(Constants.ACH_SUPREME_DEFENSE, 100.0f, (bool success) => {
							// handle success or failure
						});
					}
					if(bestDefenseEver) {
						Social.ReportProgress(Constants.ACH_BEST_DEFENSE_EVER, 100.0f, (bool success) => {
							// handle success or failure
						});
					}
					if(supremeTactic) {
						Social.ReportProgress(Constants.ACH_SUPREME_TACTIC, 100.0f, (bool success) => {
							// handle success or failure
						});
					}

					if(SavedVariables.LeaderBoardPoints >= 100000) {
						Social.ReportProgress(Constants.ACH_POINTS_100_000, 100.0f, (bool success) => {
							// handle success or failure
						});
					} 

					if(SavedVariables.LeaderBoardPoints >= 500000) {
						Social.ReportProgress(Constants.ACH_POINTS_500_000, 100.0f, (bool success) => {
							// handle success or failure
						});
					} 

					if(SavedVariables.LeaderBoardPoints >= 1000000) {
						Social.ReportProgress(Constants.ACH_POINTS_1_000_000, 100.0f, (bool success) => {
							// handle success or failure
						});
					} 

				}
			}



		}
	}

	const float max = 1, min = 0;
	public void UpdateLifeBar() {
		
		LifeBarContent01.fillAmount = (float) (max * (min + (1-min) * ((float) Game.Instance.RemainingPlayers01 / Game.TEAM_PLAYERS)));
		LifeBarContent02.fillAmount = (float) (max * (min + (1-min) * ((float) Game.Instance.RemainingPlayers02 / Game.TEAM_PLAYERS)));
	}

	public void UpdateScore() {
		
		Score.text = string.Format ("{0}-{1}", Game.Instance.PlayerScore01, Game.Instance.PlayerScore02);
	}

	public void UpdateGuiTime() {
		TimeText.text = TimeUtils.GetStringedTime(Game.Instance.TimeRemained);
	}
	#endregion

	#region NGUI
	public void Stop() {
		Game.Instance.SetGameActive(false);
	}

	public void Play() {
		Game.Instance.SetGameActive(true);
	}

	public void Redo() {
		Game.Instance.ResetGameVariables();
		Application.LoadLevelAsync(Application.loadedLevel);
	}

	public void GoToTournament() {
		Application.LoadLevelAsync(Constants.LEVEL_TOURNAMENT);
	}
	#endregion

	#region OTHER
	// Play sound, Play timer and move up the referee
	void Whistle(bool startMatch) {
		iTween.MoveTo(referee, 		iTween.Hash("x", 0, "y", 0, "time", 0.5f));
		iTween.MoveTo(referee, 		iTween.Hash("x", 0, "y", 10, "delay", 0.5));
		AudioManager.PlaySfx(whistleReferee);
		if(startMatch) {
			Game.Instance.TimerActive = true;
			InvokeRepeating("UpdateTimer", 0.5f, 1.0f);
			InvokeRepeating("PrepareEnemie", DifficultyManager.Instance.EnemieLongPreparationTime, DifficultyManager.Instance.EnemieLongPreparationTime);
		}
	}

	public void ReturnToMenu() {
		if(Game.Instance.OnlinePlay || Game.Instance.BluetoothPlay){
			UniversalOnline.Instance.CleanUp();
		}
		if(Game.Instance.TournamentMode)
			TournamentUtil.Instance.Destroy ();
		#if UNITY_PRO_LICENSE
		Application.LoadLevelAsync(Constants.LEVEL_MENU);
		#else
		Application.LoadLevel(Constants.LEVEL_MENU);
		#endif
	}
	#endregion

	#region RPC
	#if UNITY_ANDROID
	[RPC]
	void SendInfoToServer(byte[] data){
		networkView.RPC("ReceiveInfoFromClient", RPCMode.Server, data);
	}

	
	[RPC]
	void ReceiveInfoFromClient(byte[] data) {
		
		UniversalOnline.Instance.OnMessageReceived(data);
	}
	#endif
	#endregion
}
