using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	private void OnTriggerEnter2D (Collider2D col) {
		GameObject other = col.gameObject;
		if (other.CompareTag("Ball")) {
      other.SetActive(false);
    }
	}
}
