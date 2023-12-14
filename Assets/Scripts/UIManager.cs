using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public DaysTimer daysTimer;
    private GameManager _gameManager;
    private PlayerActions _playerActions;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private TMP_Text _bricks_count,_wall_count, _trenches_count, _wood_count, _towers_count, _idleTroopCount; 
    private int _currentBricks, _currentWalls, _currentTrenches, _currentWood, _currentTowers;
    [SerializeField] public bool _level1WallsMet, _level1TrenchesMet, _level1TowersMet, _level2WallsMet, _level2TrenchesMet, _level2TowersMet;
    [SerializeField] public bool _level1CriteriaMet, _level2CriteriaMet;
    public LevelManager lm;

    void Start()
    {

        _currentBricks = 0;
        _currentWalls = 0;
        _currentTrenches = 0;
        _currentWood = 0;
        _currentTowers = 0;
        DisplayIdleTroops(50000);
            
       
        _gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("UIManager:: Game Manager is null");
        }
        _playerActions = GameObject.Find("Player").GetComponent<PlayerActions>();
        if (_playerActions == null)
        {
            Debug.LogError("UIManager:: PlayerActions is null");
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            daysTimer.isStopTimeActive = !daysTimer.isStopTimeActive;
            _pauseButton.SetActive(!_pauseButton.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void DisplayTotalBricks(int totaBricksMade)
    {
        _currentBricks += totaBricksMade;
        _bricks_count.text = "Bricks: " + _currentBricks;
    }

    public void DisplayTotalWallsPhase1(int totalWallsBuilt)
    {
        _currentWalls += totalWallsBuilt;
        _wall_count.text = "Wall Miles: " + _currentWalls;
        if (_currentWalls > 10)
        {
            _level1WallsMet = true;
            _wall_count.color = Color.yellow;
            PassedPhase1();
        }
    }

    public void DisplayTotalWallsPhase2(int totalWallsBuilt)
    {
        _currentWalls += totalWallsBuilt;
        _wall_count.text = "Wall Miles: " + _currentWalls;

        if (_currentWalls > 14)
        {
            _level2WallsMet = true;
            _wall_count.color = Color.green;
            PassedPhase2();
        }
    }

    public void DisplayTotalTrenchesPhase1(int totalTrenchesDug)
    {
        _currentTrenches += totalTrenchesDug;
        _trenches_count.text = "Trenches: " + _currentTrenches;

        if (_currentTrenches > 9)
        {
            _level1TrenchesMet = true;
            _trenches_count.color = Color.yellow;
            PassedPhase1();
        }
    }
    public void DisplayTotalTrenchesPhase2(int totalTrenchesDug)
    {
        _currentTrenches += totalTrenchesDug;
        _trenches_count.text = "Trenches: " + _currentTrenches;
        if (_currentTrenches > 14)
        {
            _level2TrenchesMet = true;
            _trenches_count.color = Color.green;
            PassedPhase2();
            PassedPhase2();

        }
    }

    public void DisplayTotalWoodChopped(int totalWoodChopped)
    {
        _currentWood += totalWoodChopped;
        _wood_count.text = "Wood: " + _currentWood;
    }

    public void DisplayTotalTowersPhase1(int totalTowersMade)
    {
        _currentTowers += totalTowersMade;
        _towers_count.text = "Towers: " + _currentTowers;

        if (_currentTowers > 10 )
        {
            _level1TowersMet = true;
            _towers_count.color = Color.yellow;

            PassedPhase1();
        }

    }
    public void DisplayTotalTowersPhase2(int totalTowersMade)
    {
        _currentTowers += totalTowersMade;
        _towers_count.text = "Towers: " + _currentTowers;
        if (_currentTowers > 14)
        {
            _level2TowersMet = true;
            _towers_count.color = Color.green;
            PassedPhase2();

        }
    }

    public void DisplayIdleTroops(int idleTroopsLeft)
    {
        _idleTroopCount.text = "Idle Troops Left: " + idleTroopsLeft;
    }

    public void DisplayTotalWoodLeftover(int leftovers)
    {
        _currentWood -= leftovers;
        if (_currentWood < 0)
        {
            _currentWood = 0;
        }
        _wood_count.text = "Wood: " + _currentWood;
    }

    public void DisplayTotalBricksLeftover(int leftovers)
    {
        _currentBricks -= leftovers;
        if (_currentBricks < 0)
        {
            _currentBricks = 0;
        }
        _bricks_count.text = "Bricks: " + _currentBricks;
    }

    public void PassedPhase1()
    {
        if ((_level1WallsMet) && ( _level1TrenchesMet) && (_level1TowersMet)) //else check if phase 1 should be on
        {
            lm._phase1Active = true;
            _playerActions.ReserveTroopsArrive();
            DisplayIdleTroops(_playerActions.idleTroopCount);
        }
    }

    public void PassedPhase2()
    {
        if ((_level2WallsMet) && (_level2TrenchesMet) && (_level2TowersMet))
        {
            lm._phase2Active = true;

        }
    }

    public void PlayerWon()
    {
        SceneManager.LoadScene("Win");
    }
    public void PlayerLost()
    {
        SceneManager.LoadScene("Lose");
    }
}
