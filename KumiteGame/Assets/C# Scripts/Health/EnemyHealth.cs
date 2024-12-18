using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

	public class EnemyHealth : MonoBehaviour
	{
		static EnemyHealth instance;
	public static EnemyHealth Instance { get { return instance; } }

		public static int maxHealth;
		public static int maxShield;
   		public static bool unlimited = false;
		public static float currentHealth = 100;
		public float invulnerabilityTime = 0.5f;
		public static bool enemyhit = false;
		public static bool resetHealth = false;
	    public static float currentShield = 0;
		public static int hitsTaken;
		System.Random rnd = new System.Random();
		int num;
		int lostNum;
		Opponent opponent;
		public float regenShieldTimerMax = 1.0f;
		public static void UpdateHealth(){
		currentHealth = maxHealth;
		currentShield = maxShield;
		HealthBar.UpdateBar( "Health", currentHealth, maxHealth );
		HealthBar.UpdateBar( "Shield", currentShield, maxShield );
	}



		void Awake ()
		{
			if( instance != null )
				instance = GetComponent<EnemyHealth>();
		}

		void Start ()
	{
		try{
			hitsTaken = 0;
			currentHealth = maxHealth;
			currentShield = maxShield;
			opponent = SelectFrom.CPU.GetComponent<Opponent>();

			HealthBar.UpdateBar( "EnemyHealth", currentHealth, maxHealth );
			HealthBar.UpdateBar( "EnemyShield", currentShield, maxShield );
		}
		catch(NullReferenceException ex){
			SceneManager.LoadScene (0);
		}
	}

		void Update ()
		{
		if (!unlimited || CharacterSelect.fight || MenuScript.ArcadeMode) {
			TakeDamage (5);
		}
		}
		
	public void TakeDamage ( int damage )
	{

		if (enemyhit == true) {
				if (currentShield > 0) {
				currentShield -= damage;

				if (currentShield < 0) {
					currentHealth -= currentShield * -1;
					currentShield = 0;
				}
			}
			else
				currentHealth -= damage;
			Arcade.ArcadeScore += 10;
			Arcade.ShieldBreakerCheck += 10;
			GameMode.Added = true;
			if (currentHealth <= 0) {
				currentHealth = 0;
			}
			GenerateRandomNum ();
			if(hitsTaken >=4){
				hitsTaken = 0;
			}
			if(num != lostNum){
			if (hitsTaken == num) {
				opponent.animator.SetTrigger ("hurt");
					PainSounds.PlayCPUPain = true;

				hitsTaken = 0;
			} else if (hitsTaken == lostNum) {
		    	opponent.animator.SetTrigger ("lost");
					PainSounds.PlayCPUPain = true;

				hitsTaken = 0;
			}
			}
			HealthBar.UpdateBar ("EnemyHealth", currentHealth, maxHealth);
			HealthBar.UpdateBar ("EnemyShield", currentShield, maxShield);
		}
		enemyhit = false;
	}

	void GenerateRandomNum(){
		num = rnd.Next (0,3);
		lostNum = rnd.Next (0,5);
	}

	}


