using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	//Score text and score
	public GUIText _scoreText;
	public int _score;


	// Use this for initialization
	void Start () {
		_score = 0;
		UpdateScoreText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateScoreText()
	{
		_scoreText.text = "Score: " + _score; 
	}

	public void AddScore (int newScoreValue)
	{
		_score += newScoreValue;
		UpdateScoreText ();
	}
}
