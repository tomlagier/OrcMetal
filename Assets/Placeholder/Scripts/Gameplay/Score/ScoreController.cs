using UnityEngine;
using System.Collections;

/*
 * The ScoreController manages input and its relations to score, morale, and health. 
 * Listens to the NoteManager and KeyManager, and passes input to the ScoreManager, HealthManager, and MoraleManager
 */
public class ScoreController : MonoBehaviour {

	private ScoreManager _scoreManager;
	private ComboManager _comboManager;

	// Use this for initialization
	void Start () {
		_scoreManager = GetComponent<ScoreManager> ();
		_comboManager = GetComponent<ComboManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addNoteListener(NoteBehavior target){
		target.OnNotePoints += _scoreManager.AddScore;
		target.OnNoteHit += _comboManager.AddCombo;
		target.OnNoteMissed += _comboManager.BreakCombo;
	}
}
