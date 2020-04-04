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
	[SerializeField] int speedFrameInterp = 3;
	[SerializeField] float speed;
	[SerializeField] float maxSpeed = 20;
	Vector3 ballFreezePos = Vector3.zero;

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
			BallScript.spawn(transform.position, speed);
			ballFreezePos = transform.position;
		}
		if (Input.GetMouseButton(0)) {
			
		}
		if (Input.GetMouseButtonUp(0)) {
			BallScript.launch(ballFreezePos.x - transform.position.x);
		}
		
	}

	void FixedUpdate() {
		playerPos();
		calcSpeed();
	}


	Vector3 posOnScreen() {
		return Camera.main.ScreenToWorldPoint(new Vector3(
			Input.mousePosition.x,
			Input.mousePosition.y,
			Camera.main.nearClipPlane
		));
	}

	//ToDo: "mousePos.x - transform.position.x" muss richtig berechnet werden, wenn der Cursor am Spielrand ist
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

float calcSpeed() {
		float tempSpeed = (transform.position.x - lastPos.x)/(speedFrameInterp*Time.fixedDeltaTime);
		if (frameCounter >= speedFrameInterp) {
			lastPos = transform.position;
			frameCounter = 0;
		} else {
			frameCounter = frameCounter + 1;
		}
		if (tempSpeed > maxSpeed) {
			tempSpeed = maxSpeed;
		}
		return tempSpeed;
	}
}


