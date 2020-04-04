using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//Script in Editor hinzugefügt
	[SerializeField]
	Ball2 BallScript;
	
	Vector3 mousePos = Vector3.zero;
	Vector3 lastPos = Vector3.zero;
	int frameCounter = 0;
	[SerializeField] int speedFrameInterp;
	[SerializeField] float speed;
	[SerializeField] float maxSpeed;
	[SerializeField] float deadZoneSpeed;
	public float speedMedian = 0f;
	int spMedCounter = 0;

	void Start() {
		mousePos = posOnScreen();
		transform.position = new Vector3(0, -4.5f, 0);
		lastPos = transform.position;
	}

	//LEO: Verstehst du, warum der Mittelwert des Speeds "speedMeadian"
	//		 Nicht dem tatsächlichem Mittelwert zu entsprechen scheint?
	void Update() {
		mousePos = posOnScreen();
		if (Input.GetMouseButtonDown(0)) {
			BallScript.spawn(transform.position);
		}
		if (Input.GetMouseButton(0)) {
			if (speedMedian*speed >= 0) {
				if (Mathf.Abs(speed) > deadZoneSpeed) {
					speedMedian = (speedMedian + speed)/(1 + spMedCounter);
					spMedCounter = spMedCounter + 1;
				}
			} else {
				speedMedian = speed;
				spMedCounter = 0;
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			BallScript.launch(speedMedian, maxSpeed);
			speedMedian = 0f;
			spMedCounter = 0;
		}
		
	}

	void FixedUpdate() {
		playerPos();
		//calcSpeed();
		if (Input.GetMouseButton(0)) {
			//ToDo: Energieverlust im Anspin-modus
		}
	}


	Vector3 posOnScreen() {
		return Camera.main.ScreenToWorldPoint(new Vector3(
			Input.mousePosition.x,
			Input.mousePosition.y,
			Camera.main.nearClipPlane
		));
	}

	void playerPos() {
		float nextSpeed = (mousePos.x - transform.position.x)/Time.fixedDeltaTime;
		if (Mathf.Abs(nextSpeed) > maxSpeed) {
			transform.position = new Vector3(
				transform.position.x + Mathf.Abs(nextSpeed)/nextSpeed*maxSpeed*Time.fixedDeltaTime,
				transform.position.y,
				transform.position.z
			);
			speed = Mathf.Abs(nextSpeed)/nextSpeed*maxSpeed;
		}else {
			transform.position = new Vector3(
				mousePos.x,
				transform.position.y,
				transform.position.z
			);
			speed = nextSpeed;
		}
		//ToDo: Hardcoded gegen Wandpos ersetzen
		if (transform.position.x > 3.4f) {
			transform.position = new Vector3(3.4f, -4.5f, 0);
		} else if (transform.position.x < -3.4f) {
			transform.position = new Vector3(-3.4f, -4.5f, 0);
		}
	}

	//momentan obsolet, könnte aber später noch hilfreich sein, wenn der Speed nicht akurat genug berechnet wird
	float calcSpeed() {
		float tempSpeed = (transform.position.x - lastPos.x)/(speedFrameInterp*Time.fixedDeltaTime);
		if (frameCounter >= speedFrameInterp) {
			lastPos = transform.position;
			frameCounter = 0;
		} else {
			frameCounter = frameCounter + 1;
		}
		return tempSpeed;
	}
}


