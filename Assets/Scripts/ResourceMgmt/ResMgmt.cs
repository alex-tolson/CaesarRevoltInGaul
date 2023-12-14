using System.Collections;
using UnityEngine;

public class ResMgmt : MonoBehaviour
{
    private PlayerActions _playerActions;
    public GameObject _inputEmpty;
    [SerializeField] private string _inputTroops;
    [SerializeField] private int _currentBricks, _currentWalls, _currentWood, _currentTrenches, _currentTowers;
    [SerializeField] private int _amountInt;
    
    private int _bricksPerDay = 700;
    private int _usedBricks;
    private int _buildDaily = 500;
    private int _treeCutDaily = 6;
    private int _feetDugDaily = 15;
    private int _currentFeet;
    private int _tempWood;
    private int _structures;
    private int _miles;
    private int _towersDaily = 800;  //divide wo0d by 800 and that gives 1 tower
    private string buttonName;
    private UIManager _uiManager;

    public void StoreNumber(string s)
    {
        _inputTroops = s;
        Debug.Log("input value is " + s);
        int.TryParse(s, out int _outputInt);
        _amountInt = _outputInt;
        HideInputText();
        switch (buttonName)
        {
            case "MakeBricks_button":
                {
                    MakeBricks();
                    break;
                }
            case "BuildWall_button":
                {
                    BuildWall();
                    break;
                }
            case "DigTrench_button":
                {
                    DigTrenches();
                    break;
                }
            case "Chop_wood_button":
                {
                    ChopWood();
                    break;
                }
            case "BuildTower_button":
                {
                    BuildTower();
                    break;
                }
            default:
                {
                    Debug.Log("Invalid entry");
                    break;
                }
        }
    }

    void Start()
    {  
        _playerActions = GameObject.Find("Player").GetComponent<PlayerActions>();
        if (_playerActions == null)
        {
            Debug.LogError("ResMgmt:: Player Actions is null");
        }
        //
        _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("ResMgmt:: UIManager is null");
        }
    }

    public void ShowInputText()
    {
        _inputEmpty.gameObject.SetActive(true);
        var thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        buttonName = thisButton.name;
        Debug.Log("button name is " + buttonName);
    }

    IEnumerator MakeBricksCo(int amount)
    {
        int days = 0;
        if (_playerActions.idleTroopCount != 0)
        {
            _playerActions.SubtractIdleTroops(amount);  //stores number and subtracts it from idle troops

            while (days <= 90)
            {
                yield return new WaitForSeconds(.3f);

                days++;
            }
            _currentBricks += amount * _bricksPerDay;

            Debug.Log("current bricks is " + _currentBricks);
            _playerActions.AddIdleTroops(amount);
            _uiManager.DisplayTotalBricks(_currentBricks);
        }
    }

    public void MakeBricks()
    {
        StartCoroutine(MakeBricksCo(_amountInt));
    }

    IEnumerator BuildWallsCo(int amount)
    {
        int days = 0;
        if (_playerActions.idleTroopCount != 0)
        {
            _playerActions.SubtractIdleTroops(amount);  //stores number and subtracts it from idle troops

            while (days <= 90)
            {
                yield return new WaitForSeconds(.3f);

                days++;
            }
            _usedBricks = amount * _buildDaily;  //bricks minus what's used to make wall daily

            if (_currentBricks < 0) //if bricks go below zero, reset to zero
            {
                _currentBricks = 0;
            }
            _currentWalls += amount * _buildDaily; //walls = amount created today
            _miles = (_currentWalls / 1000) / 1000;  //miles is walls / 1000
            _currentWalls = (_currentWalls % 1000); //walls equal remainder of walls/1000

            _playerActions.AddIdleTroops(amount);
            
            _uiManager.DisplayTotalWallsPhase1(_miles);
            _uiManager.DisplayTotalBricksLeftover(_usedBricks);
        }
        else
        {
            _playerActions.StartNOMore();
        }
    }

    public void BuildWall()
    {
        StartCoroutine(BuildWallsCo(_amountInt));
    }

    IEnumerator DigTrenchesCo(int amount)
    {
        int days = 0;
        if (_playerActions.idleTroopCount != 0)
        {
            _playerActions.SubtractIdleTroops(amount);  //stores number and subtracts it from idle troops

            while (days <= 90)
            {
                yield return new WaitForSeconds(.3f);

                days++;
            }

            _currentFeet += amount * _feetDugDaily; //feet = what's been created daily

            _currentTrenches = (_currentFeet / 300) / 300;//trenches = feet/1000

            _playerActions.AddIdleTroops(amount);
            _uiManager.DisplayTotalTrenchesPhase1(_currentTrenches);
        }
        else
        {
            _playerActions.StartNOMore();
        }
    }

    public void DigTrenches()
    {
        StartCoroutine(DigTrenchesCo(_amountInt));
    }

    IEnumerator ChopWoodCo(int amount)
    {
        int days = 0;
        if (_playerActions.idleTroopCount != 0)
        {
            _playerActions.SubtractIdleTroops(amount);  //stores number and subtracts it from idle troops

            while (days <= 90)
            {
                yield return new WaitForSeconds(.3f);
                days++;
            }

            _currentWood += amount * _treeCutDaily;

            _playerActions.AddIdleTroops(amount);
            _uiManager.DisplayTotalWoodChopped(_currentWood);
        }
        else
        {
            _playerActions.StartNOMore();
        }
    }

    public void ChopWood()
    {
        StartCoroutine(ChopWoodCo(_amountInt));
    }

    IEnumerator BuildTowers(int amount)
    {
        int days = 0;
        if (_playerActions.idleTroopCount != 0)
        {
            _playerActions.SubtractIdleTroops(amount);  //stores number and subtracts it from idle troops

            while (days <= 90)
            {
                yield return new WaitForSeconds(.3f);

                days++;
            }

            _tempWood = amount * _towersDaily; //wood minus what's used to make a tower daily
            _structures += amount * _towersDaily; // structure += what's used to build a tower daily

            if (_currentWood < 0) //if wood drops below 0, reset to 0
            {
                _currentWood = 0;
            }

            _currentTowers += (_structures / 1000) / 1000; //current towers = structure / 1000
            _structures -= (_structures % 1000); //structure = remainder after towers are built

            _playerActions.AddIdleTroops(amount); //return troops to idle
            _uiManager.DisplayTotalTowersPhase1(_currentTowers); //display towers number
            _uiManager.DisplayTotalWoodLeftover(_tempWood);
        }
        else
        {
            _playerActions.StartNOMore();
        }
    }

    public void BuildTower()
    {
        StartCoroutine(BuildTowers(_amountInt));
    }

    public void HideInputText()
    {
        _inputEmpty.gameObject.SetActive(false);
    }


}
