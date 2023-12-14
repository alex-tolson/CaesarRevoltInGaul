using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private AudioManager _audio;
    public bool _phase1Active = false;
    public bool _phase2Active = false;
    public bool _phase3Active = false;
    public bool _phase1Started = false;


    private void Start()
    {

        _audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu" || scene.name == "GameOverMenu")
        {
            _audio.PlayMenuMusic();
        }
    }

    public void LoadFirstLevel()
    {
        _phase1Started = true;
        SceneManager.LoadScene("Level1");
    }

    public void  LoadInstructions()
    {
    SceneManager.LoadScene("Instructions");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

}
