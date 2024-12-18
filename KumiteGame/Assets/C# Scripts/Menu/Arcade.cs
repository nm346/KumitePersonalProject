using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Arcade : MonoBehaviour {


	public Button easy;
	public Button medium;
	public Button hard;
	public Button trueWarrior;
	public GameObject ArcadeClose;
	public Text roundText;
	public Text roundTimeText;
	public Text SubTitles;
	public static Round round;
	public static int Level = 1;
	public static bool changedRound = false;
	public static bool restartGame;
	public static string Difficulty = "EASY";
	public static GameMode gameMode;
	public static bool add25Used = true;
	public Slider slider;
	public GameObject loadingScreen;

	//Character SELECT
	public static string selected = "";
	System.Random rnd = new System.Random();
	public AudioSource liftOffAudio;
	public AudioSource hypebeastAudio;
	public AudioSource chloeAudio;
	public AudioSource yukieAudio;
	public AudioSource ScottAudio;
	public AudioSource GrafusAudio;
	public static int wins;
	public static int losses;
	public static int draws;
	public Sprite CHLOE;
	public Sprite HYPEBEAST;
	public Sprite GRAFUS;
	public Sprite LIFTOFF;
	public Sprite YUKIE;
	public Sprite SCOTT;
	public Sprite DEFAULT;
	public Text pleaseSelect;
	public Image BackDrop;
	public Image ArcadeHelp;
	public static int ArcadeScore;
	public static int ShieldBreakerCheck;
	public static string MapChoice;
	bool cleared;
	public static List<string> opponentsList;
	public static List<string> MapsList;
	public static int num;
	public static int num2;
	// Use this for initialization
	void Start () {
		GameMode.GameOver = false;
		opponentsList = new List<string> ();
		MapsList = new List<string> ();
		gameMode = new GameMode ();
		ArcadeScore = 0;
		ShieldBreakerCheck = 0;
		MapDifficulty.Difficulty = Difficulty;
		opponentsList.Add("O_CHLOE");
		opponentsList.Add("O_HYPEBEAST");
		opponentsList.Add("O_LIFTOFF");
		opponentsList.Add("O_SCOTT");
		opponentsList.Add("O_YUKIE");
		opponentsList.Add("O_GRAFUS");
		MapsList.Add ("Street Life");
		MapsList.Add ("Forbidden Kingdom");
		MapsList.Add ("Power Factory");
		MapsList.Add ("Priory Park");
		MapsList.Add ("SS1 Ship");
		MapsList.Add ("Lost Cave");
		ArcadeHelp.enabled = false;
		ArcadeClose.SetActive (false);
		pleaseSelect = GameObject.Find("pleaseSelect").GetComponent<Text>();
		pleaseSelect.text = "Select a Fighter";
		liftOffAudio = liftOffAudio.GetComponent<AudioSource>();
		hypebeastAudio = hypebeastAudio.GetComponent<AudioSource>();
		yukieAudio = yukieAudio.GetComponent<AudioSource>();
		chloeAudio = chloeAudio.GetComponent<AudioSource>();
		ScottAudio = ScottAudio.GetComponent<AudioSource>();
		GrafusAudio = GrafusAudio.GetComponent<AudioSource>();
		easy = easy.GetComponent<Button> ();
		medium = medium.GetComponent<Button> ();
		hard = hard.GetComponent<Button> ();
		trueWarrior = trueWarrior.GetComponent<Button> ();
		Round.MaxRound = 1;
		easyDifficulty ();
		if (Round.MaxRound == 1) {
			roundText.text = Round.MaxRound + " ROUND";
		} else {
			roundText.text = Round.MaxRound + " ROUNDS";
		}
		changedRound = false;
		ChangeRoundTime ();
	}


	public void FightSetter(){
		if(opponentsList.Count != 0 && opponentsList.Contains("O_"+CharacterSelect.selected)){
			opponentsList.Remove ("O_"+CharacterSelect.selected);
		}
		num = rnd.Next (0,opponentsList.Count);
		num2 = rnd.Next (0,MapsList.Count);
		CharacterSelect.opponent = opponentsList [num];
		MapChoice = MapsList [num2];
		MapDifficulty.MapChoice = MapChoice;

		if (selected != "") {
			StartCoroutine (playGame ());
		} else {
			pleaseSelect.text = "Please Select a fighter";
		}
	}

	// Update is called once per frame
	void Update () {

		ResetScript.numOfRounds = Round.MaxRound;
		ResetScript.map = MapChoice;
	}

	public void easyDifficulty(){
		PlayerHealth.maxHealth = 100;
		PlayerHealth.maxShield = 70;
		EnemyHealth.maxHealth = 100;
		EnemyHealth.maxShield = 100;
	}

	public void setBackDrop(string player){
		switch (player) {
		case "LIFTOFF":
			BackDrop.sprite = LIFTOFF;
			break;
		case "CHLOE":
			BackDrop.sprite = CHLOE;
			break;
		case "SCOTT":
			BackDrop.sprite = SCOTT;
			break;
		case "HYPEBEAST":
			BackDrop.sprite = HYPEBEAST;
			break;
		case "YUKIE":
			BackDrop.sprite = YUKIE;
			break;
		case "GRAFUS":
			BackDrop.sprite = GRAFUS;
			break;
		default:
			BackDrop.sprite = DEFAULT;
			break;
		}

	}

	public void ArcadeHelpOpen(){
		ArcadeHelp.enabled = true;
		ArcadeClose.SetActive(true);
	}

	public void ArcadeHelpClose(){
		ArcadeHelp.enabled = false;
		ArcadeClose.SetActive(false);
	}

	public void c1Select(string name){
		selected = "";

		if (selected == "") {
			selected = name;
			setBackDrop (name);
		} 
		switch(name){
		case "LIFTOFF":
			liftOffAudio.Play ();
			SubTitles.text = "LiftOff: Money!!";
			break;
		case "HYPEBEAST":
			hypebeastAudio.Play ();
			SubTitles.text = "HypeBeast: Next Supreme Drop!";
			break;
		case "CHLOE":
			chloeAudio.Play ();
			SubTitles.text = "Chloe Connors: How utterly pointless...";
			break;
		case "YUKIE":
			yukieAudio.Play ();
			SubTitles.text = "Yukie[Japanese]: If you want to run you're free to do so";
			break;
		case "SCOTT":
			ScottAudio.Play ();
			SubTitles.text = "Scott Connors: Violence is who I am...";
			break;
		case "GRAFUS":
			GrafusAudio.Play ();
			SubTitles.text = "Grafus: I WILL KILL YOU!";
			break;
		default:
			break;

		}
		CharacterSelect.selected = selected;
		pleaseSelect.text = selected;
	}



	public void ChangeRounds (){
		if (Round.MaxRound == 0) {
			Round.MaxRound = 1;
			roundText.text = Round.MaxRound + " ROUND";
			MenuScript.ArcadeRecord = 4800;
		}
		else if (Round.MaxRound == 1) {
			Round.MaxRound = 3;
			roundText.text = Round.MaxRound + " ROUNDS";
			MenuScript.ArcadeRecord = (4800*3);
		}
		else if (Round.MaxRound == 3) {
			Round.MaxRound = 5;
			roundText.text = Round.MaxRound + " ROUNDS";
			MenuScript.ArcadeRecord = (4800*5);
		}
		else if (Round.MaxRound == 5) {
			Round.MaxRound = 7;
			roundText.text = Round.MaxRound + " ROUNDS";
			MenuScript.ArcadeRecord = (4800*7);
		}
		else if (Round.MaxRound == 7) {
			Round.MaxRound = 1;
			roundText.text = Round.MaxRound + " ROUND";
			MenuScript.ArcadeRecord = (4800);
		}
		changedRound = true;
	}


	public void ChangeRounds2 (){
		if (Round.MaxRound == 0) {
			Round.MaxRound = 1;
		}
		else if (Round.MaxRound == 1) {
			Round.MaxRound = 3;
		}
		else if (Round.MaxRound == 3) {
			Round.MaxRound = 5;
		}
		else if (Round.MaxRound == 5) {
			Round.MaxRound = 7;
		}
		else if (Round.MaxRound == 7) {
			Round.MaxRound = 1;
		}
	}

	public void clear(){
		cleared = true;
		selected = "";
		if (cleared) {
			BackDrop.sprite = DEFAULT;
			pleaseSelect.text = "Select a Fighter";
		}
		cleared = false;
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

	public void ReturnMain(){
		StartCoroutine (MainMenu());
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

	IEnumerator playGame()
	{

		// The Application loads the Scene in the background at the same time as the current Scene.
		//This is particularly good for creating loading screens. You could also load the Scene by build //number.
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
