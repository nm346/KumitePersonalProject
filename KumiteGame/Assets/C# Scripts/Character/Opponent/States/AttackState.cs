using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : Iopponent {
	private Opponent opponent;
	private float combatTimer;
	private float combatCoolDown = 1.0f;
	private bool canCombat = true;
	private float jumpTimer;
	private float jumpCoolDown = 6.0f;
	private bool canJump = true;
	private int num;
	private int sound;
	private int sound2;
	private int jumpNum;
	private string attackChoice = "";
	Player pc = new Player();
	System.Random rnd = new System.Random();
	string[] attacks = new string[]{"punch","kick","r_kick","r_punch","block","duck"};

	public void Execute(){
		Combat ();
		Jump ();
		if (!opponent.InCombatRange) {
			opponent.ChangeState (new DistanceState ());
		} else if (opponent.Target == null) {
			opponent.ChangeState (new StandState ());
		}


	}
	public void Enter (Opponent opponent){
		this.opponent = opponent;
	}
	public void Exit (){
	}
	public void OnTriggerEnter(Collider2D other){}

		private void Combat(){
			combatTimer += Time.deltaTime;
	
		if (combatTimer >= combatCoolDown) {
			combatTimer += Time.deltaTime;
			if (combatTimer >= combatCoolDown) {
				canCombat = true;
				combatTimer = -0.1f;
				}

			if (canCombat) {
				canCombat = false;
				randomAttacks ();
				if (attackChoice != "" && opponent) {
					if (!Player.blocking1 && MoveState.moving == false) {
						if (Player.enemyHitType == "punch" && !Player.ducking) {
							Player.Epunched = true;
							PlayerHealth.hit = true;
							PlayerHealth.hitsTaken++;
						} else if (Player.enemyHitType == "kick") {
							Player.Ekicked = true;
							PlayerHealth.hit = true;
							PlayerHealth.hitsTaken++;

							} 
						else if(Player.enemyHitType == "block"){
							Player.Eblocked = true;
							}
						else if(Player.enemyHitType == "duck"){
							Player.Educk = true;
						}
					}
					else {
						Player.Eblocked = true;
					}
					opponent.animator.SetTrigger (attackChoice);	

				}
				}
			}
			
		}

	private void Jump(){
		jumpTimer += Time.deltaTime;
		if (jumpTimer >= jumpCoolDown) {
			canJump = true;
			jumpTimer = -0.1f;
		}
		if (canJump) {
			jumpNum = rnd.Next (0,6);

			if (jumpNum == 1) {
				//jump code
				Opponent.myRB.velocity = new Vector2 (Opponent.myRB.velocity.x,0f);
				Opponent.myRB.AddForce (new Vector2(0,Player.jumpPower),ForceMode2D.Impulse);
			}
			canJump = false;
		}
	}




	void randomAttacks(){
		num = rnd.Next (0,6);
		sound = rnd.Next (0,4);
		sound2 = sound - 1;
		if(num==sound || num ==sound2){
			PainSounds.PlayCPUAttack = true;
		}
		attackChoice = attacks[num];
		if (attackChoice == attacks [1] || attackChoice == attacks [2]) {
			Player.enemyHitType = "kick";
		} else if(attackChoice == attacks [0] || attackChoice == attacks [3]) {
			Player.enemyHitType = "punch";
		}
		else if(attackChoice == attacks [4]){
			Player.enemyHitType = "block";

		}
		else if(attackChoice == attacks [5]){
			Player.enemyHitType = "duck";

		}
	}
}
