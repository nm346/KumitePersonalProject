using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour {
//	public static string difficulty;
	public static string player;
	public static string map;
	public static string opponent;
	public static int numOfRounds;
	public GameObject loadingScreen;
	public Image BackDrop;
	public Image EnemyBackDrop;
	public Image Stage;
	public Sprite CHLOE;
	public Sprite HYPEBEAST;
	public Sprite LIFTOFF;
	public Sprite YUKIE;
	public Sprite SCOTT;
	public Sprite GRAFUS;
	public Sprite DEFAULT;
	public Sprite ForbiddenKingdom;
	public Sprite PowerFactory;
	public Sprite StreetLife;
	public Sprite SS1Ship;
	public Sprite LostCave;
	public Sprite PrioryPark;
	public Sprite Training;
	public Slider slider;
	public Text stage;
	public Text stage1;
	// Use this for initialization
	void Start () {
		
		setBackDrop (CharacterSelect.selected);
		setOppBackDrop (CharacterSelect.opponent);
		if (MapDifficulty.MapChoice != "") {
			stage.text = "Stage:" + MapDifficulty.MapChoice;
			stage1.text = "Stage:" + MapDifficulty.MapChoice;
		} else {
			stage.text = "Stage: Training Stage";
			stage1.text = "Stage: Training Stage";
		}
		setStage ();
		StartCoroutine (playGame());
	}
	
	void Update () {
	}

	public void setOppBackDrop(string opp){
		switch (opp) {
		case "O_LIFTOFF":
			EnemyBackDrop.sprite = LIFTOFF;
			break;
		case "O_CHLOE":
			EnemyBackDrop.sprite = CHLOE;
			break;
		case "O_HYPEBEAST":
			EnemyBackDrop.sprite = HYPEBEAST;
			break;
		case "O_YUKIE":
			EnemyBackDrop.sprite = YUKIE;
			break;
		case "O_SCOTT":
			EnemyBackDrop.sprite = SCOTT;
			break;
		case "O_GRAFUS":
			EnemyBackDrop.sprite = GRAFUS;
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
		case "HYPEBEAST":
			BackDrop.sprite = HYPEBEAST;
			break;
		case "YUKIE":
			BackDrop.sprite = YUKIE;
			break;
		case "SCOTT":
			BackDrop.sprite = SCOTT;
			break;
		case "GRAFUS":
			BackDrop.sprite = GRAFUS;
			break;
		default:
			BackDrop.sprite = DEFAULT;
			break;
		}

	}

	public void setStage(){
		switch (MapDifficulty.MapChoice) {
		case "Street Life":
			Stage.sprite = StreetLife;
			break;
		case "Forbidden Kingdom":
			Stage.sprite = ForbiddenKingdom;
			break;
		case "Power Factory":
			Stage.sprite = PowerFactory;
			break;
		case "Priory Park":
			Stage.sprite = PrioryPark;
			break;
		case "SS1 Ship":
			Stage.sprite = SS1Ship;
			break;
		case "Lost Cave":
			Stage.sprite = LostCave;
			break;
		default:
			Stage.sprite = Training;
			break;
		}

	}

	IEnumerator playGame()
	{

		loadingScreen.SetActive (true);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Fight");
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
