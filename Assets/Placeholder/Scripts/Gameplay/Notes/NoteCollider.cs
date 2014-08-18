using UnityEngine;
using System.Collections;

//The NoteCollider is used to determine whether a given note is within or above the target window.
public class NoteCollider : MonoBehaviour {

	//The width of the target window
	public float _windowWidth = 1f;

	public float _targetTop;
	public float _targetBottom;

	//Offset from the top of the note target window gameobject to the center of the target window. 
	private float _targetOffset = 0.2f;

	// Use this for initialization
	void Start () {

		//Initializes the game positions of the target window.
		_targetTop = transform.position.y + (renderer.bounds.size.y / 2) - _targetOffset + (_windowWidth / 2);
		_targetBottom = transform.position.y + (renderer.bounds.size.y / 2) - _targetOffset - (_windowWidth / 2);
	}

	public float GetTop()
	{
		return _targetTop;
	}

	public float GetBottom()
	{
		return _targetBottom;
	}
}
