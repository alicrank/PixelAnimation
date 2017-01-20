using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InfoBall : MonoBehaviour {

	//***************************************************************************//
	// This class manages pause and unpause states.
	//***************************************************************************//
	public static bool isPaused;
	private float savedTimeScale;
	public GameObject ballPlane;
	public GameObject cameraPlane;

	enum Page {
		INFO, BALL, CAMERA
	}
	private Page currentPage = Page.INFO;

	void Awake (){		
		isPaused = false;

		Time.timeScale = 1.0f;

		if(ballPlane)
			ballPlane.SetActive(false); 
		
		if(cameraPlane)
			cameraPlane.SetActive(false); 

	}

	void Update (){
		//touch control
		touchManager();

		//optional pause
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape)) {
			//PAUSE THE GAME
			switch (currentPage) {
			case Page.INFO: 
				BallGame(); 
				break;
			case Page.BALL: 
				UnBallGame(); 
				break;
			default: 
				currentPage = Page.INFO; 
				break;
			}
		}

		//debug restart
		if(Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene ("Menu-c#");	
		}
	}


	private RaycastHit hitInfo;
	private Ray ray;
	void touchManager (){
		if(Input.GetMouseButtonUp(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo)) {
				string objectHitName = hitInfo.transform.gameObject.name;
				switch(objectHitName) {

				case "btnBall":
					switch (currentPage) {
					case Page.INFO: 
						BallGame(); 
						break;
					case Page.BALL: 
						UnBallGame(); 
						break;
					default: 
						currentPage = Page.INFO; 
						break;
					}
					break;
				
				case "btnCamera":
					switch (currentPage) {
					case Page.INFO: 
						CameraGame(); 
						break;
					case Page.CAMERA: 
						UnCameraGame(); 
						break;
					default: 
						currentPage = Page.INFO; 
						break;
					}
					break;

				
				case "exitBall":
					UnBallGame(); 
					//Application.LoadLevel("Menu-c#");
					break;
				
				case "exitCamera":
					UnCameraGame(); 
					//Application.LoadLevel("Menu-c#");
					break;
			
				}
			}
		}
	}

	void BallGame (){
		print("Ball Information section...");
		isPaused = true;
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		//AudioListener.volume = 0;
		if(ballPlane)
			ballPlane.SetActive(true);
		currentPage = Page.BALL;
	}

	void UnBallGame (){
		print("Closing ball information section");
		isPaused = false;
		Time.timeScale = savedTimeScale;
		//AudioListener.volume = 1.0f;
		if(ballPlane)
			ballPlane.SetActive(false);   
		currentPage = Page.INFO;
	}
	void CameraGame (){
		print("Camera Information section...");
		isPaused = true;
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		//AudioListener.volume = 0;
		if(cameraPlane)
			cameraPlane.SetActive(true);
		currentPage = Page.CAMERA;
	}

	void UnCameraGame (){
		print("Closing camera information section");
		isPaused = false;
		Time.timeScale = savedTimeScale;
	//	AudioListener.volume = 1.0f;
		if(cameraPlane)
			cameraPlane.SetActive(false);   
		currentPage = Page.INFO;
	}

}
///***********************************************************************
/// Animate buttons on touch
///***********************************************************************
//IEnumerator animateButton ( GameObject _btn  ){
//	canTap = false;
//	Vector3 startingScale = _btn.transform.localScale;
//	Vector3 destinationScale = startingScale * 1.1f;
//	//yield return new WaitForSeconds(0.1f);
//	float t = 0.0f; 
//	while (t <= 1.0f) {
//		t += Time.deltaTime * buttonAnimationSpeed;
//		_btn.transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, destinationScale.x, t),
//			_btn.transform.localScale.y,
//			Mathf.SmoothStep(startingScale.z, destinationScale.z, t));
//		yield return 0;
//	}
//
//	float r = 0.0f; 
//	if(_btn.transform.localScale.x >= destinationScale.x) {
//		while (r <= 1.0f) {
//			r += Time.deltaTime * buttonAnimationSpeed;
//			_btn.transform.localScale = new Vector3(Mathf.SmoothStep(destinationScale.x, startingScale.x, r),
//				_btn.transform.localScale.y,
//				Mathf.SmoothStep(destinationScale.z, startingScale.z, r));
//			yield return 0;
//		}
//	}
//
//	if(r >= 1)
//		canTap = true;
//}
