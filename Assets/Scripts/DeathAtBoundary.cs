using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAtBoundary : MonoBehaviour {
  
  AudioSource audioSource;
  
  void Start() {
    audioSource = GetComponent<AudioSource>();
  }

	public void playDeathSound() {
		audioSource.Play();
	}
}
