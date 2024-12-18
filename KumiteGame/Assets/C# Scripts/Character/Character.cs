using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public abstract class Character : MonoBehaviour {
	public Animator MyAnimator { get; private set;}
	public bool Attack{ get; set;}
	public float maxSpeed;
	public float horizontal;
	public float vertical;
	protected bool canMove = true;
	public bool facingRight = false;
	private bool attacki;
	public bool punch_L;
	public bool punch_R;
	public bool kick_L;
	public bool kick_R;
	public static bool blocking1 = false;
	private string[] additionalAnims;
	public static string play;



	// Use this for initialization
	public virtual void Start () {
		try{
		MyAnimator = SelectFrom.Player1.GetComponent<Animator>();
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}

	public void punchR(){
		MyAnimator.SetTrigger ("punch");
		Player.hitType = "punch";
	}

	public void kickR(){
		MyAnimator.SetTrigger ("kick");
		Player.hitType = "kick";
	}

	public void punchL(){
		MyAnimator.SetTrigger ("r_punch");
		Player.hitType = "punch";
	}

	public void kickL(){
		MyAnimator.SetTrigger ("r_kick");
		Player.hitType = "kick";
	}

	public void duck(){
		MyAnimator.SetTrigger ("duck");
		Player.hitType = "duck";
	}

	public void blocking(){
		MyAnimator.SetTrigger ("block");
		Player.hitType = "block";
	}



	public void changeDirection(){
		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);﻿
	}
	public void toggleCanMove(){
		canMove = !canMove;
	}
}
