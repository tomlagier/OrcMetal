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

	//ScoreManager reference
	private ScoreController _scoreController;

	//KeyListener refrence
	private KeyListenerBehavior _listener;

	//Creation function to spawn a new note, allowing easy modification of attributes in NoteManager.
	//Defaults could be managed in the prefab, but this I find easier
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

		//Pass a reference to ScoreController so that points can be updated
		GameObject scoreControllerObject = GameObject.FindWithTag("GameController");
		if(scoreControllerObject != null){
			newNoteBehavior._scoreController = scoreControllerObject.GetComponent<ScoreController>();
		}

		newNoteBehavior._listener = KeyManager.GetKeyListenerByPosition (position);

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

		//Remove note if offscreen
		CheckOffScreen ();

		//Check if note is within target window && bind handler
		CheckEligible ();

		//Remove handler and fail note if not hit within window
		CheckFailed ();
	}

	//Helper to get the top of the note
	private float GetTop(){
		return transform.position.y + (renderer.bounds.size.y / 2);
	}

	//Destroy note if it falls offscreen
	private void CheckOffScreen(){
		if(GetTop() > _windowTopBound)
		{
			Destroy (gameObject); 
		}
	}

	//Check if the note is eligible to be hit
	private void CheckEligible(){
		float top = GetTop ();

		//Within the target window
		if (top >= _targetBottomBound && top <= _targetTopBound) {
			if(!_eligible){
				_eligible = true;

				//If note is eligible, bind an event to the proper keylistener's OnKeyPress event
				_listener.OnKeyPress += SetSuccess;
			}
		}
	}

	//Check if the note was missed
	private void CheckFailed(){
		float top = GetTop ();

		//Above the target window
		if (top > _targetTopBound) {
			if(_eligible){
				_eligible = false;

				//Remove the listener that was set in CheckEligible
				_listener.OnKeyPress -= SetSuccess;

				//Fail the note
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

	//Set the state and update the color
	public void SetState(int state){
		_state = state;
		SetColorFromState ();
	}

	//Set the note to successful and trigger points if this is a state update
	public void SetSuccess()
	{
		if(_state != Constants.NOTE_SUCCESS)
		{
			SetState(Constants.NOTE_SUCCESS);
			_scoreController.NoteHit(_pointValue);
		}
	}

	//Set the note to failed and break combo if this is a state update
	public void SetFailed()
	{
		if(_state != Constants.NOTE_SUCCESS){
			SetState (Constants.NOTE_FAILURE);
			_scoreController.NoteMissed();
		}
	}
}
