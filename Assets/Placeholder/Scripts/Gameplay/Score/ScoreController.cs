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
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Win(){
		Debug.Log ("Won!");
	}

	public void Lose(){
		Debug.Log ("Lost!");
	}

	public void NoteHit(int value){
		_scoreManager.AddScore (value);
		_comboManager.AddCombo ();

		if (_moraleManager.NoteHit ()) {
			if(_playerHealthManager.CanAttack ()){
				_enemyHealthManager.TakeDamage(_playerHealthManager.Attack ());
			}
		}
	}

	public void NoteMissed(){
		_comboManager.BreakCombo ();

		if (_moraleManager.NoteMiss ()) {
			if(_enemyHealthManager.CanAttack ()){
				_playerHealthManager.TakeDamage(_enemyHealthManager.Attack ());
			}
		}
	}
}
