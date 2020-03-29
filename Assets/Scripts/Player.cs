using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	float iX;
	[SerializeField]
	float speed = 0.2f;

	void Start () {
		transform.position = new Vector3(0, -4.5f, 0);
	}

	void Update () {
		iX = Input.GetAxis("Horizontal");
		
	}

	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x + iX * speed, transform.position.y, transform.position.z);
		if (transform.position.x > 3.4f) {
			transform.position = new Vector3(3.4f, -4.5f, 0);
		} else if (transform.position.x < -3.4f) {
			transform.position = new Vector3(-3.4f, -4.5f, 0);
		}
	}
}
