using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : Iopponent {
	private float moveTimer;
	public static bool moving;
	private float moveDuration = 10;
	private Opponent opponent;
	public void Execute(){
		Move ();
		opponent.MoveEnemy ();
		if (opponent.Target != null) {
			opponent.transform.localScale = new Vector3(opponent.transform.localScale.x * -1f, opponent.transform.localScale.y, opponent.transform.localScale.z);﻿

			opponent.ChangeState (new DistanceState ());
		}
	}
	public void Enter (Opponent opponent){
		this.opponent = opponent;
		moving = true;
	}
	public void Exit (){
		opponent.animator.SetFloat ("MoveSpeed", 0);
		moving = false;
	}
	public void OnTriggerEnter(Collider2D other){

		if (other.tag == "edge") {
			opponent.changeDirection ();
		}
	}

	private void Move(){
		moveTimer += Time.deltaTime;
		if (moveTimer >= moveDuration) {
			opponent.ChangeState(new StandState());
		}
	}
}
