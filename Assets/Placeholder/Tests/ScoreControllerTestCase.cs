using UnityEngine;
using System.Collections;

public class ScoreControllerTestCase : UUnitTestCase {

	private ScoreManager _scoreManager;
	private ComboManager _comboManager;
	private MoraleManager _moraleManager;
	private PlayerHealthManager _playerHealthManager;
	private EnemyHealthManager _enemyHealthManager;
	private ScoreController _scoreController;
	private GameObject _gameControllerObject;

	// Use this for initialization
	protected override void SetUp() {
		_gameControllerObject = GameObject.FindWithTag("GameController");
		if(_gameControllerObject != null){
			_scoreController = _gameControllerObject.GetComponent<ScoreController>();
		}

		_scoreManager = _gameControllerObject.GetComponent<ScoreManager> ();
		_comboManager = _gameControllerObject.GetComponent<ComboManager> ();
		_moraleManager = _gameControllerObject.GetComponent<MoraleManager> ();
		_playerHealthManager = _gameControllerObject.GetComponent<PlayerHealthManager> ();
		_enemyHealthManager = _gameControllerObject.GetComponent<EnemyHealthManager> ();	
	}
	
	[UUnitTest]
	public void NoteHitShouldUpdateScore() {
		int initialScore = _scoreManager.GetScore ();
		_scoreController.NoteHit (100);
		UUnitAssert.Equals (_scoreManager.GetScore(), initialScore + 100, "Score not updated correctly");
	}

	[UUnitTest]
	public void NoteHitShouldUpdateCombo() {
		int initialCombo = _comboManager.GetCombo ();
		_scoreController.NoteHit (1);
		UUnitAssert.Equals (_comboManager.GetCombo (), initialCombo + 1, "Combo not updated correctly");
	}

	[UUnitTest]
	public void NoteHitShouldUpdateMorale(){
		int initialMorale = _moraleManager.GetMorale ();
		_scoreController.NoteHit (1);
		UUnitAssert.Equals (_moraleManager.GetMorale (), initialMorale + _moraleManager._noteHit, "Morale not increased correctly");
	}

	[UUnitTest]
	public void NoteMissShouldNotUpdateScore(){
		int initialScore = _scoreManager.GetScore ();
		_scoreController.NoteMissed ();
		UUnitAssert.Equals (_scoreManager.GetScore (), initialScore, "Score updated on miss");
	}

	[UUnitTest]
	public void NoteMissShouldBreakCombo(){
		_scoreController.NoteHit (1);
		_scoreController.NoteMissed ();
		UUnitAssert.Equals (_comboManager.GetCombo (), 0, "Combo not broken after miss");
	}

	[UUnitTest]
	public void NoteMissShouldReduceMorale(){
		int initialMorale = 50;
		_moraleManager.SetMorale (initialMorale);
		_scoreController.NoteMissed ();
		UUnitAssert.Equals (_moraleManager.GetMorale (), initialMorale - _moraleManager._noteMiss, "Morale not decreased by right amount after note miss");

	}

	[UUnitTest]
	public void HealthObjectShouldNotBeAbleToAttackTwice(){
		_enemyHealthManager.Attack ();
		UUnitAssert.False (_enemyHealthManager.CanAttack (), "Enemy should not be able to attack after attacking");
	}

	[UUnitTest]
	public void MoraleShouldStayWithinBounds(){
		_moraleManager.SetMorale (1);
		_scoreController.NoteMissed ();
		UUnitAssert.Equals (_moraleManager.GetMorale (), 0, "Morale should not go below 0");

		_moraleManager.SetMorale (_moraleManager._maxValue - 1);
		_scoreController.NoteHit (1);
		UUnitAssert.Equals (_moraleManager.GetMorale (), _moraleManager._maxValue, "Morale should not go above max");

	}

	[UUnitTest]
	public void HealthShouldDecreaseWhenDamageTaken(){
		int damageAmount = 10;
		int initialHealth = _playerHealthManager.GetHealth ();
		_playerHealthManager.TakeDamage (damageAmount);
		UUnitAssert.Equals (_playerHealthManager.GetHealth (), initialHealth - damageAmount, "Health not decreased correctly");
	}

	[UUnitTest]
	public void HealthShouldNotGoBelowZero(){
		_playerHealthManager.SetHealth (1);
		_playerHealthManager.TakeDamage (2);
		UUnitAssert.Equals (_playerHealthManager.GetHealth (), 0, "Health should not go below zero");
	}

	[UUnitTest]
	public void OnDeathShouldFireAtZeroHealth(){
		bool fired = false;

		_enemyHealthManager.SetHealth (1);
		_enemyHealthManager.BindDeathEvent (delegate(){ fired = true; });
		_enemyHealthManager.TakeDamage (2);
		UUnitAssert.True (fired, "Death event should fire");
	}

	protected override void TearDown() {
	}
}
