using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandState : Iopponent {
	private Opponent opponent;
	private float idleTimer;
	private float idleDuration = 2f;

	public void Execute(){
		if (opponent.Target != null) {
			opponent.ChangeState (new MoveState ());

		}
		Idle ();
	}
	public void Enter (Opponent opponent){
		this.opponent = opponent;
	}
	public void Exit (){
	}
	public void OnTriggerEnter(Collider2D other){}

	private void Idle(){
		opponent.animator.SetFloat ("MoveSpeed",0);
		idleTimer += Time.deltaTime;
		if (idleTimer >= idleDuration) {
			opponent.ChangeState(new MoveState());
		}
	}
}
