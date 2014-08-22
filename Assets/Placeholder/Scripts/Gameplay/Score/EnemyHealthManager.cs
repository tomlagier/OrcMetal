using UnityEngine;
using System.Collections;

public class EnemyHealthManager : HealthManager {

	//Update enemy health GUI text
	protected override void UpdateHealthText(){
		_healthText.text = "Enemy Health: " + _health;
	}

}
