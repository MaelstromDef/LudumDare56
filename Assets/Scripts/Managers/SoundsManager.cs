using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;
    AudioSource musicSource;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void ToggleSound(bool on)
    {
        if(on) musicSource.Play();
        else musicSource.Pause();
    }
}
