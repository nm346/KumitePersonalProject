using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingControl : MonoBehaviour {
	public GameObject loadingScreen;
	public Slider slider;
	AsyncOperation async;

	public void LoadScreenExample(int level){
		StartCoroutine (Loading(level));
	}

	IEnumerator Loading(int level){
	
		loadingScreen.SetActive (true);
		async = SceneManager.LoadSceneAsync (level);
		async.allowSceneActivation = false;

		while (async.isDone == false) {
			slider.value = async.progress;
			if (async.progress == 0.9f) {
				slider.value = 1f;
				async.allowSceneActivation = true;
			}
			yield return null;
		}
	}

}
