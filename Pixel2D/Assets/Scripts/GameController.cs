using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public int hazardCount;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start ()
	{
		gameOver = false;
		restart = true;
		//restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();

	}

	//void Update ()
	//{
	//	if (restart)
	//	{
	//		if (Input.GetKeyDown (KeyCode.R))
	//		{
	//			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	//		}
	//	}
	//}



	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void Restart ()
	{
		restartText.text = "Restart";
		restart = true;
	}
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}