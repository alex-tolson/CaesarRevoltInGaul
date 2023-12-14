using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private int _startingTroopCount = 50000;
    [SerializeField] private int _startingReserveTroopCount = 30000;
    public GameManager gameManager;
    public int idleTroopCount;
    [SerializeField] private TMP_Text _noMore;
    public LevelManager lm;
    private UIManager _uiManager;


    void Start()
    {
       
        _uiManager = GameObject.Find("UI").GetComponent<UIManager>();


        if (lm._phase2Active == true)
        {
            lm._phase1Active = false;
            idleTroopCount += _startingReserveTroopCount;
            
        }
        else
        {
            idleTroopCount = _startingTroopCount;
        }
        lm._phase1Active = false;
    }

    public void SubtractIdleTroops(int amountToSubtract)
    {
        idleTroopCount -= amountToSubtract;
        if (idleTroopCount <= 0)
        {
            idleTroopCount = 0;
            StartCoroutine(NOMore());
        }
        _uiManager.DisplayIdleTroops(idleTroopCount);
    }

    public void AddIdleTroops(int amountToAdd)
    {
        idleTroopCount += amountToAdd;
        if (lm._phase1Active)
        {
            if (idleTroopCount > 80000)
            {
                idleTroopCount = 80000;
                _uiManager.DisplayIdleTroops(idleTroopCount);
            }
        }
        else
        {
            if (idleTroopCount > 50000)
            {
                idleTroopCount = 50000;
                _uiManager.DisplayIdleTroops(idleTroopCount);
            }
        }
        

        _uiManager.DisplayIdleTroops(idleTroopCount);
    }

    IEnumerator NOMore()
    {
        _noMore.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _noMore.gameObject.SetActive(false);
  
    }

    public void ReserveTroopsArrive()
    {
            idleTroopCount += _startingReserveTroopCount;
    }

    public void StartNOMore()
    {
        StartCoroutine(NOMore());
    }




}
