using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSight : MonoBehaviour {
	
	[SerializeField]
	private Opponent opponent;
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
		opponent.Target = other.gameObject;

		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			opponent.Target = null;

		}
	}
}
