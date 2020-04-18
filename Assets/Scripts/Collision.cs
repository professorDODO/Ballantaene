using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//every object of the Label "Collidable" should have this script
public class Collision : MonoBehaviour {

	int counterSFX = 1;

	//reaction to the collision with a ball
	public void collide(GameObject other) {
		switch (this.tag) {
			case "Player":
				if (counterSFX == 0) {
					other.GetComponent<SoundFX>().sfxBounce(0);
					counterSFX = 1;
				} else {
					other.GetComponent<SoundFX>().sfxBounce(1);
					counterSFX = 0;
				}
				break;
			case "Boundary":
				other.GetComponent<SoundFX>().sfxBounce(2);
				break;
			case "DeathPit":
				GetComponent<DeathAtBoundary>().playDeathSound();
				other.GetComponent<Death>().kill();
				break;
			default:
				Debug.Log("This Collidable has no fitting tag");
				break;
		}
	}

	/*
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
		} else if (other.CompareTag("Player")) {
			other.GetComponent<Player>().setAtWall(true, normal.x);
		}
	}

	private void OnTriggerExit2D (Collider2D col) {
		GameObject other = col.gameObject;
		if (other.CompareTag("Player")) {
			other.GetComponent<Player>().setAtWall(false, 0);
		} else if (other.CompareTag("Ball")) {
				other.GetComponent<SoundFX>().setCanRunAudio(true);
		}
	}
	*/
}
