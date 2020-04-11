using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//Script in Editor hinzugefügt
	[SerializeField]Ball BallScript;
	
	Vector3 mousePos = Vector3.zero;
	Vector3 lastPos = Vector3.zero;
	int frameCounter = 0;
	[SerializeField] int speedFrameInterp = 3;
	[SerializeField] float speed;
	[SerializeField] float maxSpeed = 20;
	Vector3 ballFreezePos = Vector3.zero;
	[SerializeField] bool atWall = false;
	float awayFromWall = 0f;

	void Start() {
		mousePos = posOnScreen();
		transform.position = new Vector3(0, -4.5f, 0);
		lastPos = transform.position;
	}

	void Update() {
		mousePos = posOnScreen();
		if (Input.GetMouseButtonDown(0)) {
			BallScript.spawn(transform.position);
			ballFreezePos = transform.position;
		}
		if (Input.GetMouseButton(0)) {
			
		}
		if (Input.GetMouseButtonUp(0)) {
			BallScript.launch(transform.position.x - ballFreezePos.x);
		}
		
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

	public void setAtWall(bool aW, float awayFromWallTemp) {
		atWall = aW;
		awayFromWall = awayFromWallTemp;
	}

	//ToDo: "mousePos.x - transform.position.x" muss richtig berechnet werden, wenn der Cursor am Spielrand ist
	void playerPos() {
		float nextSpeed = (mousePos.x - transform.position.x)/Time.fixedDeltaTime;
		if (!atWall || (atWall && (mousePos.x - transform.position.x)*awayFromWall > 0)) {
			if (Mathf.Abs(nextSpeed) > maxSpeed) {
				transform.position = new Vector3(
					transform.position.x + Mathf.Abs(nextSpeed)/nextSpeed*maxSpeed*Time.fixedDeltaTime,
					transform.position.y,
					transform.position.z
				);
				speed = Mathf.Abs(nextSpeed)/nextSpeed*maxSpeed;
			} else {
				transform.position = new Vector3(
					mousePos.x,
					transform.position.y,
					transform.position.z
				);
				speed = nextSpeed;
			}
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


