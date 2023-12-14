using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaysTimer : MonoBehaviour
{
    public int _days = 1095;  //3 years till reinforcements arrive
    private LevelManager lm;
    [SerializeField] private TMP_Text _daysText;
    [SerializeField] private bool _stopTime;
    public bool isStopTimeActive;
    Coroutine _time;

    private void Update()
    {
        Time();

        if (_days == 0 && lm._phase1Started == true)
        {
            Debug.Log("end game");
        }
    }
    public void Time()
    {
        if (isStopTimeActive == false) // then time is running
        {
            _stopTime = false;    // then time is running
            if (_time == null)
            {
                _time = StartCoroutine(DaysCountdown());  //start countdown
            }
        }
        else
        {
            _stopTime = true;
            StopAllCoroutines();
            _time = null;
        }
    }


    IEnumerator DaysCountdown()
    {
        _daysText = GameObject.Find("DaysCountdown").GetComponent<TMP_Text>();

        while (_stopTime == false) //whlie time is running
        {
            yield return new WaitForSeconds(.3f);

            _days -= 1;  //days start counting down
            _daysText.text = "Arriving in days: " + _days;
        }
    }

    IEnumerator DaysCountUp()
    {
        _daysText = GameObject.Find("DaysCountdown").GetComponent<TMP_Text>();
        while (_stopTime == false) //whlie time is running
        {
            yield return new WaitForSeconds(.3f);
            if (lm._phase3Active == true)
            {
                _days += 1;  //days start counting up
                _daysText.text = "Victory in days: " + _days;
            }
        }
    }
}
