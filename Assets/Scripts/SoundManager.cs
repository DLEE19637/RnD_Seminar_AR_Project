using UnityEngine;
using Unity;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private AudioSource soundEffectSource;

    [SerializeField]
    private AudioSource uiSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip audioCLip)
    {
        soundEffectSource.PlayOneShot(audioCLip);
    }

    public void PlayUISound(AudioClip audioCLip)
    {
        uiSource.PlayOneShot(audioCLip);
    }
}
