using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//Manages the creation and destruction of notes, as well as note-position-based checks (whether they are in the target window) and other note related hooks.
public class NoteManager : MonoBehaviour {

	//Note creation speed and note travel speed.
	//TODO Map to sensible units rather than Unity units
	public float _noteCreationSpeed;
	public float _noteSpeed;

	//Prefab note template
	private GameObject _noteTemplate;

	//Collider for checking note targets
	private NoteCollider _noteCollider;

	//All notes
	private List<NoteBehavior> _notes = new List<NoteBehavior> ();

	//Time to next note spawning
	private float _nextNote = 1;

	// Use this for initialization
	void Start () {

		//Load in note prefab
		_noteTemplate = Resources.Load ("Prefabs/Note") as GameObject;
		_noteCollider = GetComponentInChildren<NoteCollider> ();
	}
	
	// Update is called once per frame
	void Update () {

		//Create a note at a random position every (_noteCreationSpeed) seconds
		//TODO : Load from a file
		if (Time.time > _nextNote) {
			_nextNote = _noteCreationSpeed + Time.time;
			addNote(Random.Range (0,4));
		}

		//Check for collisions - where the note is in or above the target
		checkCollisions ();

		//Remove notes that are outside the backboard
		removePastNotes ();
	}

	//Add a new note at the specified position
	void addNote(int position) {

		NoteBehavior newNote = NoteBehavior.Create (_noteSpeed, _noteTemplate, position);
		_notes.Add (newNote);
	}

	//Remove notes that have passed off the lute background
	void removePastNotes()
	{
		float topBound = (renderer.bounds.size.y / 2) + transform.position.y;

		//Destroy all notes off the background
		_notes
			.Where (note => note.ExceedsBounds(topBound))
			.ToList ()
			.ForEach( note => note.Destroy () );

		//Return all remaining notes to the notes list
		_notes = _notes
					.Where (note => !note.ExceedsBounds (topBound))
					.ToList ();
	

	}

	//Check whether a note should be flagged as a miss or a possible success
	void checkCollisions()
	{
		//Attach to the ScoreController
		//TODO Figure out a better way to do this
		ScoreController controller = GetComponentInParent<ScoreController> ();

		//Check the collision status for each note
		_notes.ForEach (delegate(NoteBehavior note) {
			switch (_noteCollider.CollisionStatus (note.transform.position.y)){
				case Constants.Collision.ABOVE_TARGET:
					controller.MightMissNote(note);
					break;
				case Constants.Collision.IN_TARGET:
					controller.MightHitNote(note);
					break;
				default:
					break;
			}
		});
	}
}
