using UnityEngine;

public class AudioController : MonoBehaviour
{
    // The static instance of the AudioController
    private static AudioController instance;

    // Audio sources or other audio-related members can be defined here
    public AudioSource backgroundMusic;
    public AudioSource soundEffect;

    // Public property to access the instance
    public static AudioController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioController>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(AudioController).Name);
                    instance = singleton.AddComponent<AudioController>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void SetMusicVolume(float vol)
    {
        backgroundMusic.volume = Mathf.Clamp(vol, 0, 1);
    }

    public void SetSFXVolume(float vol)
    {
        soundEffect.volume = Mathf.Clamp(vol, 0, 1);
    }
    public void Start()
    {
        PlayBackgroundMusic();
    }
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void PlaySoundEffect(AudioClip audio)
    {
        if (soundEffect != null)
        {
            soundEffect.PlayOneShot(audio);
        }
    }
}