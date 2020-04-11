using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour{

	CircleCollider2D collider;
	public SoundFX SFX;
	bool bound = true;
	Vector2 direction = Vector2.zero;
	float spin = 0.0f;
	float speed = 0.0f;
	[SerializeField] float maxdeflection = 5;

	public void spawn(Vector3 spawnPosition) {
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

	public void launch(float launchSpin) {
		//Debug.Log("launchSpin: " + launchSpin);
		// spin = launchSpin;
		spin = launchSpin;
		bound = false;
		SFX.GetComponent<SoundFX>().sfxBounce(0);
		SFX.GetComponent<SoundFX>().setCanRunAudio(true);
	}

	public void rotate() {
		float deltaSpin = spin * Time.deltaTime;
		float sin = Mathf.Sin(deltaSpin);
		float cos = Mathf.Cos(deltaSpin);
		direction = new Vector2 (
			cos * direction.x - sin * direction.y,
			sin * direction.x + cos * direction.y
		).normalized;
	}

	public void move() {
		transform.position = new Vector3 (
			transform.position.x + direction.x * speed * Time.deltaTime,
			transform.position.y + direction.y * speed * Time.deltaTime,
			1f
		);
	}


	public void reflect(Vector2 normal, float speed_mod = 1.0f, float spin_mod = 1.0f) {
		direction = Vector2.Reflect(direction, normal);
		speed *= speed_mod;
		spin *= spin_mod;
	}

	//ToDo: spin und speed mod vom collidable object holen
	void checkCollision() {
		float range = speed * Time.deltaTime;
		float distancePercentage = 1f;
		//raycast vom äußersten Ende des Balls in Bewegungsrichtung
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, collider.radius + range);
		if (hit.collider != null) {
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Collidable")) {
				if (hit.collider.OverlapPoint(transform.position)) {
					Debug.Log("Ball Position was inside a Wall-Collider");
				}
				//Reflektionspunkt minus Länge, die vom Hitpoint aus am Ray zrückgegangen werden muss, damit der Ball nicht ins Collidable clipt
				Vector2 reflectPosition = hit.point - collider.radius/Mathf.Cos(Vector2.Angle(hit.normal, -direction))*direction;

				distancePercentage = 1 - (new Vector2(transform.position.x, transform.position.y) - reflectPosition).magnitude/range;
				reflect(hit.normal);
				direction = direction * distancePercentage;
				//die Position des Balls wird an den Reflektionspunkt gelegt,
				//damit move von hier aus in die neue Richtung bewegt.
				transform.position = reflectPosition;
			}
		}
	}

	public Vector2 getDirection() { return direction; }
	public float getSpin() { return spin; }
	public float getSpeed() { return speed; }

	void Start() {
    collider = GetComponent<CircleCollider2D>();
	}

	void Update() {
		if (!bound){
			rotate();
			checkCollision();
			move();
		}
	}
}

/*Handling of direction:
in rotate() wird ein neuer direction vector erzeugt und normalisiert
in checkCollision() wird bei einer detektierten collision direction am normalen vector reflektiert
dabei wird die bereits zurückgelegte Strecke vor der Reflektion von der Länge von direction abgezogen
in move wird der gekürzte Vector verwendet um in die neue Richtung zu Zeigen
*/