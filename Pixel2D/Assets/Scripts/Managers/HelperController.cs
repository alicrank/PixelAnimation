using UnityEngine;
using System.Collections;

public class HelperController : MonoBehaviour {
		
	///***********************************************************************
	/// Main Helper class.
	/// This script will clone helper items (+1 life) for player to eat and restore life.
	///***********************************************************************

	//Difficulty
	public static float helperSpeed = 0.95f; 		//helper screll speed on screen
	private float helperCloneIntervalMin = 30.0f;	//Min
	private float helperCloneIntervalMax = 60.0f; 	//Max

	//Pool of helper objects
	public GameObject[] helpers;

	//start point x,y,z
	private Vector3 startPoint;
	private float cloneLeftLimit = -2.3f;
	private float cloneRightLimit = 2.3f;
	private float cloneUpLimit = 4.1f;
	private float cloneDownLimit = 0.1f;
	private float rnd = 0;
	private float startTime = 0;

	void Start (){
		rnd = Random.Range(helperCloneIntervalMin, helperCloneIntervalMax);
	}

	void Update (){
		if(Time.timeSinceLevelLoad  >  rnd + startTime){
			cloneHelper();
			Debug.Log( "I have started"); 
			startTime += rnd;
			rnd = Random.Range(helperCloneIntervalMin, helperCloneIntervalMin);
		}
	}

	void cloneHelper (){
		//startPoint = new Vector3(Random.Range(-2.3f, 2.3f), 0.5f, Random.Range(5.0f, -5.0f));
		startPoint = new Vector3(Random.Range(cloneLeftLimit, cloneRightLimit), 1f, Random.Range(cloneDownLimit, cloneUpLimit));
		Instantiate(helpers[Random.Range(0, helpers.Length)], startPoint, transform.rotation);
	}	

}