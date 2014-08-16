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

	//Creation function used by NoteManager
	public static NoteBehavior Create(float speed, GameObject template, int position){

		//Create a new note from the template
		GameObject newNote = (GameObject) Instantiate (template);
		NoteBehavior newNoteBehavior = newNote.GetComponent<NoteBehavior> ();

		//Initialize speed and position
		newNoteBehavior._moveSpeed = speed;
		newNoteBehavior.SetInitialPosition (position);

		return newNoteBehavior;
	}

	// Use this for initialization
	void Start () {

		//Set initial state & color
		_state = Constants.NOTE_INITIAL;
		SetColorFromState ();
	}

	// Update is called once per frame
	void Update () {
		// Moves the notes up the screen
		transform.position += new Vector3 (0, _moveSpeed);
	}
	
	//Destroy the note
	public void Destroy()
	{
		Destroy (gameObject);
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

	//State getter
	public int GetState()
	{
		return _state;
	}

	//Position getter
	public int GetPosition(){
		return _position;
	}

}
