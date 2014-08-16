using UnityEngine;
using System.Collections;

//All constants used in the demo project
public static class Constants {
	//Keys
	public const int KEY_INACTIVE = 0;
	public const int KEY_ACTIVE = 1;
	public const int KEY_WASACTIVE = 2;

	//Notes
	public const int NOTE_INITIAL = 0;
	public const int NOTE_SUCCESS = 1;
	public const int NOTE_FAILURE = 2;

	//Key Codes

	//Collision status
	public enum Collision { IN_TARGET = 0, ABOVE_TARGET = 1, BELOW_TARGET = 2 };

	//Colors
	public static Color COLOR_DARKGREEN {
		get {
			return new Color32(6, 148, 27, 255);
		}
	}
}