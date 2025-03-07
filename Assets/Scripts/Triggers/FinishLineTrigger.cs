using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FinishLineTrigger : MonoBehaviour
{
    [Tooltip("Event invoked when player crosses finish line.")]
    public UnityEvent FinishLineCrossedEvent;

    [Tooltip("GameObjects to interact with.")]
    public GameObject[] TriggerCandidates;

    private HashSet<GameObject> triggerCandidates;

    public string nextSceneName = "NextScene";
    public float delayBeforeSceneChange = 3f;

    private void Awake()
    {
        this.triggerCandidates = new HashSet<GameObject>(this.TriggerCandidates);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.triggerCandidates.Contains(other.gameObject))
        {
			int currentScore = ScoreDisplay.instance.GetScore();
			PlayerPrefs.SetInt("PlayerScore", currentScore); // Lưu điểm vào PlayerPrefs
			PlayerPrefs.Save(); // Lưu lại thay đổi

			this.FinishLineCrossedEvent.Invoke();
            StartCoroutine(TransitionToNextScene());
        }
    }
    private IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(delayBeforeSceneChange); // Chờ hiệu ứng
        SceneManager.LoadScene(nextSceneName); // Chuyển Scene
    }
}
