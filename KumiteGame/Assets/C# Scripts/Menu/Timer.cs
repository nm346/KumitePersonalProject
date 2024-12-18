using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
	public Text timerText;
	public Text shadow;
	public static float remaining = 60;
	public static float timeSet;
	public static string time;
//	private float timeSet = remaining;
	void Start () {
		//remaining = 60;
		timerText.text = "∞";
		shadow.text = "∞";
	}

	void Update ()
	{
		if (remaining != 400) {
			remaining -= Time.deltaTime;
			if (remaining > 0) {
				timerText.text = "" + (int)remaining;
				shadow.text = "" + (int)remaining;
			} else {
				timerText.text = "00";
				shadow.text = "00";
			}
			time = timerText.text;
		}
	}


}
