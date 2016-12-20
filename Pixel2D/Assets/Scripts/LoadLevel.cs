using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	//using UnityEngine.SceneManagement;
	private bool restart;
	//public float SceneName;

	//void Start () {
	//SceneManager.LoadScene ("SceneName", LoadSceneMode.Additive);

//	void Update ()
//	{
//if (restart)
//	{
//if (Input.GetKeyDown (KeyCode.R))
//{
//	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//}
//}
//}

public void clickMe ()
{
	Application.LoadLevel ("Pixel_2D_Sprites");
}
}