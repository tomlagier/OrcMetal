using UnityEngine;
using System.Collections;

/*
 * The ScoreController manages input and its relations to score, morale, and health. 
 * Listens to the NoteManager and KeyManager, and passes input to the ScoreManager, HealthManager, and MoraleManager
 */
public class ScoreController : MonoBehaviour {

	private ScoreManager _scoreManager;
	private ComboManager _comboManager;
	private MoraleManager _moraleManager;
	private PlayerHealthManager _playerHealthManager;
	private EnemyHealthManager _enemyHealthManager;

	// Use this for initialization
	void Start () {
		_scoreManager = GetComponent<ScoreManager> ();
		_comboManager = GetComponent<ComboManager> ();
		_moraleManager = GetComponent<MoraleManager> ();
		_playerHealthManager = GetComponent<PlayerHealthManager> ();
		_enemyHealthManager = GetComponent<EnemyHealthManager> ();

		_playerHealthManager.BindDeathEvent (Lose);
		_enemyHealthManager.BindDeathEvent (Win);
	}

	//Event handler for game win
	public void Win(){
		Debug.Log ("Won!");
	}

	//Event handler for game lost
	public void Lose(){
		Debug.Log ("Lost!");
	}

	//Event handler when a note is hit
	public void NoteHit(int value){

		//Add points and increment combo
		_scoreManager.AddScore (value);
		_comboManager.AddCombo ();

		//Update morale. If morale above the attack threshold, attempt to execute player attack and resolve damage
		if (_moraleManager.NoteHit ()) {
			if(_playerHealthManager.CanAttack ()){
				_enemyHealthManager.TakeDamage(_playerHealthManager.Attack ());
			}
		}
	}

	//Event handler when a note is missed
	public void NoteMissed(){

		//Break the combo
		_comboManager.BreakCombo ();

		//Update morale. If morale below the enemy attack threshold, attempt to execute enemy attack and resolve damage
		if (_moraleManager.NoteMiss ()) {
			if(_enemyHealthManager.CanAttack ()){
				_playerHealthManager.TakeDamage(_enemyHealthManager.Attack ());
			}
		}
	}
}
