using UnityEngine;
using System.Collections;

//The behavior of individual notes
public class NoteBehavior : MonoBehaviour {

	//Note's state: Inactive, Failed, or Success
	private int _state { get; set; }

	//X position, 0 through 3
	private int _position { get; set; }

	//Speed of notes
	private float _moveSpeed { get; set; }

	//The X distance between notes
	private float _distanceBetweenNotes = 0.09f;

	//The note collider bounds
	private float _targetTopBound { get; set; }
	private float _targetBottomBound { get; set; }

	//The lute window bound
	private float _windowTopBound { get; set; }

	//Whether the note is within the target bounds
	private bool _eligible = false;

	//Point value of the note
	private int _pointValue { get; set; }

	//Note scored event
	public delegate void NotePoints(int value);
	public event NotePoints OnNotePoints;

	//Note hit event
	public delegate void NoteHit();
	public event NoteHit OnNoteHit;

	//Note missed event
	public delegate void NoteMissed();
	public event NoteMissed OnNoteMissed;
	
	//Creation function used by NoteManager
	public static void Create(float speed, GameObject template, int position, float topBound, float bottomBound, float windowBound, int pointValue){

		//Create a new note from the template
		GameObject newNote = (GameObject) Instantiate (template);
		NoteBehavior newNoteBehavior = newNote.GetComponent<NoteBehavior> ();

		//Initialize speed and position
		newNoteBehavior._moveSpeed = speed;
		newNoteBehavior.SetInitialPosition (position);

		//Set bounds
		newNoteBehavior._targetTopBound = topBound;
		newNoteBehavior._targetBottomBound = bottomBound;
		newNoteBehavior._windowTopBound = windowBound;
		newNoteBehavior._pointValue = pointValue;

		GameObject scoreControllerObject = GameObject.FindWithTag("GameController");
		if(scoreControllerObject != null){
			ScoreController scoreController = scoreControllerObject.GetComponent<ScoreController>();
			scoreController.addNoteListener(newNoteBehavior);
		}

	}

	// Use this for initialization
	private void Start () {

		//Set initial state & color
		_state = Constants.NOTE_INITIAL;
		SetColorFromState ();
	}

	// Update is called once per frame
	private void Update () {
		// Moves the notes up the screen
		transform.position += new Vector3 (0, _moveSpeed);
		CheckOffScreen ();
		CheckEligibleOrFailed ();
	}

	private float GetTop(){
		return transform.position.y + (renderer.bounds.size.y / 2);
	}
	
	private void CheckOffScreen(){
		if(GetTop() > _windowTopBound)
		{
			Destroy (gameObject); 
		}
	}

	private void CheckEligibleOrFailed(){
		float top = GetTop ();
		if (top >= _targetBottomBound && top <= _targetTopBound) {
			if(!_eligible){
				_eligible = true;
				KeyListenerBehavior listener = KeyManager.GetKeyListenerByPosition(_position);
				listener.OnKeyPress += SetSuccess;
			}
		}

		if (top > _targetTopBound) {
			if(_eligible){
				_eligible = false;
				KeyListenerBehavior listener = KeyManager.GetKeyListenerByPosition(_position);
				listener.OnKeyPress -= SetSuccess;
				SetFailed();
			}
		}

	}

	//Sets the initial X position
	public void SetInitialPosition(int offsetPositionLeft){

		//The integer position, 0 through 3
		_position = offsetPositionLeft;

		//The X offset for each position
		float baseLeftOffset = renderer.bounds.size.x + _distanceBetweenNotes;

		//Calculate and set final X position
		float finalPositionLeft = transform.position.x + (offsetPositionLeft * baseLeftOffset);
		transform.position = new Vector3(finalPositionLeft, transform.position.y, 0);
	}

	//Helper functions

	//Sets note color based on state
	public void SetColorFromState(){
		switch (_state){
			case Constants.NOTE_INITIAL:
				GetComponent<SpriteRenderer>().color = Color.blue;
				break;
			case Constants.NOTE_SUCCESS:
				GetComponent<SpriteRenderer>().color = Color.green;
				break;
			case Constants.NOTE_FAILURE:
				GetComponent<SpriteRenderer>().color = Color.red;
				break;
			default:
				GetComponent<SpriteRenderer>().color = Color.blue;
				break;
		}
	}

	//Check if the note is outside of given bounds
	public bool ExceedsBounds(float top) {
		return (transform.position.y + (renderer.bounds.size.y / 2) > top);
	}

	//Set the state and update the color
	public void SetState(int state){
		_state = state;
		SetColorFromState ();
	}

	public void SetSuccess()
	{
		if(_state != Constants.NOTE_SUCCESS)
		{
			SetState(Constants.NOTE_SUCCESS);
			if(OnNoteHit != null)
			{
				OnNoteHit();
			}

			if(OnNotePoints != null)
			{
				OnNotePoints(_pointValue);
			}
		}
	}
	
	public void SetFailed()
	{
		if(_state != Constants.NOTE_SUCCESS){
			SetState (Constants.NOTE_FAILURE);
			if(OnNoteMissed != null){
				OnNoteMissed();
			}
		}
	}

	//State getter
	public int GetState()
	{
		return _state;
	}

}
