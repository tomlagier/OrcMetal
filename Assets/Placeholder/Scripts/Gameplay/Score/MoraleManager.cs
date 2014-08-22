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
		_current = _startValue;
		_nextDecay = _startDecayDelay + Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		CheckDecay ();
	}

	private void CheckDecay(){
		if (Time.time > _nextDecay) {
			_nextDecay = Time.time + _decayRate;
			_current -= _decayValue;
			UpdateMoraleText();
		}
	}

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

	public int GetMorale(){
		return _current;
	}

	private void UpdateMoraleText()
	{
		moraleText.text = "Morale: " + _current; 
	}
}
