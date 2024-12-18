using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterSelect : MonoBehaviour {
	public static string selected = "";
	public static string opponent = "";
	bool cleared;
	public Text vs;
	public Text pleaseSelect;
	public Text SubTitles;
	public Image BackDrop;
	public Image EnemyBackDrop;
	public Sprite CHLOE;
	public Sprite HYPEBEAST;
	public Sprite LIFTOFF;
	public Sprite YUKIE;
	public Sprite SCOTT;
	public Sprite GRAFUS;
	public Sprite DEFAULT;
	public static string stageChoice;
	private bool player1Selected = false;
	public bool bothFightersSelected = false;
	private string[] choosefrom = {"Character1"};
	public GameObject loadingScreen;
	public AudioSource liftOffAudio;
	public AudioSource hypebeastAudio;
	public AudioSource chloeAudio;
	public AudioSource yukieAudio;
	public AudioSource ScottAudio;
	public AudioSource GrafusAudio;
	public AudioSource music;
	public Slider slider;
	public static bool fight;
	public void opponentSelect(){
		
	}	

	void Start(){
		 
		pleaseSelect = GameObject.Find("pleaseSelect").GetComponent<Text>();
		pleaseSelect.text = "Select a Fighter";
		liftOffAudio = liftOffAudio.GetComponent<AudioSource>();
		hypebeastAudio = hypebeastAudio.GetComponent<AudioSource>();
		yukieAudio = yukieAudio.GetComponent<AudioSource>();
		chloeAudio = chloeAudio.GetComponent<AudioSource>();
		ScottAudio = ScottAudio.GetComponent<AudioSource>();
		music = music.GetComponent<AudioSource>();
		music.Play();
	}

	public void setOppBackDrop(string opp){
		switch (opp) {
		case "LIFTOFF":
			EnemyBackDrop.sprite = LIFTOFF;
				break;
		case "CHLOE":
			EnemyBackDrop.sprite = CHLOE;
				break;
		case "SCOTT":
			EnemyBackDrop.sprite = SCOTT;
			break;
		case "HYPEBEAST":
			EnemyBackDrop.sprite = HYPEBEAST;
				break;
		case "GRAFUS":
			EnemyBackDrop.sprite = GRAFUS;
			break;
		case "YUKIE":
			EnemyBackDrop.sprite = YUKIE;
			break;
			default:
			EnemyBackDrop.sprite = DEFAULT;
				break;
			}

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
		case "GRAFUS":
			BackDrop.sprite = GRAFUS;
			break;
		case "YUKIE":
			BackDrop.sprite = YUKIE;
			break;
		default:
			BackDrop.sprite = DEFAULT;
			break;
		}

	}

	public void c1Select(string name){

		if (selected == "") {
			player1Selected = true;
			selected = name;
			setBackDrop (name);
		} 

		else if(player1Selected){
			opponent = "O_"+name;
			bothFightersSelected = true;
			setOppBackDrop (name);
		}
	//	
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
		pleaseSelect.text = "Select an opponent";
	}

	public void clear(){
		cleared = true;
		selected = "";
		opponent = "";
		if (cleared) {
			BackDrop.sprite = DEFAULT;
			EnemyBackDrop.sprite = DEFAULT;
			pleaseSelect.text = "Select a Fighter";
		}
	//	SelectFrom.ResetCharacters ();
		cleared = false;
		bothFightersSelected = false;
		vs.text = "";
	}

	public void returnToMain(){
		StartCoroutine(MainMenu());
	}

	public void ok(){
		if (!fight) {
			if (!selected.Equals ("") && !opponent.Equals ("")) {
				StartCoroutine (playGame());
				pleaseSelect.text = "";
			} else if (selected == "") {
				pleaseSelect.text = "Select a Fighter";
			} else if (opponent == "") {
				pleaseSelect.text = "Select an opponent";
			}	
		} else { 
			StartCoroutine(Stage());
		}
	
	}

	void checkFighters(){
		if (player1Selected) {
			vs.text = "VS";
		}
		if (bothFightersSelected) {
			pleaseSelect.text = "Click the Fight Button to begin";
		}
	}


	void Update()
	{
		checkFighters ();

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

	IEnumerator Stage()
	{
		loadingScreen.SetActive (true);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Stage");
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
