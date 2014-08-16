using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//Manages all KeyListeners and provides helper functions for processing their input
public class KeyManager : MonoBehaviour {

	//Returns the KeyListener at the specified position. 
	public KeyListenerBehavior GetKeyListenerByPosition(int position)
	{
		List<KeyListenerBehavior> listeners = GetComponentsInChildren<KeyListenerBehavior> ().ToList ();

		KeyListenerBehavior listener = listeners.First ( possibleListener => possibleListener.AtPosition (position));

		return listener;
	}
}
