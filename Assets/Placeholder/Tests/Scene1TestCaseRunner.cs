using UnityEngine;
using System.Collections;

public class Scene1TestCaseRunner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("RunTests", 2);
	}

	void RunTests(){
		UUnitTestSuite testSuite = new UUnitTestSuite ();
		testSuite.AddAll (typeof(ScoreControllerTestCase));
		UUnitTestResult result = testSuite.Run ();
		Debug.Log (result.Summary ());
	}
}
