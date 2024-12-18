using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class Round : MonoBehaviour {

	//Canvas components
	public Canvas ArcadeLoseMenu;
	public Canvas ArcadeWinMenu;
	public Canvas GameOverMenu;

	public static int PlayerWinRounds = 0;
	public static int EnemyWinRounds = 0;	
	public static bool RoundSet = false;
	public static int CurrentRound = 1;
	public static int MaxRound = 3;	
	public static Image[] pRounds;
	public static Image[] oRounds;

	//Audio components
	public AudioSource YouWin;
	public AudioSource YouLose;
	public AudioSource TimeUp;
	public AudioSource Ready;	
	public AudioSource Fight;
	public AudioSource KO;

	public Slider slider;
	private int losses;
	private int draws;
	private int wins;	

	public static bool canPlayReadySound;
	public GameObject loadingScreen;

	//Text components
	public Text KOTextShadow;
	public Text PlayerRecord;	
	public Text ResultText;	
	public Text FightText2;
	public Text FightText;	
	public Text RoundNo;
	public Text KOText;

	//Player round win indicators
	public Image p0;
	public Image p1;
	public Image p2;
	public Image p3;

	//Opponent round win indicators
	public Image o0;
	public Image o1;
	public Image o2;
	public Image o3;

	public static bool KOTrigger;
	public static string difficulty;
	public static string player;
	public static string opponent;
	public static int numOfRounds;
	public static string map;	
	bool alreadyKO;

	// The start function in C# is always the first function to run at runtime
	void Start () {
		//Initialising local variables and attributes
		try{
		ArcadeLoseMenu = ArcadeLoseMenu.GetComponent<Canvas> ();
		ArcadeWinMenu = ArcadeWinMenu.GetComponent<Canvas> ();
		GameOverMenu = GameOverMenu.GetComponent<Canvas> ();
		difficulty = MapDifficulty.Difficulty;
		opponent = CharacterSelect.opponent;
		player = CharacterSelect.selected;
		map = MapDifficulty.MapChoice;
		numOfRounds = MaxRound;
		PlayerRecord.text = "";
		ResultText.text = "";
		Ready = Ready.GetComponent<AudioSource>();
		Fight = Fight.GetComponent<AudioSource>();
		KO = KO.GetComponent<AudioSource>();	
		pRounds = new Image[]{p0,p1,p2,p3};
		oRounds = new Image[]{o0,o1,o2,o3};
		ArcadeLoseMenu.enabled = false;
		ArcadeWinMenu.enabled = false;
		GameOverMenu.enabled = false;	
		KOTrigger = false;
		alreadyKO = false;
	
		//Round indicators
		p0 = p0.GetComponent<Image> ();
		p1 = p1.GetComponent<Image> ();
		p2 = p2.GetComponent<Image> ();
		p3 = p3.GetComponent<Image> ();			
		o0 = o0.GetComponent<Image> ();
		o1 = o1.GetComponent<Image> ();
		o2 = o2.GetComponent<Image> ();
		o3 = o3.GetComponent<Image> ();
		p0.enabled = false;
		p1.enabled = false;
		p2.enabled = false;
		p3.enabled = false;
		o0.enabled = false;
		o1.enabled = false;
		o2.enabled = false;
		o3.enabled = false;


		if (!GameMode.GameOver) {
			PauseAndResume ();
		} else {
			Ready.enabled = false;
			Fight.enabled = false;
		}
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}
		
	public static void Reset(){
		CurrentRound = 1;
		PlayerWinRounds = 0;
		EnemyWinRounds = 0;
		RoundSet = false;
		GameMode.PlayerWonGame = false;
		GameMode.EnemyWonGame = false;
	}

	//If the user restarts the fight
	public void restart(){
		if (MenuScript.ArcadeMode) { 
			//If you restart a match the penalty is 300 points
			Arcade.ArcadeScore -= 300;
			Arcade.ShieldBreakerCheck -= 300;
			Arcade.losses++;
		}
		StartCoroutine (GoToReset());
	}

	// Update is called once per frame
	void Update () {
		newRoundSound ();
		if (KOTrigger) {
			KOText.text = "KO";
			KOTextShadow.text = "KO";
			KO.Play ();
			Round.KOTrigger = false;
		}

		if(GameMode.TimeUpEnd){
			GameMode.TimeUpEnd = false;
			KOText.text = "TIME UP";
			KOTextShadow.text = "TIME UP";
			TimeUp.Play ();
		}
		if (!KO.isPlaying && !TimeUp.isPlaying) {
			KOText.text = "";
			KOTextShadow.text = "";
		}
		if(GameMode.GameOver){
			GameOverWins ();
		}

		if (MenuScript.ArcadeMode) {
			wins = Arcade.wins;
			losses = Arcade.losses;
			draws = Arcade.draws;
			PlayerRecord.text = "Record[W/D/L]:"+wins+"/"+draws+"/"+losses;
			//Record[W/D/L]:12/12/12
		}
	}

	public void newRoundSound(){
		if(canPlayReadySound || MapDifficulty.restartGame){
			if (!GameMode.GameOver) {
			PauseAndResume ();
		} else {
			Ready.enabled = false;
			Fight.enabled = false;
				GameOverWins ();
			}
		}
		canPlayReadySound = false;
	}

	void PauseAndResume(){	
		Ready.Play ();
		Time.timeScale = 0;
		//Display Image here
		StartCoroutine(ResumeAfterNSeconds(2.3f));
	}

	float timer = 0;
	IEnumerator ResumeAfterNSeconds(float timePeriod)
	{
		yield return new WaitForEndOfFrame();
		timer += Time.unscaledDeltaTime;
		if (timer < timePeriod) {

			if (timer < 1.0f) {
				Ready.Play ();
				FightText.text = "READY?";
				FightText2.text = "READY?";
				Fight.Play();
			}
			if(timer >=1.0f){
				//FightText.color
				FightText.text = "FIGHT!";
				FightText2.text = "FIGHT!";
				RoundNo.text = "ROUND " + CurrentRound;

			}

			StartCoroutine (ResumeAfterNSeconds (2.3f));
		}
		else
		{
			Time.timeScale = 1;         
			timer = 0;
			FightText.text = "";
			FightText2.text = "";
			RoundNo.text = "";
		}
	}

	void GameOverWins()
	{	if (!KO.isPlaying) {
			StartCoroutine (GameOverAudio ());
		}
	}

	IEnumerator GameOverAudio(){
		yield return new WaitForSeconds(1.5F);
		if(GameMode.PlayerWonGame){
			YouWin.Play ();
			if (YouWin.isPlaying) {
				ResultText.text = "YOU WIN!";
			}
		}
		else if(GameMode.EnemyWonGame){
			YouLose.Play ();
			if(YouLose.isPlaying){
				ResultText.text ="YOU LOSE!";
			}
		}
		yield return new WaitForSeconds(1.5F);
		if (CharacterSelect.fight) {
			MapDifficulty.gameMode.resetKO = true;
		}
		else if(MenuScript.ArcadeMode){
			Arcade.gameMode.resetKO = true;
		}
		Time.timeScale = 0;

		if (MenuScript.ArcadeMode) {
			if (GameMode.PlayerWonGame) {
				ArcadeWinMenu.enabled = true;
				Arcade.ArcadeScore += 100;
				Arcade.ShieldBreakerCheck += 100;
			} else if (GameMode.EnemyWonGame) {
				ArcadeLoseMenu.enabled = true;
			}
		} else {
			GameOverMenu.enabled = true;
		}
		GameMode.GameOver = false;
	}



	IEnumerator GoToReset()
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
}
