using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MapDifficulty : MonoBehaviour {
	public Button map1;
	public Button map2;
	public Button map3;
	public Button map4;
	public Button map5;
	public Button map6;
	public Button play;
	public Button easy;
	public Button medium;
	public Button hard;
	public Button trueWarrior;
	public Text mapChoice;
	public Text difficultyText;
	public Text roundText;
	public Text roundTimeText;
	public AudioSource music;
	public GameObject loadingScreen;
	public Slider slider;
	public Button Select;
	public static Round round;
	System.Random rnd = new System.Random();
	public static bool changedRound = false;
	public static bool restartGame;
	public static GameMode gameMode;
	public static string MapChoice = "";
	public static string Difficulty = "MEDIUM";
	public string[] mapsArray = {"Street Life","Forbidden Kingdom","Power Factory","Priory Park","SS1 Ship","Lost Cave"};

	// Use this for initialization
	void Start () {
		gameMode = new GameMode ();
		//Map Buttons
		round = new Round();
		map1 = map1.GetComponent<Button> ();
		map2 = map2.GetComponent<Button> ();
		map3 = map3.GetComponent<Button> ();
		map4 = map4.GetComponent<Button> ();
		map5 = map5.GetComponent<Button> ();
		map6 = map6.GetComponent<Button> ();
		play = play.GetComponent<Button> ();
		music = music.GetComponent<AudioSource>();
		GameMode.GameOver = false;
		music.Play();
		easy = easy.GetComponent<Button> ();
		medium = medium.GetComponent<Button> ();
		hard = hard.GetComponent<Button> ();
		trueWarrior = trueWarrior.GetComponent<Button> ();
		Select = Select.GetComponent<Button> ();
		Round.MaxRound = 1;
		mediumDifficulty ();
		if (Round.MaxRound == 1) {
			roundText.text = Round.MaxRound + " ROUND";
		} else {
			roundText.text = Round.MaxRound + " ROUNDS";
		}
		changedRound = false;
	}

	// Update is called once per frame
	void Update () {
		Difficulty = difficultyText.text;
		//MapChoice = mapChoice.text;
		ResetScript.map = MapChoice;
		ResetScript.numOfRounds = Round.MaxRound;
		mapChoice.text = MapChoice;

	}

	public void Fight(){
		int pos =  Array.IndexOf(mapsArray, MapChoice);
		if (pos > -1)
		{
			StartCoroutine (playGame ());
		}
		else {
			mapChoice.text = "Select a stage";
		}
	}

	public void Return(){
		StartCoroutine (SelectMenu());
	}

	IEnumerator playGame()
	{
		GameObject difficluty = GameObject.Find ("Difficulty");
		GameObject maps = GameObject.Find ("Maps");
		GameObject mainButtons = GameObject.Find ("MainButtons");
		GameObject titles = GameObject.Find ("Titles");

		difficluty.gameObject.SetActive (false);
		maps.gameObject.SetActive (false);
		mainButtons.gameObject.SetActive (false);
		titles.gameObject.SetActive (false);
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

	IEnumerator SelectMenu()
	{
		loadingScreen.SetActive (true);



		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Select");
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
	//Map settings
	public void sMap1(){
		MapChoice = "Street Life";
	}
	public void sMap2(){
		MapChoice = "Forbidden Kingdom";
	}
	public void sMap3(){
		MapChoice = "Power Factory";
	}
	public void sMap4(){
		MapChoice = "Priory Park";
	}
	public void sMap5(){
		MapChoice = "SS1 Ship";
	}
	public void sMap6(){
		MapChoice = "Lost Cave";
	}

	//Difficulty settings
	public void easyDifficulty(){
		difficultyText.text = "EASY";
		PlayerHealth.maxHealth = 100;
		PlayerHealth.maxShield = 70;
		EnemyHealth.maxHealth = 100;
		EnemyHealth.maxShield = 100;
	}
	public void mediumDifficulty(){
		difficultyText.text = "MEDIUM";
		PlayerHealth.maxHealth = 100;
		PlayerHealth.maxShield = 27;
		EnemyHealth.maxHealth = 100;
		EnemyHealth.maxShield = 100;
	}
	public void hardDifficulty(){
		difficultyText.text = "HARD";
		PlayerHealth.maxHealth = 100;
		PlayerHealth.maxShield = 15;
		EnemyHealth.maxHealth = 100;
		EnemyHealth.maxShield = 100;
	}
	public void trueWarriorDifficulty(){
		difficultyText.text = "TRUEWARRIOR";
		PlayerHealth.maxHealth = 100;
		PlayerHealth.maxShield = 0;
		EnemyHealth.maxHealth = 100;
		EnemyHealth.maxShield = 100;
	}

	public void randomStage(){
		int num;
		num = rnd.Next (0,6);
		MapChoice = mapsArray [num];
	}

	public void ChangeRounds (){
		if (Round.MaxRound == 0) {
			Round.MaxRound = 1;
			roundText.text = Round.MaxRound + " ROUND";
		}
		else if (Round.MaxRound == 1) {
			Round.MaxRound = 3;
			roundText.text = Round.MaxRound + " ROUNDS";
		}
		else if (Round.MaxRound == 3) {
			Round.MaxRound = 5;
			roundText.text = Round.MaxRound + " ROUNDS";
		}
		else if (Round.MaxRound == 5) {
			Round.MaxRound = 7;
			roundText.text = Round.MaxRound + " ROUNDS";
		}
		else if (Round.MaxRound == 7) {
			Round.MaxRound = 1;
			roundText.text = Round.MaxRound + " ROUND";
		}
		changedRound = true;
	}

	public void ChangeRoundTime (){
		if (Timer.remaining == 60) {
			Timer.remaining = 400;
			Timer.timeSet = Timer.remaining;
			roundTimeText.text = "ROUND TIME: ∞";
		} else if (Timer.remaining == 400) {
			Timer.remaining = 30;
			roundTimeText.text = "ROUND TIME:" + Timer.remaining + " SECS";
			Timer.timeSet = Timer.remaining;
		} else if (Timer.remaining == 30) {
			Timer.remaining = 45;
			roundTimeText.text = "ROUND TIME:" + Timer.remaining + " SECS";
			Timer.timeSet = Timer.remaining;
		} else if (Timer.remaining == 45) {
			Timer.remaining = 60;
			roundTimeText.text = "ROUND TIME:" + Timer.remaining + " SECS";
			Timer.timeSet = Timer.remaining;
		} else if (Timer.remaining == 0) {
			Timer.remaining = 60;
			roundTimeText.text = "ROUND TIME:" + Timer.remaining + " SECS";
			Timer.timeSet = Timer.remaining;
		} else {
			Timer.remaining = 60;
			roundTimeText.text = "ROUND TIME:" + Timer.remaining + " SECS";
			Timer.timeSet = Timer.remaining;
		}
			
	}
}
