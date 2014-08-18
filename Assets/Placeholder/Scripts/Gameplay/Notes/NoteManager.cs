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
	public int _noteValue = 100;

	//Prefab note template
	private GameObject _noteTemplate;

	//Collider for checking note targets
	private NoteCollider _noteCollider;

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
	}

	//Add a new note at the specified position
	void addNote(int position) {
		float windowTop = (renderer.bounds.size.y / 2) + transform.position.y;
		float targetTopBounds = _noteCollider.GetTop ();
		float targetBottomBounds = _noteCollider.GetBottom ();

		NoteBehavior.Create (_noteSpeed, _noteTemplate, position, targetTopBounds, targetBottomBounds, windowTop, _noteValue);
	}

}
