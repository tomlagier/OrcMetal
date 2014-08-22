using UnityEngine;
using System.Collections;

public class ComboManager : MonoBehaviour {

	public GUIText _comboText;

	private int _combo = 0;

	// Use this for initialization
	void Start () {
		//Update text on init
		UpdateComboText ();
	}

	//Increment combo and update text	
	public void AddCombo()
	{
		_combo++;
		UpdateComboText ();
	}

	//Reset combo and update text
	public void BreakCombo()
	{
		_combo = 0;
		UpdateComboText ();
	}

	//Getter for combo
	public int GetCombo(){
		return _combo;
	}

	//Update the text
	private void UpdateComboText(){
		_comboText.text = "Combo :" + _combo;
	}
}
