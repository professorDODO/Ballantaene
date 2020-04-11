using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D (Collider2D col) {
		GameObject other = col.gameObject;
		if (other.CompareTag("Ball")) {
			audioSource.Play();
			other.SetActive(false);
		}
	}
}
