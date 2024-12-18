using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviourScript : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int something){
		animator.GetComponent<Character> ().Attack = true;
		animator.SetFloat ("MoveSpeed",0);
		if (animator.tag == "Player") {
			if (Player.Instance.grounded) {
				Player.Instance.myRB.velocity = Vector2.zero;
			}
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int something){		
		animator.GetComponent<Character>().Attack = false;
		animator.ResetTrigger("attack");
		animator.ResetTrigger("throw");
	}
}
