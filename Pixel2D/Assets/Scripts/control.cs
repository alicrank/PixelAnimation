using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour {

public float moveSpeed = 10f;
bool facingRight = true;

// Use this for initialization
void Start () {


}

// Update is called once per frame
void FixedUpdate () {
	//Moves Forward and back along z axis                           //Up/Down
	transform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Vertical")* moveSpeed);
	//Moves Left and right along x Axis                               //Left/Right
	transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal")* moveSpeed);      

		float move = Input.GetAxis ("Horizontal");


		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}