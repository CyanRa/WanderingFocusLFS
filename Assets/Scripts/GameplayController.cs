using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class GameplayController : MonoBehaviour
{
    public MainController myMaincontroller;

    public Constellation myConstellation = new Constellation();
    public Text constellationName;
    public Text timer;

    public int setTimer;     

    //Transforms the string of time to integer value and returns it
    public int TimeStringToInt(string timeInString) 
    {

        int timeInInt = 0;
        if (timeInString == "00:15:00") { timeInInt = 900; }
        else if (timeInString == "00:20:00") { timeInInt = 1200; }
        else if (timeInString == "00:25:00") { timeInInt = 1500; }
        else if (timeInString == "00:30:00") { timeInInt = 1800; }
        else if (timeInString == "00:35:00") { timeInInt = 2100; }
        else if (timeInString == "00:40:00") { timeInInt = 2400; }
        else if (timeInString == "00:45:00") { timeInInt = 2700; }
        else if (timeInString == "00:50:00") { timeInInt = 3000; }
        else if (timeInString == "00:55:00") { timeInInt = 3300; }
        else if (timeInString == "01:00:00") { timeInInt = 3600; }

        return timeInInt;
    }

    public class Constellation 
    {
        public string name;
        public string description;
        public int id;
        public string focusorbs;
        public int numberofstars;
        public string pathtoimage;
        public bool isdiscovered;
    }

    //transforms "angle" into a string (time) to display and returns it
    public string translateTimer(string untranslatedTime)
    {
        int untTimeInt = int.Parse(untranslatedTime);
        string timeToDisplay = "00:00:00";
        


        if (untTimeInt < 105) { timeToDisplay = "00:15:00"; } 
        else if (untTimeInt < 135) { timeToDisplay = "00:20:00"; } 
        else if (untTimeInt < 165) { timeToDisplay = "00:25:00"; } 
        else if (untTimeInt < 195) { timeToDisplay = "00:30:00"; } 
        else if (untTimeInt < 225) { timeToDisplay = "00:35:00"; } 
        else if (untTimeInt < 255) { timeToDisplay = "00:40:00"; } 
        else if (untTimeInt < 285) { timeToDisplay = "00:45:00"; } 
        else if (untTimeInt < 315) { timeToDisplay = "00:50:00"; } 
        else if (untTimeInt < 345) { timeToDisplay = "00:55:00"; } 
                              else { timeToDisplay = "01:00:00"; }


        setTimer = TimeStringToInt(timeToDisplay);
        return timeToDisplay;
    }


}
