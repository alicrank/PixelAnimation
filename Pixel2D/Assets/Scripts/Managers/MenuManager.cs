using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	///*************************************************************************///
	/// Main Menu Buttons Controller.
	///*************************************************************************///

	public AudioClip menuTap;

	private bool canTap;
	private float buttonAnimationSpeed = 9;

	void Awake ()
	{

		canTap = true; //player can tap on buttons
	}

	void Start ()
	{
		//prevent screenDim in handheld devices
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update ()
	{

		if (canTap)
			StartCoroutine (tapManager ());
	}

	///***********************************************************************
	/// Process user inputs
	///***********************************************************************
	private RaycastHit hitInfo;
	private Ray ray;

	IEnumerator tapManager ()
	{

		//Mouse of touch?
		if (Input.touches.Length > 0 && Input.touches [0].phase == TouchPhase.Ended)
			ray = Camera.main.ScreenPointToRay (Input.touches [0].position);
		else if (Input.GetMouseButtonUp (0))
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		else
			yield break;

		if (Physics.Raycast (ray, out hitInfo)) {
			GameObject objectHit = hitInfo.transform.gameObject;
			switch (objectHit.name) {
			case "btnPlay":
				canTap = false;
				playSfx (menuTap);
				StartCoroutine(animateButton(objectHit));
				yield return new WaitForSeconds (1.0f);
				SceneManager.LoadScene ("Game-c#");
				break;

			case "btnExit":
				canTap = false;
				playSfx (menuTap);
				StartCoroutine (animateButton (objectHit));
				yield return new WaitForSeconds (1.0f);
				Application.Quit ();
				break;

			case "btnInfo":
				canTap = false;
				playSfx (menuTap);
				StartCoroutine (animateButton (objectHit));
				yield return new WaitForSeconds (1.0f);
				//Application.Quit();
				break;

			case "btnRank":
				canTap = false;
				playSfx (menuTap);
				StartCoroutine (animateButton (objectHit));
				yield return new WaitForSeconds (1.0f);
				SceneManager.LoadScene ("Settings-c#");
				break;

			}	
		}
	}

	///***********************************************************************
	/// Animate button by modifying it's scale
	///***********************************************************************
	IEnumerator animateButton (GameObject _btn)
	{
		canTap = false;
		Vector3 startingScale = _btn.transform.localScale;
		Vector3 destinationScale = startingScale * 1.1f;
		//yield return new WaitForSeconds(0.1f);
		float t = 0.0f; 
		while (t <= 1.0f) {
			t += Time.deltaTime * buttonAnimationSpeed;
			_btn.transform.localScale = new Vector3 (Mathf.SmoothStep (startingScale.x, destinationScale.x, t),
				_btn.transform.localScale.y,
				Mathf.SmoothStep (startingScale.z, destinationScale.z, t));
			yield return 0;
		}

		float r = 0.0f; 
		if (_btn.transform.localScale.x >= destinationScale.x) {
			while (r <= 1.0f) {
				r += Time.deltaTime * buttonAnimationSpeed;
				_btn.transform.localScale = new Vector3 (Mathf.SmoothStep (destinationScale.x, startingScale.x, r),
					_btn.transform.localScale.y,
					Mathf.SmoothStep (destinationScale.z, startingScale.z, r));
				yield return 0;
			}
		}

		if (r >= 1)
			canTap = true;
	}

	///***********************************************************************
	/// play audio clip
	///***********************************************************************
	void playSfx (AudioClip _sfx)
	{
		GetComponent<AudioSource> ().clip = _sfx;
		if (!GetComponent<AudioSource> ().isPlaying)
			GetComponent<AudioSource> ().Play ();
	}

}