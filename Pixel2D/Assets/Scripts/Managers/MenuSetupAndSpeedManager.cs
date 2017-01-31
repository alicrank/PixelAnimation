using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSetupAndSpeedManager : MonoBehaviour {

	///*************************************************************************///
	/// Main Menu Buttons Controller.
	///*************************************************************************///

	private int controlType = 1;		
	public AudioClip menuTap;
	private bool canTap;
	private float buttonAnimationSpeed = 9;

	void Awake (){
		canTap = true; //player can tap on buttons
	}

	void Start (){
		//prevent screenDim in handheld devices
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	public void speedValueChanged(float newSpeed)
	{
		newSpeed = newSpeed / 10.0f;
		Debug.Log (newSpeed);
		PlayerPrefs.SetFloat ("velocityFactor", newSpeed);
	}
	void Update (){
		if(canTap)	
			StartCoroutine(tapManager());
	}

	///***********************************************************************
	/// Process user inputs
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
			switch(objectHit.name) {
			case "btnPlay":
				playSfx(menuTap);
				StartCoroutine(animateButton(objectHit));
//				yield return new WaitForSeconds(1.0f);
				SceneManager.LoadScene ("Menu-c#");
				Debug.Log(string.Format("Back"));
				break;

			case "btnExit":
				canTap = false;
				playSfx(menuTap);
				StartCoroutine(animateButton(objectHit));
				yield return new WaitForSeconds(1.0f);
				Application.Quit();
				break;

			case "btnBall":
				//controlType = 0;
				PlayerPrefs.SetInt("controlType", controlType);
				Debug.Log(string.Format("Ball"));
				break;

			case "btnCamera":
				//controlType = 1;
				PlayerPrefs.SetInt("controlType", controlType);
				Debug.Log(string.Format("Cam"));
				break;

			}	
		}
	}

	///***********************************************************************
	/// Animate button by modifying it's scale
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
	/// play audio clip
	///***********************************************************************
	void playSfx ( AudioClip _sfx  ){
		GetComponent<AudioSource>().clip = _sfx;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}

}