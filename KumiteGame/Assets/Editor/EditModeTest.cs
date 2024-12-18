using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
public class eEditModeTest {

	//TEST FOR ARCADE SCENE

	[Test]
	public void ArcadeStartsAtEasy() {
		//This test will assert that the begining difficulty for arcade mode is easy;
		Assert.AreEqual("EASY", Arcade.Difficulty);
	}

	[Test] // This test will assert that both player and opponent health start at 100 in arcade mode;
	public void PlayerAndEnemyStartAtFullHealth(){
		Arcade arcade = new Arcade();
		arcade.easyDifficulty ();
		Assert.IsTrue (PlayerHealth.maxHealth==100);
		Assert.IsTrue (EnemyHealth.maxHealth==100);
	}

	[Test] // This test will assert that playershield starts at 70 and enemy shield starts at 100
	public void PlayerAndEnemyStartAtFullShield(){
		Arcade arcade = new Arcade();
		arcade.easyDifficulty ();
		Assert.IsTrue (PlayerHealth.maxShield==70);
		Assert.IsTrue (EnemyHealth.maxShield==100);
	}

	[Test] //In order to win a fight, number of round can never be split or else fights can end up in draws
	public void RoundCanNeverBeEvenNumber(){
		Arcade arcade = new Arcade();
		for(int i = 0; i <= 10; i++){ //We will run the change round function 10 times to ensure we never get an even number of rounds
			arcade.ChangeRounds2();
			Assert.IsFalse(Round.MaxRound %2==0);
		}
	}

	[Test] //If the opponent hits the player, the player's health bar should not be on 100.
	public void Player_Took_HIT(){
		PlayerHealth ph = new PlayerHealth ();
		PlayerHealth.hit = true;
		ph.TakeDamage (20);
		PlayerHealth.currentShield = 0;
		Assert.IsFalse (PlayerHealth.currentHealth==100); //Health is no longer 100;
		Assert.Less(PlayerHealth.currentHealth,100); //The health must be less than 100;
	}

	[Test] //If the opponent hits the player, the player's shield bar should not be on 100.
	public void Player_Shield_Takes_Hit(){
		PlayerHealth ph = new PlayerHealth ();
		PlayerHealth.hit = true;
		PlayerHealth.currentShield = 100; //Shield is at 100
		ph.TakeDamage (20); //Oppont hits shield with 20 power
		Assert.IsFalse (PlayerHealth.currentShield==100 ); //Sheild is no longer 100;
		Assert.Less(PlayerHealth.currentShield,100); //The shield must be less than 100;

	}

	[Test] //If the opponent hits the player, the player's shield bar should not be on 100.
	public void Can_Randomize_Map(){
		MapDifficulty md = new MapDifficulty ();
		HashSet<string> maps = new HashSet<string>();
		for(int i = 0; i <= 10; i++){
			md.randomStage ();
			maps.Add (MapDifficulty.MapChoice);
		}
		Assert.GreaterOrEqual (maps.Count, 4);
	}


	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator ArcadeEditModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
