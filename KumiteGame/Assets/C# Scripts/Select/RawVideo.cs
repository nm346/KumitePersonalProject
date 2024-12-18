using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using UnityEngine.SceneManagement;

public class RawVideo: MonoBehaviour
{
	public RawImage rawimage;
	public VideoPlayer videoPlayer;

	void Start()
	{
		try{
		Application.runInBackground = true;
		StartCoroutine(playVideo());
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}

	IEnumerator playVideo()
	{
		videoPlayer.playOnAwake = false;
		videoPlayer.Prepare();

		//Wait until Movie is prepared
		WaitForSeconds waitTime = new WaitForSeconds(1);
		while (!videoPlayer.isPrepared)
		{
			yield return waitTime;
			break;
		}


		//Assign the Texture from Movie to RawImage to be displayed
		rawimage.texture = videoPlayer.texture;

		//Play Movie 
		videoPlayer.Play();

		while (videoPlayer.isPlaying)
		{
			yield return null;
		}
	}
}