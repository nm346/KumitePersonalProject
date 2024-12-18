using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PainSounds : MonoBehaviour {
	public AudioSource YukiePain;
	public AudioSource YukieAttack;
	public AudioSource ChloePain;
	public AudioSource ChloeAttack;
	public AudioSource LiftOffPain;
	public AudioSource LiftOffAttack;
	public AudioSource HypebeastPain;
	public AudioSource HypebeastAttack;
	public AudioSource ScottPain;
	public AudioSource ScottAttack;
	public AudioSource GrafusPain;
	public AudioSource GrafusAttack;

	string playerName;
	string opponentName;
	public static bool PlayCPUPain;
	public static bool PlayPlayerPain;
	public static bool PlayCPUAttack;
	public static bool PlayPlayerAttack;
	// Use this for initialization
	void Start () {
		try{
		playerName = SelectFrom.Player1.name;
		opponentName = SelectFrom.CPU.name.Substring(2);
		YukiePain = YukiePain.GetComponent<AudioSource>();
		YukieAttack = YukieAttack.GetComponent<AudioSource>();
		ChloePain = ChloePain.GetComponent<AudioSource>();
		ChloeAttack = ChloeAttack.GetComponent<AudioSource>();
		LiftOffPain = LiftOffPain.GetComponent<AudioSource>();
		LiftOffAttack = LiftOffAttack.GetComponent<AudioSource>();
		ScottPain = ScottPain.GetComponent<AudioSource>();
		ScottAttack = ScottAttack.GetComponent<AudioSource>();
		GrafusPain = GrafusPain.GetComponent<AudioSource>();
		GrafusAttack = GrafusAttack.GetComponent<AudioSource>();
		HypebeastAttack = HypebeastAttack.GetComponent<AudioSource>();
		HypebeastPain = HypebeastPain.GetComponent<AudioSource>();
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}

	
	// Update is called once per frame
	void Update () {
		playpains ();
		playAttacks ();
	}

	void playpains(){

			if (PlayPlayerPain) {
				PlayPlayerPain = false;
				PainPlayer();
			}
			if (PlayCPUPain) {
				PlayCPUPain = false;
				PainOpponent ();
			}
		}

	void playAttacks(){

		if (PlayPlayerAttack) {
			PlayPlayerAttack = false;
			AttackPlayer();
		}
		if (PlayCPUAttack) {
			PlayCPUAttack = false;
			AttackOpponent();
		}
	}

	void PainPlayer(){
		if (playerName == "YUKIE") {
			YukiePain.Play ();
		}
		else if (playerName == "HYPEBEAST") {
			HypebeastPain.Play ();
		}
		else if (playerName == "CHLOE") {
			ChloePain.Play ();
		}
		else if (playerName == "SCOTT") {
			ScottPain.Play ();
		}
		else if (playerName == "LIFTOFF") {
			LiftOffPain.Play ();
		}
		else if (playerName == "GRAFUS") {
			GrafusPain.Play ();
		}

	}

	void PainOpponent(){
		if (opponentName == "YUKIE") {
			YukiePain.Play ();
		}
		else if (opponentName == "HYPEBEAST") {
			HypebeastPain.Play ();
		}
		else if (opponentName == "CHLOE") {
			ChloePain.Play ();
		}
		else if (opponentName == "SCOTT") {
			ScottPain.Play ();
		}
		else if (opponentName == "LIFTOFF") {
			LiftOffPain.Play ();
		}
		else if (opponentName == "GRAFUS") {
			GrafusPain.Play ();
		}
	}
		


	void AttackPlayer(){
		if (playerName == "YUKIE") {
				YukieAttack.Play ();
		}
		if (playerName == "CHLOE") {
			ChloeAttack.Play ();
		}
		if (playerName == "HYPEBEAST") {
			HypebeastAttack.Play ();
		}
		if (playerName == "SCOTT") {
			ScottAttack.Play ();
		}
		if (playerName == "LIFTOFF") {
			LiftOffAttack.Play ();
		}
		if (playerName == "GRAFUS") {
			GrafusAttack.Play ();
		}
		}

		void AttackOpponent(){
		if (opponentName == "YUKIE") {
				YukieAttack.Play ();
		}
		if (opponentName == "CHLOE") {
			ChloeAttack.Play ();
		}
		if (opponentName == "HYPEBEAST") {
			HypebeastAttack.Play ();
		}
		if (opponentName == "SCOTT") {
			ScottAttack.Play ();
		}
		if (opponentName == "LIFTOFF") {
			LiftOffAttack.Play ();
		}
		if (opponentName == "GRAFUS") {
			GrafusAttack.Play ();
		}
	}
}
