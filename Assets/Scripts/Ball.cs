using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour{

	public SoundFX SFX;
	bool bound = true;
	Vector2 direction = Vector2.zero;
	float spin = 0.0f;
	float speed = 0.0f;
	[SerializeField] float maxdeflection = 5;

	public void spawn (Vector3 spawnPosition) {
		Debug.Log("spawnPosition: " + spawnPosition);
		gameObject.SetActive(true);
		transform.position = new Vector3(
			spawnPosition.x,
			spawnPosition.y + 1f,
			0f
		);
		direction = Vector2.up;
		speed = 20f;
		bound = true;
	}

	public void launch (float launchSpin) {
		Debug.Log("launchSpin: " + launchSpin);
		// spin = launchSpin;
		spin = launchSpin;
		bound = false;
		SFX.GetComponent<SoundFX>().sfxBounce(0);
		SFX.GetComponent<SoundFX>().setCanRunAudio(true);
	}

	public void rotate () {
		float deltaSpin = spin * Time.deltaTime;
		float sin = Mathf.Sin(deltaSpin);
		float cos = Mathf.Cos(deltaSpin);
		direction = new Vector2 (
			cos * direction.x - sin * direction.y,
			sin * direction.x + cos * direction.y
		);
	}

	public void move () {
		transform.position = new Vector3 (
			transform.position.x + direction.x * speed * Time.deltaTime,
			transform.position.y + direction.y * speed * Time.deltaTime,
			1f
		);
	}


	public void reflect (Vector2 normal, float speed_mod = 1.0f, float spin_mod = 1.0f) {
		direction = Vector2.Reflect(direction, normal);
		speed *= speed_mod;
		spin *= spin_mod;
	}

	public Vector2 getDirection () { return direction; }
	public float getSpin () { return spin; }
	public float getSpeed () { return speed; }

	void Update () {
		if (!bound){
			rotate();
			move();
		}
	}
}
