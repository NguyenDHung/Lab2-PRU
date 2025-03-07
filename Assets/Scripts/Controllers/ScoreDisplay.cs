using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
	public static ScoreDisplay instance;
	private TextMeshProUGUI textMeshPro;
	private int score = 0;
	private int maxScore = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Awake()
	{
		instance = this;
	}
	void Start()
	{
		textMeshPro = GetComponent<TextMeshProUGUI>(); // Gán đúng component
		UpdateScore();
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void AddScore(int amount)
	{
		score += amount;
		UpdateScore();
	}

	private void UpdateScore()
	{
		textMeshPro.text = "Score: " + score; // Cập nhật điểm số đúng cách
	}

	public int GetScore()
	{
		return score;
	}
	public int GetMaxScore()
	{
		return maxScore;
	}
	public void SetMaxScore()
	{
		maxScore = score;
	}

	public void setScore(int score)
	{
		this.score = score;
	}
}
