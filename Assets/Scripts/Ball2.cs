using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2 : MonoBehaviour{

	bool launched = false;
	Vector2 direction = Vector2.zero;
	float spin = 0.0f;
	float speed = 0.1f;

	void FixedUpdate(){
		if (launched == true) {
			transform.position = new Vector3(
				transform.position.x + direction.x * speed,
				transform.position.y + direction.y * speed,
				0f
			);
		}
	}

	public void spawn(Vector3 pTf) {
		gameObject.SetActive(true);
		transform.position = new Vector3(
			pTf.x,
			pTf.y + 0.4f,
			0f
		);
		launched = false;
	}

	public void launch(float pSpeed, float pMaxSpeed) {
		direction = new Vector2(
			pSpeed/pMaxSpeed,
			(pMaxSpeed - Mathf.Abs(pSpeed))/pMaxSpeed
		).normalized;
		launched = true;
	}
}
