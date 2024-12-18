using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public Canvas pause;
	public Canvas settings;
//	public Canvas controlsList;
	public GameObject FightPause;
	public GameObject ArcadePause;
	public Button pauseButton;
	public Button musicOn;
	public Button musicOff;
	public Button close;
	public Button control_Open;
	public Button control_Close;
	public Canvas controlOverview;
	private static PauseMenu instance;
	public AudioSource trainingMusic;


	public GameObject loadingScreen;
	public Slider slider;

	bool playMusic;
	bool musicToggle;

	void Start () {
		try{
		//FightPause = FightPause.GetComponent<Canvas>();
		//ArcadePause = ArcadePause.GetComponent<Canvas> ();
		controlOverview = controlOverview.GetComponent<Canvas> ();
		trainingMusic = trainingMusic.GetComponent<AudioSource>();
		playMusic = true;
		pause = pause.GetComponent<Canvas> ();
		settings = settings.GetComponent<Canvas> ();
		pauseButton = pauseButton.GetComponent<Button> ();
		musicOn = musicOn.GetComponent<Button> ();
		musicOff = musicOff.GetComponent<Button> ();
		close = close.GetComponent<Button> ();
		control_Open = control_Open.GetComponent<Button> ();
		control_Close = control_Close.GetComponent<Button> ();
		pause.enabled = false;
		settings.enabled = false;
		controlOverview.enabled = false;
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}

	//Show options menu, hide start options
	public void pausePress(){
		pause.enabled = true;
		pauseButton.enabled = false;
		Time.timeScale = 0;
		if (MenuScript.ArcadeMode) {
			ArcadePause.SetActive(true);
			FightPause.SetActive(false);
		} else {
			ArcadePause.SetActive(false);
			FightPause.SetActive(true);
		}
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

	public void openSettings(){
		pause.enabled = false;
		settings.enabled = true;
	}

	public void openControls(){
		settings.enabled = false;
		controlOverview.enabled = true;

	}

	public void closeControls(){
		settings.enabled = true;
		controlOverview.enabled = false;
	}

	public void closeSettings(){
		pause.enabled = true;
		settings.enabled = false;
	}

	//close options menu
	public void resume(){
		pause.enabled = false;
		Time.timeScale = 1;
		pauseButton.enabled = true;
	}

	//load main menu
	public void returnToMain(){
		Time.timeScale = 1;
		pause.enabled = false;
		SelectFrom.ReAddCharacters ();
		GameMode.GameOver = false;
		Arcade.ArcadeScore = 0;
		Arcade.ShieldBreakerCheck = 0;
		Timer.remaining = 60;
		Arcade.Level = 1;
		StartCoroutine(MainMenu());

	}

	public void rematch(){
		StartCoroutine (returnMapDifficulty());
	}
	public void returnToSelect (){
		SelectFrom.ReAddCharacters ();
		Time.timeScale = 1;
		pause.enabled = false;
		GameMode.GameOver = false;
		CharacterSelect.selected = "";
		CharacterSelect.opponent = "";
		StartCoroutine(SelectMenu());
	}



	void Update () {
		if (playMusic == true && musicToggle == true)
		{
			trainingMusic.Play();
			musicToggle = false;
		}
		if (playMusic == false && musicToggle == true)
		{
			trainingMusic.Stop();
			musicToggle = false;
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

	IEnumerator returnMapDifficulty()
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

	//	AsyncOperation async;
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
