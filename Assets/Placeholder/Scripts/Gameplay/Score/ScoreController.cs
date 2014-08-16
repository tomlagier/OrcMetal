using UnityEngine;
using System.Collections;

/*
 * The ScoreController manages input and its relations to score, morale, and health. It calls the KeyListeners to determine whether notes are being hit.
 * It is responsible for passing input from game components to the scoring mechanics of the game.
 * Listens to the NoteManager and KeyManager, and passes input to the ScoreManager, HealthManager, and MoraleManager
 */
public class ScoreController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Called if a note is above the target window. Checks if the note was already hit, and fails it if it was not hit. 
	public void MightMissNote(NoteBehavior note)
	{
		if (note.GetState() != Constants.NOTE_SUCCESS)
		{
			note.SetState (Constants.NOTE_FAILURE);
		}
	}

	//Called if a note is within the window. If the associated key is pressed while the window is within the target, flags the note as successful.
	public void MightHitNote(NoteBehavior note)
	{
		//Get the associated listener with a position
		KeyManager manager = GetComponentInChildren<KeyManager> ();
		KeyListenerBehavior listener = manager.GetKeyListenerByPosition (note.GetPosition ());

		if(listener.IsActive ())
		{
			note.SetState (Constants.NOTE_SUCCESS);
		}
	}
}
