using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class GrowthPanelManager : MonoBehaviour
{
    //Game objects linked 
    public GameObject AccessoriesPanel;
    public GameObject GrowthPanel;
    public GameObject LayoutPanel;
    public GameObject GrowthItemPrefab;

    public GameObject[] GrowthItemCell = new GameObject[9];

    public bool isShowingDescription = false;

    public TextAsset GrowthItemsJSON;

    public GrowthItemShop myGrowthItemShop = new GrowthItemShop();

    public int pageCounter = 0;
    public int numberOfPages;

    public void InstantiateShopObjects(GrowthItemShop growthItemShop, int pageCounter) 
    {
        GrowthItem[] pageToDisplay = new GrowthItem[9];
        
        for (int i = 0; i < 9; i++) 
        {
            //Fills the pageToDisplay array with 9 items to display. Loads pageCounter serves as a starting point, increments and decrements by 9 every time NextPage or PreviousPage method is called
            if ( i < growthItemShop.growthItems.Length) 
            {
                if (myGrowthItemShop.growthItems[i+pageCounter] != null) { pageToDisplay[i] = myGrowthItemShop.growthItems[i + pageCounter]; }
               
            }
                       
        }

        //Note that these are called by their order as children in the inspector. Changing that order will screw with things
        //Values in the JSON are stored as integers, and are converted to string here (price and amount available)
        for (int i = 0; i <  9; i++) 
        {
            if (pageToDisplay[i].isUnlocked)
            {
                //Gets the sprite if the item is unlocked
                GrowthItemCell[i].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(pageToDisplay[i].pathtoimage);
            }
            else 
            {
                GrowthItemCell[i].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("GrowthItemImages/Question Mark");
            }
          

            //Gets the cost
            GrowthItemCell[i].transform.GetChild(1).GetComponent<Text>().text = pageToDisplay[i].price.ToString();

            //Gets the amount available
            GrowthItemCell[i].transform.GetChild(3).transform.GetComponentInChildren<Text>().text = pageToDisplay[i].amountavailable.ToString();

            //Gets the description
            GrowthItemCell[i].transform.GetChild(4).transform.GetComponentInChildren<Text>().text = pageToDisplay[i].description;
        }

        


    }

    public void NextPage() 
    {   
        //Checks if there is a next page to load, if yes, increments the pageCounter by and calls the InstantiateShopObjects method
        if (pageCounter != myGrowthItemShop.growthItems.Length - 9) 
        {
            pageCounter += 9;
            InstantiateShopObjects(myGrowthItemShop, pageCounter);
        }
        
        
    }

    public void PreviousPage()
    {
        //Checks if there is a previous page to load, if yes, increments the pageCounter by and calls the InstantiateShopObjects method
        if (pageCounter != 0) 
        {
            pageCounter -= 9;
            InstantiateShopObjects(myGrowthItemShop, pageCounter);
        }
        
    }

    // Serializes a class that consists of an array of GrowthItem objects (necessary to load/store the list of JSON database of growth items)
    [System.Serializable]
    public class GrowthItemShop 
    {
        public GrowthItem[] growthItems;
    }

    //Serializes GrowthItem (necessary to load/store items form the list of JSON growth items)
    [System.Serializable]
    public class GrowthItem 
    {
       public string name;
       public string pathtoimage;
       public string description;
       public int price;
       public int growthvalue;
       public int amountavailable;
       public bool isUnlocked;
    }

    //Whenever an item is clicked its description is setActive, otherwise the description is turned off. All descriptions are pre-loaded, but are setInactive in the start
    //Note that the description is called by its' order in the inspector. Changing that order will break this method
    public void ShowDescription()
    {
        if (!isShowingDescription) { EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(4).gameObject.SetActive(true); isShowingDescription = true; }
        else{
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(4).gameObject.SetActive(false);
            isShowingDescription = false;
        }

        Debug.Log(isShowingDescription);
    }



    //Start function most notably fetches the JSON file and loads in the growth items, then instantiates the first page even before the shop is accessed
    void Start()
    {
        myGrowthItemShop = JsonUtility.FromJson<GrowthItemShop>(GrowthItemsJSON.text);
        InstantiateShopObjects(myGrowthItemShop, pageCounter);
        numberOfPages = myGrowthItemShop.growthItems.Length / 9;
    }

    //Switches to the accessories panel. Despite being simillar the panels are anticipated to have different functionalities, and the differences in properties of accessories and growth items would mess with Serialization.
    //Alternatively, storing both types of items in one file is possible, but due to already existing difficulties in navigation further messiness is undesirable
    public void GoToAccessoriesPanel() 
    {
        GrowthPanel.SetActive(false);
        AccessoriesPanel.SetActive(true);
    }

}
