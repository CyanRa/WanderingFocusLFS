using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class AccessoriesPanelManager : MonoBehaviour
{
    public GameObject AccessoriesPanel;
    public GameObject GrowthPanel;

    public AccessoryShop myAccessoryShop = new AccessoryShop();

    public TextAsset accessoryJSON;

    public GameObject[] AccessoryCells = new GameObject[9];

    public int pageCounter = 0;

    public void InstantiateShopObjects(AccessoryShop accessoryShop, int pageCounter)
    {
        Accessory[] pageToDisplay = new Accessory[9];

        for (int i = 0; i < 9; i++)
        {
            //Fills the pageToDisplay array with 9 items to display. Loads pageCounter serves as a starting point, increments and decrements by 9 every time NextPage or PreviousPage method is called
            if (i < accessoryShop.accessory.Length)
            {
                pageToDisplay[i] = myAccessoryShop.accessory[i + pageCounter];
                
            }

        }

        //Note that these are called by their order as children in the inspector. Changing that order will screw with things
        //Values in the JSON are stored as integers, and are converted to string here (price and amount available)
        for (int i = 0; i < 9; i++)
        {
            if (pageToDisplay[i].isUnlocked)
            {
                //Gets the sprite if the item is unlocked
                AccessoryCells[i].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(pageToDisplay[i].pathtoimage);
                Debug.Log(pageToDisplay[i].pathtoimage);
            }
            else
            {
                AccessoryCells[i].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("GrowthItemImages/Question Mark");
            }


            //Gets the cost
            AccessoryCells[i].transform.GetChild(1).GetComponent<Text>().text = pageToDisplay[i].price.ToString();

            //Gets the description
            AccessoryCells[i].transform.GetChild(3).transform.GetComponentInChildren<Text>().text = pageToDisplay[i].description;
        }




    }

    public void NextPage()
    {
        //Checks if there is a next page to load, if yes, increments the pageCounter by and calls the InstantiateShopObjects method
        if (pageCounter != myAccessoryShop.accessory.Length - 9)
        {
            pageCounter += 9;
            InstantiateShopObjects(myAccessoryShop, pageCounter);
        }


    }

    public void PreviousPage()
    {
        //Checks if there is a previous page to load, if yes, increments the pageCounter by and calls the InstantiateShopObjects method
        if (pageCounter != 0)
        {
            pageCounter -= 9;
            InstantiateShopObjects(myAccessoryShop, pageCounter);
        }

    }

    // Serializes a class that consists of an array of Accessory objects (necessary to load/store the list of JSON database of growth items)
    [System.Serializable]
    public class AccessoryShop
    {
        public Accessory[] accessory;
    }

    //Serializes Accessory (necessary to load/store items form the list of JSON growth items)
    [System.Serializable]
    public class Accessory
    {
        public string name;
        public string pathtoimage;
        public string description;
        public int price;
        public bool isBought;
        public bool isUnlocked;
    }

    void Start()
    {
        myAccessoryShop = JsonUtility.FromJson<AccessoryShop>(accessoryJSON.text);
        InstantiateShopObjects(myAccessoryShop, pageCounter);
        GrowthPanel.SetActive(false);
    }

    //Switches to the accessories panel. Despite being simillar the panels are anticipated to have different functionalities, and the differences in properties of accessories and growth items would mess with Serialization.
    //Alternatively, storing both types of items in one file is possible, but due to already existing difficulties in navigation further messiness is undesirable
    //WARNING: NAMES ARE INVERTED FOR NO REASON
    public void GoToGrowthPanel()
    {
        GrowthPanel.SetActive(true);
        AccessoriesPanel.SetActive(false);
    }

}
