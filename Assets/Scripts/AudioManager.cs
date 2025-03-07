using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // luu audio source
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    // luu am thanh
    public AudioClip backgroundClip;
    public AudioClip jump;
    public AudioClip ough;
    public AudioClip umph;
    public AudioClip dapDat;
    public AudioClip lonVong;
    public AudioClip tangToc;
    public AudioClip truotTuyet;
    public AudioClip getCoin;

    void Awake()
    {
        // Kiểm tra nếu đã có instance thì hủy đối tượng mới để tránh trùng lặp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Giữ đối tượng qua các scene
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicAudioSource.clip = backgroundClip;
        musicAudioSource.loop = true; // bat che do lap
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.PlayOneShot(sfxClip);
    }

}
