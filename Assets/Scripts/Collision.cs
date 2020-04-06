using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	[SerializeField] Vector2 normal = Vector2.zero;

	private void OnTriggerEnter2D (Collider2D col) {
		GameObject other = col.gameObject;
		if (other.CompareTag("Ball")) {
      other.GetComponent<Ball>().reflect(normal);
    }
    if (other.CompareTag("Player")) {
    	other.GetComponent<Player>().setAtWall(true, normal.x);
    }
	}

	private void OnTriggerExit2D (Collider2D col) {
		GameObject other = col.gameObject;
		if (other.CompareTag("Player")) {
    	other.GetComponent<Player>().setAtWall(false, 0);
    }
	}
}
