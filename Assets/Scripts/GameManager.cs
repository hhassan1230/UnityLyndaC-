using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	private float _timeRemaining;

	public float TimeRemaining 
	{
		get { return _timeRemaining; }
		set { _timeRemaining = value; }
	}

	private int _numCoins;

	public int NumCoins {
		get { return _numCoins; }
		set { _numCoins = value; }
	}

	private float _playerHealth;

	public float PlayerHealth 
	{
		get { return _playerHealth; }
		set { _playerHealth = value; }
	}

	private float maxHealth = 3;

	private float maxTime = 5 * 60; // In seconds.


	private bool isInvulnerable = false;

	void OnEnable()
	{
		DamagePlayerEvent.OnDamagePlayer += DecrementPlayerHealth;

	}

	void OnDisable()
	{
		DamagePlayerEvent.OnDamagePlayer -= DecrementPlayerHealth;

	}

	// Use this for initialization
	void Start () {
		TimeRemaining = maxTime;
		PlayerHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;

		if(TimeRemaining <= 0)
		{
			Restart ();
		}
	}

	private void DecrementPlayerHealth(GameObject player)
	{
		if (isInvulnerable) 
		{
			return;
		}

		StartCoroutine (InvulnerableDelay ());

		PlayerHealth--;

		if (PlayerHealth <= 0) 
		{
			Restart ();

		}
	}

	private void Restart()
	{
		Application.LoadLevel (Application.loadedLevel);
		TimeRemaining = maxTime;
		PlayerHealth = maxHealth;
	}

	private IEnumerator InvulnerableDelay()
	{
		isInvulnerable = true;
		yield return new WaitForSeconds (1.0f);
		isInvulnerable = false;
	}

	public float GetPlayerHealthPercentage()
	{
		return PlayerHealth / (float)maxHealth;
	}
}
