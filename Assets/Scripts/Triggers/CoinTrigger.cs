using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
	private AudioManager audioManager;
	[Tooltip("GameObjects to interact with.")]
	public GameObject[] TriggerCandidates;

	private HashSet<GameObject> triggerCandidates;
	private bool isCollected = false;
	private void Awake()
	{
		this.triggerCandidates = new HashSet<GameObject>(this.TriggerCandidates);
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.triggerCandidates.Contains(other.gameObject)&& !isCollected)
		{
			audioManager.PlaySFX(audioManager.getCoin);
			isCollected = true;
			ScoreDisplay.instance.AddScore(1);
			Destroy(gameObject);
		}
	}
}
