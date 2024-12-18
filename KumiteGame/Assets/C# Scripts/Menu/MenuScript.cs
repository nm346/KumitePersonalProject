using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour {
	public Canvas optionsMenu;
	public Canvas TrainingSettings;
	public Canvas FightSettings;
	public Canvas controlOverview;
	public Button startText;
	public Button TrainingSets;
	public Button optionsText;
	public Button musicOn;
	public Button musicOff;
	public static int ArcadeRecord;
	public AudioSource menuMusic;
	public static bool ArcadeMode;
	public GameObject loadingScreen;
	public static bool Training = false;
	public Slider slider;
	bool playMusic;
	bool musicToggle;

	// Use this for initialization
	void Start () {
		ArcadeMode = false;
		CharacterSelect.fight = false;
		Training = false;
		playMusic = true;
		ArcadeRecord = 4900;
		MapDifficulty.MapChoice = "";
		musicOn = musicOn.GetComponent<Button> ();
		TrainingSets = TrainingSets.GetComponent<Button> ();
		musicOff = musicOff.GetComponent<Button> ();
		menuMusic = menuMusic.GetComponent<AudioSource>();
		optionsMenu = optionsMenu.GetComponent<Canvas> ();
		FightSettings = FightSettings.GetComponent<Canvas> ();
		controlOverview = controlOverview.GetComponent<Canvas> ();
		TrainingSettings = TrainingSettings.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		optionsText = optionsText.GetComponent<Button> ();
		PlayerHealth.maxHealth = 100;
		PlayerHealth.maxShield = 27;
		EnemyHealth.maxHealth = 100;
		EnemyHealth.maxShield = 100;
		optionsMenu.enabled = false;
		TrainingSettings.enabled = false;
		FightSettings.enabled = false;
		controlOverview.enabled = false;
		unlimitedOff ();
	}

	//Show options menu, hide start options
	public void optionsPress(){
		optionsMenu.enabled = true;
		startText.enabled = false;
		optionsText.enabled = false;
	}

	public void TrainSetPress(){
		optionsMenu.enabled = false;
		FightSettings.enabled = false;
		TrainingSettings.enabled = true;
		startText.enabled = false;
		optionsText.enabled = false;
		controlOverview.enabled = false;
	}

	public void FightSetPress(){
		optionsMenu.enabled = false;
		FightSettings.enabled = true;
		TrainingSettings.enabled = false;
		startText.enabled = false;
		optionsText.enabled = false;
		controlOverview.enabled = false;
	}

	public void UISetPress(){
		optionsMenu.enabled = false;
		FightSettings.enabled = false;
		TrainingSettings.enabled = false;
		startText.enabled = false;
		optionsText.enabled = false;
		controlOverview.enabled = true;
	}



	//close options menu
	public void closePress(){
		optionsMenu.enabled = false;

		startText.enabled = true;
		optionsText.enabled = true;
	}

	//Close training options
	public void closeTrainPress(){
		TrainingSettings.enabled = false;
		FightSettings.enabled = false;
		optionsMenu.enabled = true;
		startText.enabled = true;
		optionsText.enabled = true;
		controlOverview.enabled = false;
	}

	//load training stage

	public void GoToArcade(){
		StartCoroutine(PlayArcadeMode());
	}

	public void stop_Music(){
		playMusic = false;
		musicToggle = true;
		musicOff.enabled = false;
		musicOn.enabled = true;
	}

	public void play_Music(){
		playMusic = true;
		musicToggle = true;
		musicOn.enabled = false;
		musicOff.enabled = true;
	}

	public void returnToSelect (){
		CharacterSelect.selected = "";
		CharacterSelect.opponent = "";
		CharacterSelect.fight = false;
		StartCoroutine(SelectMenu());
	}

	public void fightNow (){
		CharacterSelect.selected = "";
		CharacterSelect.opponent = "";
		CharacterSelect.fight = true;
		StartCoroutine(SelectMenu());

	}

	public void TrainingStage (){
		CharacterSelect.selected = "";
		CharacterSelect.opponent = "";
		CharacterSelect.fight = false;
		Training = true;
		Timer.remaining = 400;
		StartCoroutine(SelectMenu());

	}
	// Update is called once per frame
	void Update () {
		if (playMusic == true && musicToggle == true)
		{
			menuMusic.Play();
			musicToggle = false;
		}
		if (playMusic == false && musicToggle == true)
		{
			menuMusic.Stop();
			musicToggle = false;
		}
	}
	IEnumerator PlayArcadeMode()
	{
		loadingScreen.SetActive (true);
		ArcadeMode = true;
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Arcade");
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

	//Fight option buttons
	public void FightsfxOn(){
		Player.fightSFX = true;
	}

	public void FightsfxOff(){
		Player.fightSFX = false;
	}

	public void FightMusicOn(){
		SelectFrom.playMusic = true;
	}

	public void FightMusicOff(){
		SelectFrom.playMusic = false;
	}

	//Training Option buttons
	public void unlimitedOn(){
		PlayerHealth.unlimited = true;
		EnemyHealth.unlimited = true;
	}

	public void unlimitedOff(){
		PlayerHealth.unlimited = false;
		EnemyHealth.unlimited = false;
	}

	public void TrainingsfxOn(){
		Player.trainingSFX = true;
	}

	public void TrainingsfxOff(){
		Player.trainingSFX = false;
	}



	public void TrainingMusicOn(){
		Player.TrainingMusic = true;
	}

	public void TrainingMusicOff(){
		Player.TrainingMusic = false;
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
}
