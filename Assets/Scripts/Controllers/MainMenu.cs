using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(1);

		PlayerPrefs.SetInt("PlayerScore", 0);
		PlayerPrefs.Save();
		ScoreDisplay.instance.setScore(0);
	}
	public void QuitGame()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false; // Thoát Play Mode trong Unity Editor
		#else
			Application.Quit(); // Thoát game khi build thành file th?c thi
		#endif	
	}
}
