using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private int levelMap = 1;
	public GameObject scoreDisplay;

	public TextMeshProUGUI score;
	public static GameManagerScript instance;
	public GameObject endMenu;

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		scoreDisplay.SetActive(true);
		int savedScore = PlayerPrefs.GetInt("PlayerScore");
		ScoreDisplay.instance.setScore(savedScore);
	}


	// Update is called once per frame
	void Update()
	{

	}

	public void DisplayEndMenu()
	{
		endMenu.SetActive(true);
		score.text = "Score: " + ScoreDisplay.instance.GetScore();
	}

	public void DisplayScore(bool isDisplay)
	{
		scoreDisplay.SetActive(isDisplay);
	}

	private void SetLevelMap()
	{
		levelMap += 1;
	}
	private int GetLevelMap()
	{
		return levelMap;
	}
}
