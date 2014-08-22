using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	//Score text and score
	public GUIText _scoreText;
	public int _score;


	// Use this for initialization
	void Start () {

		//Initialize score
		_score = 0;
		UpdateScoreText ();
	}

	//Update score GUI text	
	void UpdateScoreText()
	{
		_scoreText.text = "Score: " + _score; 
	}

	//Increment score and update text
	public void AddScore (int newScoreValue)
	{
		_score += newScoreValue;
		UpdateScoreText ();
	}

	//Getter for score
	public int GetScore(){
		return _score;
	}
}
