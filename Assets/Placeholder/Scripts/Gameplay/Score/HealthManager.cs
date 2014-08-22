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
		_health = _startHealth;
		_nextAttack = 1;
		UpdateHealthText ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckDeath ();

		if (Time.time > _nextAttack) {
			_canAttack = true;
		}
	}

	public bool CanAttack(){
		return _canAttack;
	}

	public int Attack(){
		_nextAttack = Time.time + _attackCooldown;
		_canAttack = false;
		return _damagePerHit;
	}

	public void TakeDamage(int damage){
		_health -= damage;

		UpdateHealthText ();

		CheckDeath ();
	}

	private void CheckDeath(){
		if (_health <= 0) {
			if (OnDeath != null){
				OnDeath();
			}
		}
	}

	public void BindDeathEvent(DeathEvent deathEvent){
		OnDeath += deathEvent;
	}

	public void End(){
		OnDeath = null;
	}

	protected abstract void UpdateHealthText();
}
