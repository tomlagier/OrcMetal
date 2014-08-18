using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	//Score text and score
	public GUIText scoreText;
	public int score;


	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScoreText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateScoreText()
	{
		scoreText.text = "Score: " + score; 
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScoreText ();
	}


}
