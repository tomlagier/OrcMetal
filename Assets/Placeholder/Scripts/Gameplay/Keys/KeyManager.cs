using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//Manages all KeyListeners and provides helper functions for processing their input
public class KeyManager : MonoBehaviour {

	//Maps input buttons to note positions.
	public static string GetKeyFromPosition ( int position ){
		
		string key;
		switch (position) {
		case 0:
			key = "left1";
			break;
		case 1:
			key = "left2";
			break;
		case 2:
			key = "right1";
			break;
		case 3:
			key = "right2";
			break;
		default:
			key = "none";
			break;
		}
		
		return key;
	}
	
	//Returns the KeyListener at the specified position. 
	public static KeyListenerBehavior GetKeyListenerByPosition(int position)
	{
		List<KeyListenerBehavior> listeners = FindObjectsOfType<KeyListenerBehavior> ().ToList ();
		
		KeyListenerBehavior listener = listeners.First ( possibleListener => possibleListener.AtPosition (position));
		
		return listener;
	}
}
