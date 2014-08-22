using UnityEngine;
using System.Collections;

public class ScoreControllerTestCase : UUnitTestCase {

	private ScoreController _scoreController;

	// Use this for initialization
	protected override void SetUp() {
		GameObject scoreControllerObject = GameObject.FindWithTag("GameController");
		if(scoreControllerObject != null){
			_scoreController = scoreControllerObject.GetComponent<ScoreController>();
		}
	}
	
	[UUnitTest]
	public void ShouldDoSomething() {

	}
	
	protected override void TearDown() {
	}
}
