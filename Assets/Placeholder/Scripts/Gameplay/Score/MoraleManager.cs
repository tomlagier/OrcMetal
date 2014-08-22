using UnityEngine;
using System.Collections;

public class MoraleManager : MonoBehaviour {

	public int _startValue = 50;
	public int _maxValue = 100;
	public int _decayRate = 1;
	public int _decayValue = 3;
	public int _noteHit = 5;
	public int _noteMiss = 3;

	public int _lowMoraleThreshhold = 10;
	public int _highMoraleThreshhold = 90;

	public GUIText moraleText;

	private float _startDecayDelay = 1;
	private float _nextDecay;
	private int _current;

	// Use this for initialization
	void Start () {

		//Initialize starting morale and next decay timer
		_current = _startValue;
		_nextDecay = _startDecayDelay + Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		//Check if it is time to decay morale
		CheckDecay ();
	}

	//If it is time to reduce morale from decay, update morale and GUI text 
	private void CheckDecay(){
		if (Time.time > _nextDecay) {
			_nextDecay = Time.time + _decayRate;
			_current -= _decayValue;
			UpdateMoraleText();
		}
	}

	//Increase morale on note hit, to maximum of maxValue, update morale text, and return whether we are currently in high morale threshold
	public bool NoteHit(){
		if (_current < (_maxValue - _noteHit)) {
			_current += _noteHit;
		} else {
			_current = _maxValue;
		}

		UpdateMoraleText();

		if (_current >= _highMoraleThreshhold) {
			return true;
		}

		return false;
	}

	//Decrease morale to a minimum of 0, update GUI text, and return whether we are in low morale threshold
	public bool NoteMiss(){
		if(_current > _noteMiss){
			_current -= _noteMiss;
		} else {
			_current = 0;
		}

		UpdateMoraleText();

		if (_current <= _lowMoraleThreshhold) {
			return true;
		}

		return false;
	}

	//Getter for current morale
	public int GetMorale(){
		return _current;
	}

	//Update morale text
	private void UpdateMoraleText()
	{
		moraleText.text = "Morale: " + _current; 
	}
}
