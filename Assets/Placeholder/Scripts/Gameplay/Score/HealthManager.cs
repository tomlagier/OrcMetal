using UnityEngine;
using System.Collections;

public abstract class HealthManager : MonoBehaviour {

	protected int _health;

	public int _startHealth = 100;
	public int _damagePerHit = 10;
	public int _attackCooldown = 5;

	public delegate void DeathEvent();
	public event DeathEvent OnDeath;

	public GUIText _healthText;

	protected float _nextAttack;
	protected bool _canAttack = false;

	// Use this for initialization
	protected void Start () {

		//Initialize health, next attack , and GUItext
		_health = _startHealth;
		_nextAttack = 1;
		UpdateHealthText ();
	}
	
	// Update is called once per frame
	void Update () {

		//Check if this object has died
		CheckDeath ();

		//Check if this object can attack
		if (Time.time > _nextAttack) {
			_canAttack = true;
		}
	}

	//Check if object can attack
	public bool CanAttack(){
		return _canAttack;
	}

	//Set time until next attack and return damage done by object
	public int Attack(){
		_nextAttack = Time.time + _attackCooldown;
		_canAttack = false;
		return _damagePerHit;
	}

	//Reduce health by damage amount and update text, then check if object died
	public void TakeDamage(int damage){
		_health -= damage;

		UpdateHealthText ();

		CheckDeath ();
	}

	//If object is dead, call death callback
	private void CheckDeath(){
		if (_health <= 0) {
			if (OnDeath != null){
				OnDeath();
			}
		}
	}

	//Add death callback
	public void BindDeathEvent(DeathEvent deathEvent){
		OnDeath += deathEvent;
	}

	//Remove death handlers from object
	public void End(){
		OnDeath = null;
	}

	//Expose update health text method for children
	protected abstract void UpdateHealthText();
}
