using UnityEngine;
using System.Collections;

//The NoteCollider is used to determine whether a given note is within or above the target window.
public class NoteCollider : MonoBehaviour {

	//The width of the target window
	public float _windowWidth = 1f;

	public float _targetTop;
	public float _targetBottom;

	//Offset from the top of the note target window gameobject to the center of the target window. 
	private float _targetOffset = 0.4f;

	// Use this for initialization
	void Start () {

		//Initializes the game positions of the target window.
		_targetTop = transform.position.y + (renderer.bounds.size.y / 2) - _targetOffset + (_windowWidth / 2);
		_targetBottom = transform.position.y + (renderer.bounds.size.y / 2) - _targetOffset - (_windowWidth / 2);
	}

	//Returns a position status based on the target window and a given position - above the target, in the target, or below target.
	public Constants.Collision CollisionStatus(float position)
	{
		Constants.Collision status;

		if (position > _targetTop) {
			status = Constants.Collision.ABOVE_TARGET;
		} else if (position <= _targetTop && position >= _targetBottom ){
			status = Constants.Collision.IN_TARGET;
		} else {
			status = Constants.Collision.BELOW_TARGET;
		}

		return status;
	}
}
