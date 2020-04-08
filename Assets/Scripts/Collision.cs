using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	public SoundFX SFX;
	[SerializeField] Vector2 normal = Vector2.zero;
	[SerializeField] bool canRunAudio = true;
	int counter = 0;

	private void OnTriggerEnter2D (Collider2D col) {
		GameObject other = col.gameObject;
		if (other.CompareTag("Ball")) {
      other.GetComponent<Ball>().reflect(normal);
      if (gameObject.CompareTag("Player") && canRunAudio) {
        if (counter == 0) {
        	SFX.GetComponent<SoundFX>().sfxBounce(0);
        	counter = 1;
        } else {
					SFX.GetComponent<SoundFX>().sfxBounce(1);
					counter = 0;
        }
      }
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
    if (other.CompareTag("Ball") && gameObject.CompareTag("Player") && canRunAudio) {
        canRunAudio = true;
    }
	}
}
