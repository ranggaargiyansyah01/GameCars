using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour 
{
	public void ReplyGame()
	{
		SceneManager.LoadScene ("MathCars");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
