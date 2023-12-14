using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _gameMusicClip;
    [SerializeField] private AudioClip _menuMusicClip;
    [SerializeField] private AudioClip _footstepsClip;

    // Start is called before the first frame update
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioManager:: AudioSource is null");
        }

    }

    public void PlayMenuMusic()
    {
        _audioSource.clip = _menuMusicClip;
        _audioSource.Play();
    }
    public void PlayGameMusic()
    {
        _audioSource.clip = _gameMusicClip;
        _audioSource.Play();
    }
    public void PlayFootSteps()
    {
        _audioSource.clip = _footstepsClip;
        _audioSource.Play();
    }
}
