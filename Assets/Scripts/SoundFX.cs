using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip[] audioSrcArr;
	bool canRunAudio = true;

	void Start() {
    audioSource = GetComponent<AudioSource>();
  }

	public void sfxBounce(int i) {
		if (canRunAudio) {
			audioSource.clip = audioSrcArr[i];
			audioSource.Play();
			canRunAudio = false;
		}
	}

	public bool getCanRunAudio() {return canRunAudio;}
	public void setCanRunAudio(bool val) {canRunAudio = val;}
}
