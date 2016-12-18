using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour {

	private float score;

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pixel")
			score++;
			//audio.PlayOneShot(ping);
			Destroy(gameObject);
		Debug.Log(score++);

	}
}
