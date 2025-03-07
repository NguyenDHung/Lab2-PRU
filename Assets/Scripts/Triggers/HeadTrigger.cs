using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeadTrigger : MonoBehaviour
{
	private AudioManager audioManager;
	public BoolVariable IsAlive;

	[Tooltip("Event invoked when collision occurs.")]
	public UnityEvent HeadCollisionEvent;

	[Tooltip("GameObjects to interact with.")]
	public GameObject[] TriggerCandidates;

	public IntVariable startingHealth;
	public GameObject Player;
	public GameObject EndMenu;
	public int currentHealth { get; set; }

	private HashSet<GameObject> triggerCandidates;

	private void Awake()
	{
		currentHealth = startingHealth.Value;
		this.triggerCandidates = new HashSet<GameObject>(this.TriggerCandidates);
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	private void Update()
	{
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.triggerCandidates.Contains(other.gameObject) && this.IsAlive.Value)
		{
			TakeDamge();
		}
	}


	private void Knockback()
	{
		float knockbackDistanceX = 5f;
		float knockbackDistanceY = 10f;

		Vector3 safePosition = Player.transform.position + new Vector3(knockbackDistanceX, knockbackDistanceY, 0);

		Player.transform.position = safePosition;
		Player.transform.rotation = Quaternion.identity;
	}

	public void TakeDamge()
	{
		currentHealth -= 1;
		if (currentHealth > 0)
		{
			// Take damge
			this.HeadCollisionEvent.Invoke();
			Knockback();
			audioManager.PlaySFX(audioManager.umph);
		}
		else
		{
			// Player dead
			this.HeadCollisionEvent.Invoke();
			audioManager.PlaySFX(audioManager.ough);
			IsAlive.Value = false;
			//EndMenu.SetActive(true);

			GameManagerScript.instance.DisplayEndMenu();

			Time.timeScale = 0.0f;
		}
	}
}
