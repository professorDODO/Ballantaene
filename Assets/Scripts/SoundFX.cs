using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip[] audioSrcArr;

	void Start() {
    audioSource = GetComponent<AudioSource>();
  }

	public void sfxBounce(int i) {
		audioSource.clip = audioSrcArr[i];
		audioSource.Play();
	}
}
