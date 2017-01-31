using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	//***************************************************************************//
	// This class manages pause and unpause states.
	//***************************************************************************//
	public static bool isPaused;
	private float savedTimeScale;
	public GameObject infoPlane;

	enum Page {
		PLAY, INFO
	}
	private Page currentPage = Page.PLAY;

	void Awake (){		
		isPaused = false;

		Time.timeScale = 1.0f;

		if(infoPlane)
			infoPlane.SetActive(false);
		
		
	}

	void Update (){
		//touch control
		touchManager();

		//optional pause
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape)) {
			//PAUSE THE GAME
			switch (currentPage) {
			case Page.PLAY: 
				InfoGame(); 
				break;
			case Page.INFO: 
				UnInfoGame(); 
				break;
			default: 
				currentPage = Page.PLAY; 
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

				case "btnInfo":
					switch (currentPage) {
					case Page.PLAY: 
						InfoGame(); 
						break;
					case Page.INFO: 
						UnInfoGame(); 
						break;
					default: 
						currentPage = Page.PLAY; 
						break;
					}
					break;

				case "btnRank":
					UnInfoGame(); 
					SceneManager.LoadScene ("Settings-c#");	
					break;

				case "retryButtonPause":
					UnInfoGame(); 
					SceneManager.LoadScene ("Menu-c#");	
					break;

				case "menuButtonPause":
					UnInfoGame(); 
					SceneManager.LoadScene ("Menu-c#");	
					break;
				case "btnExit":
					UnInfoGame ();
					Application.Quit();
					SpheroProvider.GetSharedProvider().DisconnectSpheros();
					break;
				}
			}
		}
	}

	void InfoGame (){
		print("Game in Information section...");
		isPaused = true;
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		//AudioListener.volume = 0;
		if(infoPlane)
			infoPlane.SetActive(true);
		currentPage = Page.INFO;
	}

	void UnInfoGame (){
		print("Closing information section");
		isPaused = false;
		Time.timeScale = savedTimeScale;
		//AudioListener.volume = 1.0f;
		if(infoPlane)
			infoPlane.SetActive(false);   
		currentPage = Page.PLAY;
	}

}