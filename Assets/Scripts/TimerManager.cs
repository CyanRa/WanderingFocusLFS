using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class TimerManager : MonoBehaviour
{
    public GameplayController myGameplayController;
    public RadialSlider myRadialSlider;

    public int timeToDisplay;
    public string timeToDisplayString;
    public int secondsToDisplay;

    public Text HoursText;
    public Text MinutesText;
    public Text SecondText;

    public bool isTimerOn = false;
    public bool isTimerSet = false;

    public float elapsed;
    public float timerSpeed = 2f;



    //TODO load in from saved constellation
    void Awake() 
    { 
        
    }
    
    void Start()
    {
        timeToDisplay = 900;
    }

    
    void Update()
    {
        if (!isTimerOn)
        {
            if (!isTimerSet) { timeToDisplay = myGameplayController.TimeStringToInt(myRadialSlider.textString); }
            
            HoursText.text = HoursToDisplay(timeToDisplay);
            MinutesText.text = MinutesToDisplay(timeToDisplay);
        }
        else {

            elapsed += Time.deltaTime;
            if(elapsed >= timerSpeed) 
            {
                HoursText.text = HoursToDisplay(timeToDisplay);
                MinutesText.text = MinutesToDisplay(timeToDisplay);
                SecondText.text = SecondsToDisplay(secondsToDisplay);

                elapsed = 0f;
                timeToDisplay -= 1;
                secondsToDisplay -= 1;
                
            }
            

        }
    }

    public string HoursToDisplay(int timeToDisplay)
    {
        if (timeToDisplay / 3600 == 0) { return "00"; }
        return (timeToDisplay / 3600).ToString();
    }

    public string MinutesToDisplay(int timeToDisplay)
    { 
        
        if (timeToDisplay / 60 == 60) { return "00"; }
        if (timeToDisplay == 0) { return "00"; }
        return (timeToDisplay / 60).ToString();

    }

    public string SecondsToDisplay(int timeToDisplay)
    {
        switch (timeToDisplay) 
        {
            case 0: secondsToDisplay = 60; return "00"; break;
            case 60: return "00"; break;
            case 9: return "09"; break;
            case 8: return "08"; break;
            case 7: return "07"; break;
            case 6: return "06"; break;
            case 5: return "05"; break;
            case 4: return "04"; break;
            case 3: return "03"; break;
            case 2: return "02"; break;
            case 1: return "01"; break;
            default: return timeToDisplay.ToString(); ; break;
        }


    } 

    public void TurnOnTimer() 
    {
        if (isTimerOn == false)
        {
            isTimerSet = true;
            isTimerOn = true;
            if (!isTimerSet) { secondsToDisplay = 60; }
            
        }
        else {
            if (!isTimerSet) { secondsToDisplay = 0; }
            
            isTimerOn = false;
        }
       
    }

   
}
