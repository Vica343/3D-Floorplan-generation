using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurniturePlacer : MonoBehaviour
{
    public GameObject Armchair;
    public GameObject Bath;
    public GameObject Bath2;
    public GameObject BathroomSink;
    public GameObject Bed;
    public GameObject Chair;
    public GameObject Cooker;
    public GameObject Door;    
    public GameObject KitchenSink;
    public GameObject Shower;
    public GameObject Sofa;
    public GameObject Sofa2;
    public GameObject Table;
    public GameObject Urinal;
    public GameObject WC;

    public GameObject Bookcase;
    public GameObject CoffeTable;
    public GameObject Counter;
    public GameObject Nightstand;
    public GameObject LTable;
    public GameObject TV;
    public GameObject Plant;
    public GameObject Wardrobe;

    int whichbath;
    int whichsofa;

    public Dropdown dd;
    public Button rotater;
    public Button changemodel;
    public Button newplan;
    public Button exit;

    List<string[]> guesses = new List<string[]>();
    List<string> directions = new List<string>();
    List<float[]> boundaries = new List<float[]>();

    public string selectedobject { get; set; }
    public bool isinitailized { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerUI = GameObject.FindGameObjectWithTag("Canvas"); // The canvas
        dd = playerUI.GetComponentInChildren<Dropdown>();
        rotater = playerUI.GetComponentsInChildren<Button>()[0];
        changemodel = playerUI.GetComponentsInChildren<Button>()[1];
        newplan = playerUI.GetComponentsInChildren<Button>()[2];
        exit = playerUI.GetComponentsInChildren<Button>()[3];
        changemodel.enabled = false;
        rotater.enabled = false;
        dd.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dd);
        });
        rotater.onClick.AddListener(RotateModel);
        changemodel.onClick.AddListener(ChangeModel);
        newplan.onClick.AddListener(NewPlan);
        exit.onClick.AddListener(EndApplication);

        string filepathguesses = PlayerPrefs.GetString("predictionData");
        string filepathboundaries = PlayerPrefs.GetString("boundaryData");
        string filepathdirections = PlayerPrefs.GetString("directionData");


        var filelinesguess = File.ReadAllLines(@filepathguesses);       
        foreach (var line in filelinesguess)
        {
            string[] guess = line.Split(' ');
            guesses.Add(guess);
        }
        var filelinesdirection = File.ReadAllLines(@filepathdirections);
        foreach (var line in filelinesdirection)
        {
            string direction = line.Trim();
            directions.Add(direction);
        }
        var filelinesboundary = File.ReadAllLines(@filepathboundaries);
        foreach (var line in filelinesboundary)
        {
            float[] boundary = new float[4];
            string[] boundarys = line.Split(' ');
            boundary[0] = float.Parse(boundarys[0], CultureInfo.InvariantCulture);
            boundary[1] = float.Parse(boundarys[1], CultureInfo.InvariantCulture);
            boundary[2] = float.Parse(boundarys[2], CultureInfo.InvariantCulture);
            boundary[3] = float.Parse(boundarys[3], CultureInfo.InvariantCulture);
            boundaries.Add(boundary);
        }
        for (int i = 0; i< guesses.Count; i++)
        {
            var guess = guesses[i];            
            addNewObject(guess[0], i);          

        }
        }
    

    void addNewObject(string name, int i)
    {
        GameObject furniture;
        switch (name)
        {
            case "table":
                furniture = Instantiate(Table, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "table";
                RescaleObject(furniture, i);
                break;
            case "armchair":
                furniture = Instantiate(Armchair, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "armchair";
                RescaleObject(furniture, i);
                break;
            case "bath":
                furniture = Instantiate(Bath, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "bath";
                whichbath = 1;
                RescaleObject(furniture, i);
                break;
            case "bath2":
                furniture = Instantiate(Bath2, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "bath";
                whichbath = 2;
                RescaleObject(furniture, i);
                break;
            case "bathroom-sink":
                furniture = Instantiate(BathroomSink, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "bathroom-sink";
                RescaleObject(furniture, i);
                break;
            case "bed":
                furniture = Instantiate(Bed, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "bed";
                RescaleObject(furniture, i);
                break;
            case "chair":
                furniture = Instantiate(Chair, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "chair";
                RescaleObject(furniture, i);
                break;
            case "door":
                furniture = Instantiate(Door, new Vector3((boundaries[i])[1], 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "door";
                RescaleObject(furniture, i);
                break;
            case "cooker":
                furniture = Instantiate(Cooker, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "cooker";
                RescaleObject(furniture, i);
                break;
            case "kitchen-sink":
                furniture = Instantiate(KitchenSink, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "kitchen-sink";
                RescaleObject(furniture, i);
                break;
            case "shower":
                furniture = Instantiate(Shower, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "shower";
                RescaleObject(furniture, i);
                break;
            case "sofa":
                furniture = Instantiate(Sofa, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "sofa";
                RescaleObject(furniture, i);
                whichsofa = 1;
                break;
            case "sofa2":
                furniture = Instantiate(Sofa2, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "sofa";
                RescaleObject(furniture, i);
                whichsofa = 2;               
                break;
            case "urinal":
                furniture = Instantiate(Urinal, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "urinal";
                RescaleObject(furniture, i);
                break;
            case "WC":
                furniture = Instantiate(WC, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "WC";
                RescaleObject(furniture, i);
                break;
            case "plant":
                furniture = Instantiate(Plant, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "plant";
                RescaleObject(furniture, i);
                break;
            case "counter":
                furniture = Instantiate(Counter, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "counter";
                RescaleObject(furniture, i);
                break;
            case "coffe-table":
                furniture = Instantiate(CoffeTable, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "coffe-table";
                RescaleObject(furniture, i);
                break;
            case "bookcase":
                furniture = Instantiate(Bookcase, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "bookcase";
                RescaleObject(furniture, i);
                break;
            case "wardrobe":
                furniture = Instantiate(Wardrobe, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "wardrobe";
                RescaleObject(furniture, i);
                break;
            case "TV":
                furniture = Instantiate(TV, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "TV";
                RescaleObject(furniture, i);
                break;
            case "nightstand":
                furniture = Instantiate(Nightstand, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "nightstand";
                RescaleObject(furniture, i);
                break;
            case "l-table":
                furniture = Instantiate(LTable, new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2)), Quaternion.identity);
                furniture.gameObject.tag = "l-table";
                RescaleObject(furniture, i);
                break;

        }
       
    }

    // Update is called once per frame
    void Update()
    {
        var toogles = dd.GetComponentsInChildren<Toggle>(true);
        if (toogles.Length > 16)
        {
            toogles[5].interactable = false;
            toogles[16].interactable = false;
        }
    }

    void DropdownValueChanged(Dropdown change)
    {
        if (!isinitailized)
        {
            GameObject oldfurniture = GameObject.Find(selectedobject);
            int i = Int32.Parse(selectedobject);
            oldfurniture.gameObject.tag = change.options[change.value].text;
            Destroy(oldfurniture);
            addNewObject(change.options[change.value].text, i);
        }
        
    }

    void NewPlan()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    void EndApplication()
    {
        Application.Quit();
    }

    void RotateModel()
    {               
        GameObject oldfurniture = GameObject.Find(selectedobject);
        int rotatecount = oldfurniture.GetComponent<ObjectClick>().rotatecount;
        int i = Int32.Parse(selectedobject);
        if (oldfurniture.tag == "door")
        {
            if (rotatecount % 2 == 1)
            {
                if (oldfurniture.transform.position.x == (boundaries[i])[1])
                {
                    oldfurniture.transform.position = new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, (boundaries[i])[0]);
                    oldfurniture.transform.Rotate(0, 90, 0);
                }
                else if (oldfurniture.transform.position.z == (boundaries[i])[0])
                {
                    oldfurniture.transform.position = new Vector3((boundaries[i])[1] + (boundaries[i])[3], 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2));
                    oldfurniture.transform.Rotate(0, 90, 0);

                }
                else if (oldfurniture.transform.position.x == (boundaries[i])[1] + (boundaries[i])[3])
                {
                    oldfurniture.transform.position = new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0] + ((boundaries[i])[2])));
                    oldfurniture.transform.Rotate(0, 90, 0);
                }
                else
                {
                    oldfurniture.transform.position = new Vector3((boundaries[i])[1], 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2));
                    oldfurniture.transform.Rotate(0, 90, 0);
                }
            }
            else
            {
                oldfurniture.transform.Rotate(0, 90, 0);
            }
           

        }
        else
        {
            oldfurniture.transform.Rotate(0, 90, 0);
        }
        oldfurniture.GetComponent<ObjectClick>().rotatecount++;


    }

    void ChangeModel()
    {
        GameObject oldfurniture = GameObject.Find(selectedobject);
        var selected = selectedobject;
        var tag = oldfurniture.gameObject.tag;        
        if (tag == "sofa")
        {
            Destroy(oldfurniture);
            if (whichsofa == 1)
            {
                
                addNewObject("sofa2", Int32.Parse(selectedobject));
                selectedobject = selected;
            }
            else
            {
                addNewObject("sofa", Int32.Parse(selectedobject));
                selectedobject = selected;
            }
        }
        else if (tag == "bath")
        {
            Destroy(oldfurniture);
            if (whichbath == 1)
            {

                addNewObject("bath2", Int32.Parse(selectedobject));
                selectedobject = selected;
            }
            else
            {
                addNewObject("bath", Int32.Parse(selectedobject));
                selectedobject = selected;
            }
        }
    }

   


    void RescaleObject(GameObject furniture, int i)
    {
        var size = furniture.GetComponent<Renderer>().bounds.size;
        Vector3 rescale = furniture.transform.localScale;

        if (directions[i] == "back")
        {
            furniture.transform.Rotate(0, 180, 0);
            rescale.x = (boundaries[i])[3] * rescale.x / size.x;
            rescale.z = (boundaries[i])[2] * rescale.z / size.z;
            rescale.y = Math.Min(rescale.x, rescale.z);
            furniture.transform.localScale = rescale;
        }
        else if (directions[i] == "right")
        {
            if (furniture.tag == "door")
            {
                furniture.transform.position = new Vector3((boundaries[i])[1] + ((boundaries[i])[3] / 2), 0, ((boundaries[i])[0] + ((boundaries[i])[2])));
                furniture.gameObject.tag = "door";
            }

            furniture.transform.Rotate(0, 90, 0);
            if ((((boundaries[i])[3] > (boundaries[i])[2]) && (size.x < size.z)) || (((boundaries[i])[2] > (boundaries[i])[3]) && (size.z < size.x)))
            {
                rescale.x = (boundaries[i])[2] * rescale.x / size.x;
                rescale.z = (boundaries[i])[3] * rescale.z / size.z;
                rescale.y = Math.Min(rescale.x, rescale.z);
                furniture.transform.localScale = rescale;
            }
            else
            {
                rescale.x = (boundaries[i])[3] * rescale.x / size.x;
                rescale.z = (boundaries[i])[2] * rescale.z / size.z;
                rescale.y = Math.Min(rescale.x, rescale.z);
                furniture.transform.localScale = rescale;
            }


        }
        else if (directions[i] == "left")
        {
            if (furniture.tag == "door")
            {
                furniture.transform.position = new Vector3((boundaries[i])[1], 0, ((boundaries[i])[0]) + ((boundaries[i])[2] / 2));
                furniture.gameObject.tag = "door";
            }
            furniture.transform.Rotate(0, 270, 0);
            if ((((boundaries[i])[3] > (boundaries[i])[2]) && (size.x < size.z)) || (((boundaries[i])[2] > (boundaries[i])[3]) && (size.z < size.x)))
            {
                rescale.x = (boundaries[i])[2] * rescale.x / size.x;
                rescale.z = (boundaries[i])[3] * rescale.z / size.z;
                rescale.y = Math.Min(rescale.x, rescale.z);
                furniture.transform.localScale = rescale;
            }
            else
            {
                rescale.x = (boundaries[i])[3] * rescale.x / size.x;
                rescale.z = (boundaries[i])[2] * rescale.z / size.z;
                rescale.y = Math.Min(rescale.x, rescale.z);
                furniture.transform.localScale = rescale;
            }
        }
        else
        {
            rescale.x = (boundaries[i])[3] * rescale.x / size.x;
            rescale.z = (boundaries[i])[2] * rescale.z / size.z;
            rescale.y = Math.Min(rescale.x, rescale.z);           
            furniture.transform.localScale = rescale;
        }

        if (furniture.tag == "door")
        {
            rescale.x = size.x;
            furniture.transform.localScale = rescale;
        }       
        

        size = furniture.GetComponent<Renderer>().bounds.size;

        if (size.y > 150)
        {
            rescale.y = 150 * rescale.y / size.y;
            furniture.transform.localScale = rescale;
            //furniture.transform.position = new Vector3(furniture.transform.position.x, 50, furniture.transform.position.z);
            size = furniture.GetComponent<Renderer>().bounds.size;               

        }

        furniture.name = i.ToString();
        furniture.AddComponent<MeshCollider>();
        furniture.AddComponent<BoxCollider>();
        furniture.AddComponent<ObjectClick>();

    }

   
}
