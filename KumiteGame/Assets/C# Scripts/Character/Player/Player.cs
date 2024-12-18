using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CnControls;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class Player : Character
{

	public Rigidbody2D myRB;
	public bool grounded = false;
	public static bool stGrounded;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;

	public GameObject Target{ get; set; }

	public static float jumpPower = 7.6f;
	public Button xButton;
	public Button aButton;
	public Button yButton;
	public Button bButton;
	public Button blockButton;
	SpriteRenderer renderer;
	public static bool ducking = false;
	public static bool Educking = false;
	public static bool Eblocking = false;
	public bool canHitEnemy;
	private float combatTimer;
	private float combatCoolDown = 0.03f;
	private bool canCombat;
	public Timer timer;
	public GameObject player;
	GameObject SFXs;
	GameObject music;
	private int num;
	private int sound;
	private int sound2;
	static Player instance;
	public AudioSource blockSFX;
	public AudioSource punchSFX;
	public AudioSource kickSFX;
	public AudioSource EblockSFX;
	public AudioSource EpunchSFX;
	public AudioSource EkickSFX;
	public static bool Epunched = false;
	public static bool Ekicked = false;
	public static bool Eblocked = false;
	public static bool Educk = false;
	public static string hitType;
	public static string enemyHitType;
	public static bool tookHardHit;
	public static string hitFromEnemy;
	public static bool trainingSFX = true;
	public static bool fightSFX = true;
	public static bool TrainingMusic = true;
	public static bool canCelebrate;
	System.Random rnd = new System.Random ();


	public static Player Instance { get { return instance; } }

	// Use this for initialization
	public override void Start ()
	{
		try{
		base.Start ();
		player = SelectFrom.Player1;
		myRB = player.GetComponent<Rigidbody2D> ();
		groundCheck = player.GetComponentInParent<Transform> ();
		renderer = player.GetComponent<SpriteRenderer> ();
		xButton = xButton.GetComponent<Button> ();
		aButton = aButton.GetComponent<Button> ();
		yButton = yButton.GetComponent<Button> ();
		bButton = bButton.GetComponent<Button> ();
		blockSFX = blockSFX.GetComponent<AudioSource> ();
		kickSFX = kickSFX.GetComponent<AudioSource> ();
		punchSFX = punchSFX.GetComponent<AudioSource> ();
		EblockSFX = blockSFX;
		EpunchSFX = punchSFX;
		EkickSFX = kickSFX;
		tookHardHit = false;
		blocking1 = false;
		xButton.onClick.AddListener (delegate() {
			attackEnemy ();
		});		
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		SFXs = GameObject.FindGameObjectWithTag ("SFX");
		music = GameObject.FindGameObjectWithTag ("Music");
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}

	void Update ()
	{
		SoundEffects ();
		horizontal = CnInputManager.GetAxis ("Horizontal");
		vertical = CnInputManager.GetAxis ("Vertical");
		//Turning trtaining SFX on and off
		if (canMove && grounded && vertical > 0.7) {
			MyAnimator.SetBool ("isGrounded", false);
			myRB.velocity = new Vector2 (myRB.velocity.x, 0f);
			myRB.AddForce (new Vector2 (0, jumpPower), ForceMode2D.Impulse);
			grounded = false;
		}
		grounded = IsGrounded ();
		if (canCelebrate) {
			MyAnimator.SetTrigger ("won");
			canCelebrate = false;
		}

		if (vertical < -0.7 || blocking1) {
			MyAnimator.SetBool ("canIdle", false);
			if (vertical < -0.7) {
				ducking = true;
				duck ();
			} else {
				ducking = false;
			}
		} else {
			MyAnimator.SetBool ("canIdle", true);
		}
		if (enemyHitType == "duck" && hitType == "punch") {
			canHitEnemy = false;
		} else {
			canHitEnemy = true;
		}
		IsGrounded ();
		MyAnimator.SetBool ("isGrounded", grounded);
		float move = horizontal;
		myRB.velocity = new Vector2 (move * maxSpeed, myRB.velocity.y);
		MyAnimator.SetFloat ("MoveSpeed", Mathf.Abs (move));
		//pressing to move
		if (canMove) {
			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight) {
				Flip ();
			}
		} else {
			myRB.velocity = new Vector2 (0, myRB.velocity.y);
			MyAnimator.SetFloat ("MoveSpeed", 0);

		}
		if (tookHardHit) {
			CheckAnim (hitFromEnemy);
			tookHardHit = false;
		}

		canUseButtons ();
		EnemySFX ();

	}

	private void canUseButtons ()
	{
		if (MyAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle") || !IsGrounded ()) {
			xButton.interactable = true;
			yButton.interactable = true;
			aButton.interactable = true;
			bButton.interactable = true;
		} else {
			xButton.interactable = false;
			yButton.interactable = false;
			aButton.interactable = false;
			bButton.interactable = false;
		}

		if (blocking1) {
			xButton.interactable = false;
			yButton.interactable = false;
			aButton.interactable = false;
			bButton.interactable = false;
		}
	}

	private void attackEnemy ()
	{
		num = rnd.Next (0, 6);
		sound = rnd.Next (0, 4);
		sound2 = sound - 1;
		if (Opponent.check) {
			canHitEnemy = true;
		} else {
			canHitEnemy = false;
		}
		if (enemyHitType == "block") {
			canHitEnemy = false;
		}
		if (canHitEnemy && canCombat) {
			if (num == sound || num == sound2) {
				if (hitType == "punch" || hitType == "kick") {
					PainSounds.PlayPlayerAttack = true;
				}
			}

			if (hitType == "punch") {
				punchSFX.Play ();
			} else if (hitType == "kick") {
				kickSFX.Play ();
			} else {
				blockSFX.Play ();
			}
			EnemyHealth.enemyhit = true;
			canCombat = false;
		} else {
		}
	}

	public void Combat ()
	{
		combatTimer += Time.deltaTime;
		if (combatTimer >= combatCoolDown) {
			combatTimer += Time.deltaTime;
			if (combatTimer >= combatCoolDown) {
				canCombat = true;
				combatTimer = 0;
			} else {
				canCombat = false;
			}

			if (canCombat) {
				//	
				attackEnemy ();
			}
		}




	}

	void CheckAnim (string s)
	{

		switch (s) {
		case "hurt":
			MyAnimator.SetTrigger (s);
			PainSounds.PlayPlayerPain = true;
			break;
		case "ko":
			MyAnimator.SetTrigger (s);
			break;
		case "lost":
			MyAnimator.SetTrigger (s);
			PainSounds.PlayPlayerPain = true;
			break;
		case "won":
			MyAnimator.SetTrigger (s);
			break;
		default:
			s = "";
			break;

		}
		s = "";

	}

	bool IsGrounded ()
	{
		Vector2 position = groundCheck.position;
		Vector2 direction = Vector2.down;
		float distance = 1.0f;

		RaycastHit2D hit = Physics2D.Raycast (position, direction, distance, groundLayer);
		if (hit.collider != null) {
			return true;
		} else {
			return false;
		}

	}

	void checkIfBlocking ()
	{
		if (blocking1 || ducking) {
			xButton.interactable = false;
			yButton.interactable = false;
			aButton.interactable = false;
			bButton.interactable = false;
		} else {
			xButton.interactable = true;
			yButton.interactable = true;
			aButton.interactable = true;
			bButton.interactable = true;
		}
	}

	public void isBlocking ()
	{
		blocking1 = true;
		MyAnimator.SetTrigger ("block");

	}

	public void notBlocking ()
	{
		blocking1 = false;
	}

	public void EisBlocking ()
	{
		Eblocking = true;
	}

	public void EnotBlocking ()
	{
		Eblocking = false;
	}

	void EnemySFX ()
	{
		if (Epunched) {
			EpunchSFX.Play ();
			Epunched = false;	
		}
		if (Ekicked) {
			EkickSFX.Play ();
			Ekicked = false;
		}
		if (Eblocked) {
			EblockSFX.Play ();
			Eblocked = false;
		}
	}

	void SoundEffects ()
	{
		if (!trainingSFX && !CharacterSelect.fight) {
			SFXs.gameObject.SetActive (false);
		}
		//Turning fight scene SFX on and off
		else if (!fightSFX && CharacterSelect.fight) {
			SFXs.gameObject.SetActive (false);
		} else {
			SFXs.gameObject.SetActive (true);
		}
	}



	public void Flip ()
	{
		facingRight = !facingRight;
		renderer.flipX = !renderer.flipX;
	}
}
