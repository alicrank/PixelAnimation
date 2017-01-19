using UnityEngine;
using System.Collections;

public class InfoBall : MonoBehaviour {

	//***************************************************************************//
	// This class manages pause and unpause states.
	//***************************************************************************//
	public static bool isPaused;
	private float savedTimeScale;
	public GameObject ballPlane;
	public GameObject cameraPlane;

	enum Page {
		PLAY, BALL, CAMERA
	}
	private Page currentPage = Page.PLAY;

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
			case Page.PLAY: 
				BallGame(); 
				break;
			case Page.BALL: 
				UnBallGame(); 
				break;
			default: 
				currentPage = Page.PLAY; 
				break;
			}
		}

		//debug restart
		if(Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevelName);
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
					case Page.PLAY: 
						BallGame(); 
						break;
					case Page.BALL: 
						UnBallGame(); 
						break;
					default: 
						currentPage = Page.PLAY; 
						break;
					}
					break;
				
				case "btnCamera":
					switch (currentPage) {
					case Page.PLAY: 
						CameraGame(); 
						break;
					case Page.CAMERA: 
						UnCameraGame(); 
						break;
					default: 
						currentPage = Page.PLAY; 
						break;
					}
					break;

				
				case "exitBall":
					UnBallGame(); 
				//	Application.LoadLevel("Menu");
					break;
				
				case "exitCamera":
					UnCameraGame(); 
				//	Application.LoadLevel("Menu");
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
		AudioListener.volume = 0;
		if(ballPlane)
			ballPlane.SetActive(true);
		currentPage = Page.BALL;
	}

	void UnBallGame (){
		print("Closing ball information section");
		isPaused = false;
		Time.timeScale = savedTimeScale;
		AudioListener.volume = 1.0f;
		if(ballPlane)
			ballPlane.SetActive(false);   
		currentPage = Page.PLAY;
	}
	void CameraGame (){
		print("Camera Information section...");
		isPaused = true;
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.volume = 0;
		if(cameraPlane)
			cameraPlane.SetActive(true);
		currentPage = Page.CAMERA;
	}

	void UnCameraGame (){
		print("Closing camera information section");
		isPaused = false;
		Time.timeScale = savedTimeScale;
		AudioListener.volume = 1.0f;
		if(cameraPlane)
			cameraPlane.SetActive(false);   
		currentPage = Page.PLAY;
	}

}