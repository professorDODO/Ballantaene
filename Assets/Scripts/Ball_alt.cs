using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_alt : MonoBehaviour {

	Vector2 direction = Vector2.zero;
	float spin = 0.0f;
	float speed = 0.0f;

	bool bound = true;
	[SerializeField]
	GameObject player = null;

	void Start () {
		spawn();
	}

	public void spawn () {
		bound = true;
		float rand = Random.Range(0.2f, 0.8f);
		direction = new Vector2(rand, 1.0f - rand);
		speed = 0.05f;
		transform.position = new Vector3(
			player.transform.position.x,
			player.transform.position.y + 0.1f,
			1f
		);
	}
	public void reflect (Vector2 normal, float speed_mod = 0.0f, float spin_mod = 0.0f) {
		direction = Vector2.Reflect(direction, normal);
		speed += speed_mod;
		spin += spin_mod;
	}

	public Vector2 getDirection () { return direction; }
	public float getSpin () { return spin; }
	public float getSpeed () { return speed; }

	void Update () {
		if (Input.GetKey("up")) {
			bound = false;
		}
	}

	void FixedUpdate () {
		if (bound) {
			transform.position = new Vector3(
				player.transform.position.x,
				player.transform.position.y + 0.4f,
				1f
			);
		} else {
			transform.position = new Vector3(
				transform.position.x + direction.x * speed,
				transform.position.y + direction.y * speed,
				0
			);
		}
	}
}
