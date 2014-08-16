using UnityEngine;
using System.Collections;

//Contains input helper functions for translating game input into functions.
public static class KeyHelper {

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

}
