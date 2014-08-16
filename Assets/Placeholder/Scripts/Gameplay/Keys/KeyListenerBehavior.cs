using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//The behavior of key listeners, keeps their state and associated key
public class KeyListenerBehavior : MonoBehaviour {

	//The integer X position of the key
	public int _position;

	//Length of time a key remains "active" while being held down
	public float _cooldown = 0.2f;

	//State - active, inactive, or was active
	private int _state { get; set; }

	//The associated key for this listener
	private string _key;

	//Time since the key was last pressed
	private float _lastPress;

	// Use this for initialization
	void Start () {

		//Initialize to inactive state, and set up the internal associated key
		_state = Constants.KEY_INACTIVE;
		_key = KeyHelper.GetKeyFromPosition (_position);
	}
	
	// Update is called once per frame
	void Update () {

		//Triggered once per keypress, set time to prevent holding down a key to hit all notes.
		if(Input.GetButtonDown (_key)){
			_lastPress = Time.time;
		}

		//Set key color based on state
		if(Input.GetButton (_key)){
			if (Time.time < _lastPress + _cooldown)	{
				_state = Constants.KEY_ACTIVE;
			} else {
				_state = Constants.KEY_WASACTIVE;
			}
		} else {
			_state = Constants.KEY_INACTIVE;
		}

		SetColorFromState();
	}
	

	//Sets the color from the state
	private void SetColorFromState()
	{
		switch (_state){
			case Constants.KEY_INACTIVE:
				GetComponent<SpriteRenderer>().color = Color.white;
				break;
			case Constants.KEY_WASACTIVE:
				GetComponent<SpriteRenderer>().color = Constants.COLOR_DARKGREEN;
				break;
			case Constants.KEY_ACTIVE:
				GetComponent<SpriteRenderer>().color = Color.green;
				break;
			default:
				GetComponent<SpriteRenderer>().color = Color.white;
				break;
		}
	}

	//Check if the listener is at a given position (to enable position-based lookup)
	public bool AtPosition(int position){
		return (position == _position);
	}

	//Check if the listener is currently active
	public bool IsActive ()
	{
		return _state == Constants.KEY_ACTIVE;
	}
}
