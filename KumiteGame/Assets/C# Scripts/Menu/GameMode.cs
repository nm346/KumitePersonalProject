using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour {

	float enemyHealth = EnemyHealth.currentHealth;
	float playerHealth = PlayerHealth.currentHealth;
	private Opponent opponent;
	public Player player;
	public Text RoundText;
	public Text RoundTextShadow;
	public Text RoundOutOf;
	public Text currentlevel;
	public Text Record;
	public Text currentlevel2;
	public Text ArcadeScore;
	public Text ArcadeScore2;
	public Round round;
	public Text ShieldBreakerText;
	public Text PointsLeftToUnlockShieldBreaker;
	public static bool Added;
	public static bool Removed;
	public Text added;
	public Text removed;
	public float remaining = Timer.remaining;
	public float endtime;
	public bool PlayerWon = false;
	public bool EnemyWon = false; 
	public static bool Draw = false;
	public static bool PlayerWonGame = false; 
	public static bool EnemyWonGame = false; 
	bool waitForAnimation;
	public bool GameReset = false;
	private string winner;
	bool Win = true;
	public bool resetKO = false;
	public static bool GameOver;
	bool foundWinner = false;
	private bool isRunning = false;
	public static bool OverByKO;
	public static bool TimeUpEnd;
	public static int numOfShieldBreakers;
	public Slider slider;
	public GameObject loadingScreen;
	public GameObject ShieldBreaker; 
	public GameObject add25;
	public AudioSource GlassSound;
	private bool add25Used;

	System.Random rnd = new System.Random();

	// Use this for initialization
	void Start () {
		try{
		Round.Reset();
		GameOver = false;
		waitForAnimation = true;
		round = new Round();
		GlassSound = GlassSound.GetComponent<AudioSource>();
		if (CharacterSelect.fight || MenuScript.ArcadeMode) {
			GameObject screen = GameObject.Find ("TrainingScreen");
			screen.SetActive (false);
		}
		Draw = false;
		opponent = SelectFrom.CPU.GetComponent<Opponent>();
		player = SelectFrom.Player1.GetComponent<Player>();
		ShieldBreaker.SetActive(false);
		CheckWinner ();
		TimeUpEnd = false;
		if(Arcade.add25Used){
				add25.SetActive(false);		
			}
		if (Arcade.Level % 3 == 0) {
				Arcade.add25Used = false;
				add25.SetActive (true);
				GlassSound.Play();
			}
		
		}
		catch(NullReferenceException ex){
			StartCoroutine (MainMenu());
		}

	}

//	 Update is called once per frame
	void Update () {
		if (MenuScript.ArcadeMode) {
			Record.text = "High Score: "+MenuScript.ArcadeRecord+"xp";
			currentlevel.text = "Arcade Level:" + Arcade.Level;
			currentlevel2.text = "Arcade Level:" + Arcade.Level;
			ArcadeScore.text = "Current Score: "+Arcade.ArcadeScore+"xp";
			ArcadeScore2.text = "Current Score: "+Arcade.ArcadeScore+"xp";
			if(Arcade.ArcadeScore > MenuScript.ArcadeRecord){
				MenuScript.ArcadeRecord = Arcade.ArcadeScore;
			}
			ScoreStatus ();
		}
		CheckShieldBreaker ();
		if (numOfShieldBreakers > 0) {
			ShieldBreaker.SetActive (true);
			ShieldBreakerText.text = "SHIELD BREAKERS READY:"+numOfShieldBreakers;
		} 
		else {
			ShieldBreaker.SetActive (false);
			ShieldBreakerText.text = "";
		}
		Check25Health ();	
		PointsLeftToUnlockShieldBreaker.text = "POINTS LEFT TILL SHIELD BREAKER:"+(700-Arcade.ShieldBreakerCheck);
		RoundOutOf.text = "OUT OF " + Round.MaxRound;
		if (!GameOver) {
			CheckWinner ();
		} 
			EndGame ();

	}

	void CheckWinner2(){ //FIND THE WINNER
		RoundText.text = "Round " + Round.CurrentRound;	
		RoundTextShadow.text = "Round " + Round.CurrentRound;	
		if (PlayerHealth.currentHealth <= 0) {
			foundWinner = true;
			EnemyWon = true;
			PlayerWon = false;
			OverByKO = true;
			opponent.animator.SetTrigger("won");


		} else if (EnemyHealth.currentHealth <= 0) {
			foundWinner = true;
			EnemyWon = false;
			PlayerWon = true;
			OverByKO = true;
			Player.canCelebrate = true;
		}
		if (Timer.time == "0") {
			TimeUpEnd = true;
			if (PlayerHealth.currentHealth > EnemyHealth.currentHealth) {
				foundWinner = true;
				EnemyWon = false;
				PlayerWon = true;
				Draw = false;
				Player.canCelebrate = true;

			} else if (PlayerHealth.currentHealth < EnemyHealth.currentHealth) {
				foundWinner = true;
				EnemyWon = true;
				Draw = false;
				PlayerWon = false;
				opponent.animator.SetTrigger ("won");
			} else {
				foundWinner = true;
				PlayerWon = false;
				EnemyWon = false;
				Draw = true;
			}
		}

	}
	void ScoreStatus(){
		if (Added) {
			added.text = "+10xp";
			removed.text = "";
		}
		else if(Removed){
			added.text = "";
			removed.text = "-5xp";
		}
		Added = false;
		Removed = false;
	}

	public void UseShieldBreaker(){
		if (numOfShieldBreakers >= 1) {
			EnemyHealth.currentShield = 5f;
			numOfShieldBreakers--;
			EnemyHealth.enemyhit = true;
			GlassSound.Play ();
		}

	}

	void CheckShieldBreaker(){
		if(Arcade.ShieldBreakerCheck >= 700){
			GameMode.numOfShieldBreakers++;
			Arcade.ShieldBreakerCheck = 0;
		}
	}

	void CheckWinner(){
			if(!foundWinner){

				CheckWinner2();
				if (foundWinner == true && waitForAnimation) {
					if (PlayerWon && !EnemyWon) {
						Round.PlayerWinRounds++;
					} else if (EnemyWon && !PlayerWon) {
						Round.EnemyWinRounds++;
					}
					waitForAnimation = false;
				if (!TimeUpEnd) {
					Round.KOTrigger = true;
				}
					StartCoroutine(waitForRound());
				}
			}
	}

	IEnumerator waitForRound(){
		yield return new WaitForSeconds(1.5F);
		PlayerWon = false;
		EnemyWon = false;
		foundWinner = false;

		waitForAnimation = true;
		SelectFrom.GameReset2 ();
		checkRounds ();
		Round.canPlayReadySound = true;
	}
		

	void Check25Health(){
		if (Arcade.Level % 3 == 0 && !Arcade.add25Used) {
			add25.SetActive (true);
		}
		if (Arcade.add25Used) {
			add25.SetActive (false);
		}

	}

	public void add25Extrahealth(){
		
		if (PlayerHealth.currentHealth >= 100) {
			PlayerHealth.hit = true;
			PlayerHealth.currentShield += 55;
		} else {
			PlayerHealth.currentHealth += 55;
		}
		Arcade.add25Used = true;
		GlassSound.Play ();
		add25.SetActive (false);
	}

	void checkRounds(){
		if (Round.PlayerWinRounds != 0) {
			for (int i = 0; i < Round.PlayerWinRounds; i++) {
				Round.pRounds [i].enabled = true;

			}

		}	
		if (Round.EnemyWinRounds != 0) {
			for (int i = 0; i < Round.EnemyWinRounds; i++) {
				Round.oRounds [i].enabled = true;
			}
		}

	}



	void EndGame(){

		if (Round.PlayerWinRounds > Round.MaxRound / 2) {
			winner = SelectFrom.p;
			PlayerWonGame = true;
			EnemyWonGame = false;
			GameOver = true;
		}
		else if(Round.EnemyWinRounds > Round.MaxRound / 2){
			winner = SelectFrom.o.Substring(2);
			PlayerWonGame = false;
			EnemyWonGame = true;
			GameOver = true;
		}

		else if (Round.CurrentRound > Round.MaxRound) {
			RoundText.text = "";
			RoundTextShadow.text = "";
			if (Round.PlayerWinRounds > Round.EnemyWinRounds) {
				winner = SelectFrom.p;
				PlayerWonGame = true;
				EnemyWonGame = false;
			}
			else if (Round.PlayerWinRounds < Round.EnemyWinRounds) {
				winner = SelectFrom.o;
				PlayerWonGame = false;
				EnemyWonGame = true;
			}
			GameOver = true;
						RoundText.text = "GAME OVER";
					RoundTextShadow.text = "GAME OVER";
		}
	}

	void launchTraining(){
		if(isRunning) Debug.Log("Coroutine can't be launched, already running.");
		else {
			isRunning = true;
			StartCoroutine (TrainingStage ());
		}
	}

	IEnumerator TrainingStage()
	{

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Fight");
			asyncLoad.allowSceneActivation = false;

		isRunning = false;
		yield return null;

	}

	public void Continue(){
		if(Arcade.opponentsList.Count != 0 && Arcade.opponentsList.Contains("O_"+CharacterSelect.selected)){
			Arcade.opponentsList.Remove ("O_"+CharacterSelect.selected);
		}
		Arcade.num = rnd.Next (0,Arcade.opponentsList.Count);
		Arcade.num2 = rnd.Next (0,Arcade.MapsList.Count);
		CharacterSelect.opponent = Arcade.opponentsList [Arcade.num];
		Arcade.MapChoice = Arcade.MapsList [Arcade.num2];
		MapDifficulty.MapChoice = Arcade.MapChoice;
		IncreaseLevels ();
		Arcade.wins++;

		StartCoroutine (playGame ());

	}

	public void IncreaseLevels(){
		Arcade.Level++;
		if (Arcade.Level == 2) {
			PlayerHealth.maxHealth = 100;
			PlayerHealth.maxShield = 60;
			EnemyHealth.maxHealth = 100;
			EnemyHealth.maxShield = 100;
		}
		else if(Arcade.Level>2){
			PlayerHealth.maxHealth = 100;
			PlayerHealth.ArcadeHit += 0.5f;
			if(PlayerHealth.maxShield!=0){
				PlayerHealth.maxShield -=20;
			}
		}
	}

	IEnumerator playGame()
	{
		loadingScreen.SetActive (true);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Reset");
		asyncLoad.allowSceneActivation = false;
		while (asyncLoad.isDone == false) {
			slider.value = asyncLoad.progress;
			if (asyncLoad.progress == 0.9f) {
				slider.value = 1f;
				asyncLoad.allowSceneActivation = true;
			}
			yield return null;
		}
	}

	IEnumerator MainMenu()
	{
		loadingScreen.SetActive (true);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");
		asyncLoad.allowSceneActivation = false;
		while (asyncLoad.isDone == false) {
			slider.value = asyncLoad.progress;
			if (asyncLoad.progress == 0.9f) {
				slider.value = 1f;
				asyncLoad.allowSceneActivation = true;
			}
			yield return null;
		}

	}
}
