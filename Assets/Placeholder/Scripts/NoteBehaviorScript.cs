using UnityEngine;
using System.Collections;

public class NoteBehaviorScript : MonoBehaviour {

	//The time it takes for the note to move to the top of the screen
	public float moveSpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.up * moveSpeed;
	}
}