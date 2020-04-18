using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	public void kill() {
		this.gameObject.SetActive(false);
	}
}
