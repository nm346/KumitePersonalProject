using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceState : Iopponent {
	private Opponent opponent;
	private float throwTimer;
	private float throwCoolDown;

	public void Execute(){
		if(opponent.InCombatRange){
			opponent.ChangeState (new AttackState ());
			opponent.animator.SetFloat ("MoveSpeed", 0);
		}
		else if(opponent.Target != null) {
			opponent.changeDirection ();
			opponent.MoveEnemy ();

		} else {
			opponent.ChangeState (new StandState ());
		}
	}
	public void Enter (Opponent opponent){
		this.opponent = opponent;
	}
	public void Exit (){

	}
	public void OnTriggerEnter(Collider2D other){}
}
