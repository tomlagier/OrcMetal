using UnityEngine;
using System.Collections;

public class Scene1TestCaseRunner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UUnitTestSuite testSuite = new UUnitTestSuite ();
		testSuite.AddAll (typeof(ScoreControllerTestCase));
		UUnitTestResult result = testSuite.Run (null);
		Debug.Log (result.Summary ());
	}
}
