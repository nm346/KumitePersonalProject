using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Opponent : Character {

	private Iopponent currentState;
	public Animator animator;
	public static Rigidbody2D myRB;
	public bool moving = true;
	public GameObject Target{ get; set; }
	private float combatRange = 0.3f;
	public static bool check;
	[SerializeField]
	private float throwRange;

	public bool InCombatRange{
		get
		{
			if(Target != null){
				return Vector2.Distance(transform.position,Target.transform.position) <= combatRange; //combat range
			
			}

			return false;
		}
	}

	// Use this for initialization
	public override void Start () {
		try{
		myRB = SelectFrom.CPU.GetComponent<Rigidbody2D>();
		base.Start ();
		animator = gameObject.GetComponent<Animator>();
		ChangeState (new MoveState());
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
		}

	// Update is called once per frame
	void Update () {
		currentState.Execute ();
		LookAtTarget ();

		check = InCombatRange;	
	}

	private void LookAtTarget(){
		if (Target != null) {
			float xDir = Target.transform.position.x - transform.position.x;

			if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
				changeDirection ();
			}
		}


	}

	public void ChangeState(Iopponent newState){
		if (currentState != null) {
			currentState.Exit ();
		}
		currentState = newState;

		currentState.Enter (this);
	}

	public void MoveEnemy(){	
		if (!Attack) {
			animator.SetFloat ("MoveSpeed", 1);
			transform.Translate (GetDirection () * (maxSpeed * Time.deltaTime));
		} 

	
	}

	public Vector2 GetDirection (){
		return facingRight ? Vector2.left : Vector2.right;
	}

	void OnTriggerEnter2D(Collider2D other){
		try{
		currentState.OnTriggerEnter (other);
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}


}
