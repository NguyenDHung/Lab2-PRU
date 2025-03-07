using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLineEnd : MonoBehaviour
{
	[Tooltip("Event invoked when player crosses finish line.")]
	public UnityEvent FinishLineCrossedEvent;

	[Tooltip("GameObjects to interact with.")]
	public GameObject[] TriggerCandidates;

	public GameObject EndMenu;

	private HashSet<GameObject> triggerCandidates;

	private void Awake()
	{
		this.triggerCandidates = new HashSet<GameObject>(this.TriggerCandidates);
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.triggerCandidates.Contains(other.gameObject))
		{
			int currentScore = ScoreDisplay.instance.GetScore();
			PlayerPrefs.SetInt("PlayerScore", currentScore); 
			PlayerPrefs.Save(); // L?u l?i thay ??i
			this.FinishLineCrossedEvent.Invoke();

			EndMenu.SetActive(true);
			Time.timeScale = 0f;
		}
	}
}
