using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

	public class PlayerHealth : MonoBehaviour
	{
		static PlayerHealth instance;
		public static PlayerHealth Instance { get { return instance; } }
		public static int maxHealth;
		public static int maxShield;
		public static bool unlimited = false;
		public static float currentHealth = 100;
		public float invulnerabilityTime = 0.5f;
		public static float ArcadeHit = 5; 
		public static bool hit = false;
	    public static float currentShield = 0;
		Player player;
		public float regenShieldTimerMax = 1.0f;
	 	public static int hitsTaken;
		System.Random rnd = new System.Random();
		int num;
		int lostNum;

	public static  void UpdateHealth(){
		currentHealth = maxHealth;
		currentShield = maxShield;
		HealthBar.UpdateBar( "Health", currentHealth, maxHealth );
		HealthBar.UpdateBar( "Shield", currentShield, maxShield );
		}
		

		void Awake ()
		{
			if( instance != null )
				instance = GetComponent<PlayerHealth>();
		}

		void Start ()
		{
		try{
			hitsTaken = 0;
			currentHealth = maxHealth;
			currentShield = maxShield;
		player = SelectFrom.Player1.GetComponent<Player> ();
			HealthBar.UpdateBar( "Health", currentHealth, maxHealth );
			HealthBar.UpdateBar( "Shield", currentShield, maxShield );
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
		}

		void Update ()
		{
		if (!unlimited && CharacterSelect.fight) {
			if (MapDifficulty.Difficulty == "TRUEWARRIOR") {
				TakeDamage (9);
			} else if (MapDifficulty.Difficulty == "HARD") {
				TakeDamage (6);
			} else {
				TakeDamage (5);
			}
		}

		if (!unlimited && MenuScript.ArcadeMode) {
			TakeDamage (ArcadeHit);

			}
		}


		public void TakeDamage ( float damage )
	{
		if (hit == true) {
				if (currentShield > 0) {
				currentShield -= damage;

				if (currentShield < 0) {
					currentHealth -= currentShield * -1;
					currentShield = 0;
				}
			}
			else
				currentHealth -= damage;
			Arcade.ArcadeScore -= 5;
			Arcade.ShieldBreakerCheck -= 5;
			GameMode.Removed = true;
			if (currentHealth <= 0) {
				currentHealth = 0;
			}
			GenerateRandomNum ();
			if(hitsTaken >=4){
				hitsTaken = 0;
			}
			if(num != lostNum){
				if (hitsTaken == num) {
					Player.hitFromEnemy = "hurt";
					Player.tookHardHit = true;
					hitsTaken = 0;
				} else if (hitsTaken == lostNum) {
					Player.hitFromEnemy ="lost";
					Player.tookHardHit = true;


					hitsTaken = 0;
				}
			}
			HealthBar.UpdateBar ("Health", currentHealth, maxHealth);
			HealthBar.UpdateBar ("Shield", currentShield, maxShield);
		}
		hit = false;
	}

	void GenerateRandomNum(){
		num = rnd.Next (0,2);
		lostNum = rnd.Next (0,4);
	}

	}


