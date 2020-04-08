using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	[SerializeField] Vector2 normal = Vector2.zero;
	int counter = 1;

	private void OnTriggerEnter2D (Collider2D col) {
		GameObject other = col.gameObject;
		if (other.CompareTag("Ball")) {
      other.GetComponent<Ball>().reflect(normal);
      if (gameObject.CompareTag("Player")) {
        if (counter == 0) {
        	other.GetComponent<SoundFX>().sfxBounce(0);
        	counter = 1;
        } else {
					other.GetComponent<SoundFX>().sfxBounce(1);
					counter = 0;
        }
      } else if (gameObject.CompareTag("Boundary")) {
      	other.GetComponent<SoundFX>().sfxBounce(2);
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
    if (other.CompareTag("Ball")) {
        other.GetComponent<SoundFX>().setCanRunAudio(true);
    }
	}
}
