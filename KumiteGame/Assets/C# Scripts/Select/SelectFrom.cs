using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class SelectFrom : MonoBehaviour
{
	private GameObject playerChoice;
	private GameObject opponentChoice;
	//private bool playAsLiftOff = false;
	public static GameObject CPU;
	public static GameObject Player1;
	private static GameObject theMap;
	private static GameObject[] array;
	private static GameObject[] array1;
	private GameObject levels;
	//	private bool character1Selected = false;
	private string selectedPlayer = CharacterSelect.selected;
	private string selectedOpponent = CharacterSelect.opponent;
	public static bool playMusic= true;
	private static GameObject[] Maps;
	AudioSource a;
	public Text playerName;
	public Text opponentName;
	public Text playerName1;
	public Text opponentName1;
	public static string p;
	public static string o;
	Vector3 playerStartPos;
	Vector3 opponentStartPos;
	public Slider slider;
	public GameObject loadingScreen;
	void Start ()
	{
		try{
			playerStartPos = new Vector3(-0.9f, 1.16f, 0.0f);
			opponentStartPos = new Vector3(1.1f, 1.16f, 0.0f);
			levels = GameObject.Find ("levels");
			array = GameObject.FindGameObjectsWithTag("Player"); //Finds all game objects tagged as "Player"	 
			array1 = GameObject.FindGameObjectsWithTag("Opponent"); //Finds all game objects tagged as "Opponent"
			Maps = GameObject.FindGameObjectsWithTag("Map"); ////Finds all game objects tagged as "Map"
			MusicForFight();
			Player1 = GameObject.Find (selectedPlayer); // Sets the player using the string selectedPlayer from the Character select class
			Player1.SetActive (true); //Player 1 is active, all other players are deactivated and invisible within the scene
			CPU = GameObject.Find (selectedOpponent); // Sets the opponet using the string selectedOpponent from the Character select class
			CPU.SetActive (true); //The opponent is active, all other opponents are deactivated and invisible within the scene
			Player1.transform.position = playerStartPos;
			CPU.transform.position = opponentStartPos;	
			ResetCharacters ();
			GameMode.TimeUpEnd = false;
		}
		catch(NullReferenceException ex){
			StartCoroutine (MainMenu());
		}
	}


	public static void GameReset2(){
		Vector3 playerStartPos;
		Vector3 opponentStartPos;
		playerStartPos = new Vector3(-0.9f, 1.16f, 0.0f);
		opponentStartPos = new Vector3(1.1f, 1.16f, 0.0f);
		if (CharacterSelect.fight) {
			MapDifficulty.gameMode.GameReset = true;
		}
		else if(MenuScript.ArcadeMode){
			Arcade.gameMode.GameReset = true;
		}
		Timer.remaining = Timer.timeSet;
		if (!GameMode.Draw) {
			Round.CurrentRound++;
		}
		GameMode.Draw = false;
		Player1.transform.position = playerStartPos;
		CPU.transform.position = opponentStartPos;
		EnemyHealth.UpdateHealth ();
		PlayerHealth.UpdateHealth ();
		EnemyHealth.enemyhit = true;
	}


	public void CharactersDontMove(){

		Player1.transform.position = playerStartPos;
		CPU.transform.position = opponentStartPos;
	}

	public void ResetCharacters(){

		for(int i = 0; i < array.Length;i++){ 
			if(!array[i].name.Equals(selectedPlayer)){
				array [i].SetActive (false);
			}
		}

		for(int i = 0; i < Maps.Length;i++){ //Reset Maps
			if (!MenuScript.Training) {
				if (!Maps [i].name.Equals (MapDifficulty.MapChoice)) {
					Maps [i].SetActive (false);
				}
			} else {
				Maps [i].SetActive (false);
			}
		}

		for(int i = 0; i < array1.Length;i++){ //Reset enemies
			if(!array1[i].name.Equals(selectedPlayer)){
				array1 [i].SetActive (false);
			}
		}
	}

	public static void ReAddCharacters(){

		for(int i = 0; i < array.Length;i++){
			array [i].SetActive (true);
		}
		for(int i = 0; i < Maps.Length;i++){
			Maps [i].SetActive (true);
		}
		for(int i = 0; i < array1.Length;i++){
			array1 [i].SetActive (true);
		}
	}



	void Update ()
	{
		if(!MenuScript.ArcadeMode){
			levels.SetActive (false);
		}
		checkSelectedPlayer ();
		checkSelectedOpponent ();

		if (GameMode.GameOver && !MapDifficulty.restartGame) {
			CharactersDontMove ();
		} else {
			playerStartPos = Player1.transform.position;
			opponentStartPos = CPU.transform.position;
		}
		returntofight ();
	}



	void chosenPlayer(){
		Player1.SetActive (true);
		playerChoice = Player1;
		playerName.text = selectedPlayer;
		playerName1.text = selectedPlayer;
		p = selectedPlayer;
	}


	void chosenOpponent(){
		CPU.SetActive (true);
		opponentChoice = CPU;
		opponentName.text = selectedOpponent.Substring(2);
		opponentName1.text = selectedOpponent.Substring(2);
		o = selectedOpponent;

	}

	void checkSelectedPlayer(){
		chosenPlayer ();
	}

	void checkSelectedOpponent(){
		chosenOpponent ();
	}

	public GameObject GetOpponent(){
		return CPU;
	}



	void MusicForFight(){
		if(CharacterSelect.fight && !playMusic){
			for (int i = 0; i < Maps.Length; i++) {
				a = Maps [i].GetComponentInChildren<AudioSource> ();
				a.Stop ();
			}
		}
	}


	void returntofight(){
		if (CPU.transform.position.x > 2.30) {
			CPU.GetComponent<Opponent>().changeDirection ();
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

