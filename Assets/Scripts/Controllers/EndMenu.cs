using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
	public TextMeshProUGUI score;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	public void Home()
	{
		SceneManager.LoadScene(0);
		Time.timeScale = 1.0f;
	}

	public void Restart()
	{
		SceneManager.LoadScene(1);
		Time.timeScale = 1.0f;
	}
}
