using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private AudioManager _audio;
    public DaysTimer daysTimer;
    [SerializeField] private PlayerActions _playerActions;
    public LevelManager lm;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _decisionDecisions;
    [SerializeField] private TMP_Text _decision_text;

    void Start()
    {

        _audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level1" || scene.name == "Level2")
        {
            _audio.PlayGameMusic();
        }
        daysTimer = GameObject.Find("Timer").GetComponent<DaysTimer>();
        daysTimer.isStopTimeActive = false;
        _decisionDecisions.SetActive(false);

    }

    private void Update()
    { 
        if (lm._phase1Active == true)
        {
            LoadInstructions2();
        }
        else if (lm._phase2Active == true)
        {
            CrucialDecision();
        }

    }


    public void CrucialDecision()
    {
        _decisionDecisions.SetActive(true);
    }
    public void LoadInstructions2()
    {
;
        lm._phase1Active = false;
        lm._phase2Active = false;
        lm._phase3Active = true;
        SceneManager.LoadScene("Lvl2Instructions");

    }
}
