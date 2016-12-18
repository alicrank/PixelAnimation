using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	//public GameObject pixelExplosion;

	public AudioClip clip;
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

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
			//clip = GetComponent<AudioClip>();
	
		}

		//if (co.name == "pixel")
		//{
		//	Instantiate(pixelExplosion);

			gameController.AddScore(scoreValue);
			Destroy (gameObject);
			
		}

}