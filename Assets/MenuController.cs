using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public Button start;
    public Button end;
    public InputField wallData;
    public InputField boundaryData;
    public InputField predictionData;
    public InputField directionData;
    public Text error;

    // Start is called before the first frame update
    void Start()
    {
        /*GameObject playerUI = GameObject.FindGameObjectWithTag("Canvas"); // The canvas
        start = playerUI.GetComponentInChildren<Button>();
        wallData = playerUI.GetComponentsInChildren<InputField>()[0];
        boundaryData = playerUI.GetComponentsInChildren<InputField>()[1];
        predictionData = playerUI.GetComponentsInChildren<InputField>()[2];
        directionData = playerUI.GetComponentsInChildren<InputField>()[3];*/

        error.text = "";

        start.onClick.AddListener(CreateFloorPlan);
        end.onClick.AddListener(EndApplication);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetString("wallData", (wallData.text).Trim());
        PlayerPrefs.SetString("boundaryData", (boundaryData.text).Trim());
        PlayerPrefs.SetString("predictionData", (predictionData.text).Trim());
        PlayerPrefs.SetString("directionData", (directionData.text).Trim());
    }

    void CreateFloorPlan()
    {
        if (File.Exists((wallData.text).Trim()) && File.Exists((boundaryData.text).Trim()) && File.Exists((predictionData.text).Trim()) && File.Exists((directionData.text).Trim()))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BuildFloorPlan");
        }
        else
        {
            error.text = "Error! Invalid file path. Try again!";
        }
        
    }

    void EndApplication()
    {
        Application.Quit();
    }
}
