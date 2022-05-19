using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ObjectClick : MonoBehaviour
{

    public Dropdown dd;
    public GameObject furniture;
    public Button changemodel;
    public Button rotater;
    public String ownvalue;
    string filepathguesses;
    public int rotatecount;
    List<string[]> guesses = new List<string[]>();
    List<string> categories = new List<string> { "armchair",  "bathroom-sink", "bath", "bed", "chair", "cooker", "door", "kitchen-sink", "shower", "sofa", "table", "urinal", "WC"};
    List<string> plusobjects = new List<string> { "bookcase",  "counter", "coffe-table", "l-table", "nightstand", "plant", "TV", "wardrobe"};

    private void Start()
    {
        rotatecount = 0;
        GameObject playerUI = GameObject.FindGameObjectWithTag("Canvas"); // The canvas
        furniture = GameObject.Find("Furniture");
        dd = playerUI.GetComponentInChildren<Dropdown>();    
        dd.ClearOptions();
        rotater = playerUI.GetComponentsInChildren<Button>()[0];
        changemodel = playerUI.GetComponentsInChildren<Button>()[1];
        filepathguesses = PlayerPrefs.GetString("predictionData");

        var filelinesguess = File.ReadAllLines(@filepathguesses);
        foreach (var line in filelinesguess)
        {
            string[] guess = line.Split(' ');
            guesses.Add(guess);
        }
    }
    void Update()
    {
        // Check for mouse input
        
        
    }

    void OnMouseDown()
    {
        
        int name = Int32.Parse(gameObject.name);
        rotater.enabled = true;
        if (gameObject.tag == "sofa" ||gameObject.tag == "bath")
        {
            changemodel.enabled = true;
        }
        else
        {
            changemodel.enabled = false;

        }

        List<string> listguesses = new List<string>(guesses[name]);
        listguesses.RemoveAt(3);
        listguesses.Add("Possible objects:");
        foreach (var category in categories)
        {
            if (!listguesses.Contains(category))
            {
                listguesses.Add(category);
            }
        }
        listguesses.Add("Other objects:");
        foreach (var obj in plusobjects)
        {
            listguesses.Add(obj);
        }
        int index = listguesses.FindIndex(a => a == gameObject.tag);
        dd.ClearOptions();
        dd.AddOptions(listguesses);
        furniture.GetComponent<FurniturePlacer>().isinitailized = true;
        dd.value = index;
        dd.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(dd.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta.x, 300); 
        furniture.GetComponent<FurniturePlacer>().isinitailized = false;
        furniture.GetComponent<FurniturePlacer>().selectedobject = gameObject.name;
        
       

    }
   
}
