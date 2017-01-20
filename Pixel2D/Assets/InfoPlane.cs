using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoPlane : MonoBehaviour {

	///***********************************************************************
	/// GameOver Manager Class. 
	///***********************************************************************

	public AudioClip menuTap;
	private bool canTap;
	private float buttonAnimationSpeed = 9;



	void Awake (){
		canTap = true;


		//bestScore = PlayerPrefs.GetInt("bestScore");
		//bestScoreText.GetComponent<TextMesh>().text = bestScore.ToString();
	}

	void Update (){	

		//Set the new score on the screen
	//	scoreText.GetComponent<TextMesh>().text = PlayerManager.playerScore.ToString();

		if(canTap)
			StartCoroutine(tapManager());
	}
	///***********************************************************************
	/// Manage user taps on gameover buttons
	///***********************************************************************
	private RaycastHit hitInfo;
	private Ray ray;
	IEnumerator tapManager (){

		//Mouse of touch?
		if(	Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended)  
			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		else if(Input.GetMouseButtonUp(0))
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		else
			yield break;

		if (Physics.Raycast(ray, out hitInfo)) {
			GameObject objectHit = hitInfo.transform.gameObject;
			print(objectHit.name);
			switch(objectHit.name) {
			case "retryButton":
				playSfx(menuTap);			//play audioclip
				StartCoroutine(animateButton(objectHit));	//animate the button
				yield return new WaitForSeconds(0.4f);	//Wait
				SceneManager.LoadScene ("Menu-c#");
				break;
			case "menuButton":
				playSfx(menuTap);
				StartCoroutine(animateButton(objectHit));
				yield return new WaitForSeconds(1.0f);
				SceneManager.LoadScene ("Menu-c#");
				break;
			}	
		}
	}

	///***********************************************************************
	/// Animate buttons on touch
	///***********************************************************************
	IEnumerator animateButton ( GameObject _btn  ){
		canTap = false;
		Vector3 startingScale = _btn.transform.localScale;
		Vector3 destinationScale = startingScale * 1.1f;
		//yield return new WaitForSeconds(0.1f);
		float t = 0.0f; 
		while (t <= 1.0f) {
			t += Time.deltaTime * buttonAnimationSpeed;
			_btn.transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, destinationScale.x, t),
				_btn.transform.localScale.y,
				Mathf.SmoothStep(startingScale.z, destinationScale.z, t));
			yield return 0;
		}

		float r = 0.0f; 
		if(_btn.transform.localScale.x >= destinationScale.x) {
			while (r <= 1.0f) {
				r += Time.deltaTime * buttonAnimationSpeed;
				_btn.transform.localScale = new Vector3(Mathf.SmoothStep(destinationScale.x, startingScale.x, r),
					_btn.transform.localScale.y,
					Mathf.SmoothStep(destinationScale.z, startingScale.z, r));
				yield return 0;
			}
		}

		if(r >= 1)
			canTap = true;
	}

	///***********************************************************************
	/// IPlay audioclip
	///***********************************************************************
	void playSfx ( AudioClip _sfx  ){
		GetComponent<AudioSource>().clip = _sfx;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}

}