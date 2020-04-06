using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollision : MonoBehaviour {
	Vector2 normal = Vector2.right;
	private void OnTriggerEnter2D (Collider2D ball) {
		ball.GetComponent<Ball>().reflect(normal);
	}
}
