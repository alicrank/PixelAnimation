using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningStar : MonoBehaviour {

	public int scoreValue;
	private GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	void OnTriggerEnter2D(Collider2D co) {

		if (co.name == "pixel")
		{
			Destroy (gameObject);
		}

		gameController.AddScore(scoreValue);
		//Application.LoadLevel("HighScore");
		SceneManager.LoadScene("HighScore");	
	
	}
}