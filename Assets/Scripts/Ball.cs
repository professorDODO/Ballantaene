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
	[SerializeField] float maxdeflection = 5f;
	[SerializeField] int interpolationFrames = 2;

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

	public void rotate(float fraction) {
		float deltaSpin = fraction * spin * Time.deltaTime;
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
	//returns true, wenn something was hit
	bool checkCollision(float fraction) {
		float range = speed * Time.deltaTime;
		float distancePercentage = 1f;
		RaycastHit2D hit = Physics2D.CircleCast(transform.position, collider.radius, direction, fraction * range, LayerMask.GetMask("Collidable"));
		if (hit.collider != null) {
			if (hit.collider.OverlapPoint(transform.position)) {
					Debug.Log("Ball Position was inside a Collider");
			}
			Vector2 reflectPosition = hit.centroid;
			distancePercentage = 1 - (new Vector2(transform.position.x, transform.position.y) - reflectPosition).magnitude/(fraction * range);
			reflect(hit.normal);
			direction = direction * distancePercentage;
			transform.position = reflectPosition;
			return true;
		} else {
			return false;
		}
	}

	public Vector2 getDirection() { return direction; }
	public float getSpin() { return spin; }
	public float getSpeed() { return speed; }

	void Start() {
    collider = GetComponent<CircleCollider2D>();
	}

	//ToDo: InterpolationFrames sorgen dafür, dass der Ball ab und zu stecken bleibt
	//			war kein Prolem, ohne Interpolation
	void Update() {
		if (!bound){
			for (int i = 1; i <= interpolationFrames; i++)  {
  			rotate((float)i/interpolationFrames);
				if (checkCollision((float)i/interpolationFrames)) {
					break;
				}
  		}
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