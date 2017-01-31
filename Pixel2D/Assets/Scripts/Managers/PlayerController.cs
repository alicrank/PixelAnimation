using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    ///***********************************************************************
    /// PlayerController class
    /// This class is responsible to move the player by touch or by device acceleraion sensors
    ///***********************************************************************
    public Joystick mJoystick;
	//Player Control Type (tilt or touch)
	public static int controlType; //0=tilt , 1=touch

	//Distance between player and user's finger
	private int fingerOffset = 100;
    private bool useCamera = false;
	//Private internal variables
	private float xVelocity = 0.0f;
	private float zVelocity = 0.0f;
	private float speed = 1.0f;
	private Vector3 dir = Vector3.zero;
	private Vector3 screenToWorldVector;
    public string url;
    private WWW www_;
    private string cheading_;
	Vector3 dest = Vector3.zero;
	private float xx=0.0f;
    private float step=0.0f;
	void Awake (){

		// get yVelocity  (currentpositiony - previouspositiony) 
		//fetch user defined controlType
		controlType = PlayerPrefs.GetInt("controlType");
	}

	void Start (){
		// Y offset for player
		transform.position = new Vector3(transform.position.x,
		                                 0.5f,
		                                 transform.position.z);
		dest = transform.position;
        url = "http://192.168.8.102:5000/currentPos";
        
        StartCoroutine(WaitForHeading());
		Debug.Log (controlType);
	}
    public IEnumerator WaitForHeading()
    {
        while (true)
        {
            WWW mpage = new WWW(url);
            yield return mpage;
            cheading_ = mpage.text;
        }
    }


	void Update (){
        if (!GameController.gameOver)
        {
            if ((mJoystick.updatePixel) ||( useCamera))
            {
                    touchControl();

                //this is just for debug and play in PC and SHOULD be commented at final build
                
                //offset for player
                transform.position = new Vector3(transform.position.x,
                                                 0.5f,
                                                 transform.position.z);

                //prevent player from exiting the view (downside)
                if (transform.position.z < -2.3f)
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y,
                                                     -2.3f);

                //prevent player from exiting the view (Upside)
                if (transform.position.z > 5.2f)
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y,
                                                     5.2f);

                //left/right movement limiter
                if (transform.position.x > 2.5f)
                    transform.position = new Vector3(2.5f,
                                                     transform.position.y,
                                                     transform.position.z);

                if (transform.position.x < -2.5f)
                    transform.position = new Vector3(-2.5f,
                                                     transform.position.y,
                                                     transform.position.z);
            }

			if (useCamera) {
				string[] pos = cheading_.Split(',');
				float x = float.Parse(pos[0]);
				float y = (float.Parse(pos[1]));
				transform.position = new Vector3(x, transform.position.y, y);

			}
			Vector3 dir = dest - (Vector3)transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            dest = transform.position;
        }

	}


	///***********************************************************************
	/// Control playerShip's position by acceleration sensors
	///***********************************************************************
	void camControl (){
        string[] pos = cheading_.Split(',');
        float x = float.Parse(pos[0]);
        float y = (float.Parse(pos[1]));
        //Debug.Log(string.Format(cheading_));
        //Debug.Log(string.Format(pos[0]));
        Debug.Log(string.Format(pos[1]));
        transform.position = new Vector3(x, transform.position.z, y);
    }
	///***********************************************************************
	/// Control playerShip's position with touch position parameters
	///***********************************************************************
	void touchControl (){
        dir.x = Mathf.Cos(mJoystick.currentHeadingRad);
		dir.y = Mathf.Sin(mJoystick.currentHeadingRad);
		if(dir.sqrMagnitude > 1) 
			dir.Normalize();	
		dir *= Time.deltaTime;
		transform.Translate(dir * speed);
    }
	


	void playSfx ( AudioClip _sfx  ){
		GetComponent<AudioSource>().clip = _sfx;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}

}
