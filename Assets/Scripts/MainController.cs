using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class MainController : MonoBehaviour
{
    public GameplayController myGameplayController;

    public GameObject MainScreen; 
    public GameObject NightSkyScreen;
    public GameObject FocusShopScreen;
    public GameObject SpiritPetScreen;
    public GameObject CollectionScreen;
    public GameObject ChosenConstellationScreen;

    public GameObject SmallConstellation;
    public GameObject MediumConstellation;
    public GameObject BigConstellation;

    public GameObject ChosenConstellationDisplay;


    static int focusOrbs = 15;
    static string chosenConstellation = "None Chosen";

    public Text FocusOrbsCounter;
    
    public TextAsset constellationJSON;

    public Constellation myConstellation = new Constellation();

    private Constellation[] ConstellationsForDisplay = new Constellation[3];

    public ConstellationList myConstellationList = new ConstellationList();

    void Start()
    {

        myConstellationList = JsonUtility.FromJson<ConstellationList>(constellationJSON.text);
        MainScreen.transform.SetAsLastSibling();
        FocusOrbsCounter.text = focusOrbs.ToString();
       

    }


    void Update()
    {

    }


    public class displayConstellation 
    {
        public string name;
        public string path;

    
    }

    [System.Serializable]
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

    [System.Serializable]
    public class ConstellationList 
    {
        public Constellation[] constellation;
    }

    public void GoToMainScreen()
    {

        MainScreen.transform.SetAsLastSibling();

    }

    // Goes to night sky screen. If a constellation is already chosen, goes to Chosen Constellation screen
    public void GoToNightSkyScreen()
    {
        if (chosenConstellation != "None Chosen") { GoToChosenConstellationScreen(); } else { 
        NightSkyScreen.transform.SetAsLastSibling();
        ConstellationsForDisplay = ConstellationsToInstantiate();
        InstantiateConstillationsForDisplay(ConstellationsForDisplay);
    }

    }

    public void GoToChosenConstellationScreen()
    {
        
        int indexRaiser = 0;

        if (chosenConstellation == "None Chosen") { chosenConstellation = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text; } 

        Constellation myChosenConstellationClass = new Constellation();

        while (myChosenConstellationClass.name != chosenConstellation)
        {            
            if (myConstellationList.constellation[indexRaiser].name == chosenConstellation) { myChosenConstellationClass = myConstellationList.constellation[indexRaiser]; }
            indexRaiser++;
            
        }

        ChosenConstellationDisplay.GetComponent<Image>().sprite = Resources.Load<Sprite>(myChosenConstellationClass.pathtoimage);
        ChosenConstellationDisplay.GetComponentInChildren<Text>().text = myChosenConstellationClass.name;

        SetChosenConstellation(myChosenConstellationClass);
        
        ChosenConstellationScreen.transform.SetAsLastSibling();


    }

    public void GoToFocusShopScreen()
    {

        FocusShopScreen.transform.SetAsLastSibling();

    }

    public void GoToSpiritPetScreen()
    {

        SpiritPetScreen.transform.SetAsLastSibling();

    }

    public void GoToCollectionScreen()
    {

        CollectionScreen.transform.SetAsLastSibling();

    }


    //Generates a random array of class Constellation size 3 (1 small, 1 medium and 1 big) and returns it
    public Constellation[] ConstellationsToInstantiate()
    {
        Constellation[] ConstellationsToInstantiate = new Constellation[3];

        Constellation mySmallConstellation = new Constellation();
        Constellation myMediumConstellation = new Constellation();
        Constellation myBigConstellation = new Constellation();
        
        while (mySmallConstellation.name == null)
        {
            int smallconstellationrandomint = Random.Range(0, 8);
            if (myConstellationList.constellation[smallconstellationrandomint].focusorbs == "40") { mySmallConstellation = myConstellationList.constellation[smallconstellationrandomint]; }
            
        }


        while (myMediumConstellation.name == null)
        {
            int smallconstellationrandomint = Random.Range(0, 8);
            if (myConstellationList.constellation[smallconstellationrandomint].focusorbs == "80") { myMediumConstellation = myConstellationList.constellation[smallconstellationrandomint]; }

        }


        while (myBigConstellation.name == null)
        {
            int smallconstellationrandomint = Random.Range(0, 8);
            if (myConstellationList.constellation[smallconstellationrandomint].focusorbs == "120") { myBigConstellation = myConstellationList.constellation[smallconstellationrandomint]; }

        }

        //Sets the generated properties to prefabs in the scene
        ConstellationsToInstantiate[0] = mySmallConstellation;
        ConstellationsToInstantiate[1] = myMediumConstellation;
        ConstellationsToInstantiate[2] = myBigConstellation;

        return ConstellationsToInstantiate;
    }

    //sets the names, image and focusorbs for the 3 constellations in the "Night Sky"
    public void InstantiateConstillationsForDisplay(Constellation[] constellations) 
    {
       // Debug.Log(constellations[0].name);
       // Debug.Log(constellations[1].name);
       // Debug.Log(constellations[2].name);

        SmallConstellation.GetComponent<Image>().sprite = Resources.Load<Sprite>(constellations[0].pathtoimage);
        SmallConstellation.GetComponentInChildren<Text>().text = constellations[0].name;
        SmallConstellation.transform.GetChild(0).SetAsLastSibling();
        SmallConstellation.GetComponentInChildren<Text>().text = constellations[0].focusorbs;
        SmallConstellation.transform.GetChild(0).SetAsLastSibling();
       
        MediumConstellation.GetComponent<Image>().sprite = Resources.Load<Sprite>(constellations[1].pathtoimage);
        MediumConstellation.GetComponentInChildren<Text>().text = constellations[1].name;
        MediumConstellation.transform.GetChild(0).SetAsLastSibling();
        MediumConstellation.GetComponentInChildren<Text>().text = constellations[1].focusorbs;
        MediumConstellation.transform.GetChild(0).SetAsLastSibling();
       
        BigConstellation.GetComponent<Image>().sprite = Resources.Load<Sprite>(constellations[2].pathtoimage);
        BigConstellation.GetComponentInChildren<Text>().text = constellations[2].name;
        BigConstellation.transform.GetChild(0).SetAsLastSibling();
        BigConstellation.GetComponentInChildren<Text>().text = constellations[2].focusorbs;
        BigConstellation.transform.GetChild(0).SetAsLastSibling();
    }

    public void SetChosenConstellation(Constellation setConstellation) 
    {
        myGameplayController.myConstellation.name = setConstellation.name;
        myGameplayController.myConstellation.description = setConstellation.description;
        myGameplayController.myConstellation.id = setConstellation.id;
        myGameplayController.myConstellation.focusorbs = setConstellation.focusorbs;
        myGameplayController.myConstellation.numberofstars = setConstellation.numberofstars;
        myGameplayController.myConstellation.pathtoimage = setConstellation.pathtoimage;
        myGameplayController.myConstellation.isdiscovered = setConstellation.isdiscovered;
    }
}
