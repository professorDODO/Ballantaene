using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	private void OnTriggerEnter2D (Collider2D ball) {
		ball.GetComponent<Ball>().spawn();
	}
}
