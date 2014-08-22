using UnityEngine;
using System.Collections;

public class PlayerHealthManager : HealthManager {

	//Update player GUI text
	protected override void UpdateHealthText(){
		_healthText.text = "Your Health: " + _health;
	}

}
