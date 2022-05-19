/*
 * PolyExtruder.cs
 *
 * Description: Class to demonstrate the application and functionalities of the PolyExtruder.cs script.
 * 
 * Supported Unity version: 2019.2.17f1 Personal (tested)
 *
 * Author: Nico Reski
 * Web: https://reski.nicoversity.com
 * Twitter: @nicoversity
 * GitHub: https://github.com/nicoversity
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Class to demonstrate the PolyExtruder class' functionalities.
/// </summary>
public class WallExtruder : MonoBehaviour
{
    // enum for selecting example data (to be selected via Unity Inspector)
    public Vector2[] Wall;
    string filepath; 
    // set up options (adjusted via Unity Inspector)
    public bool isOutlineRendered = false;
    public bool is3D = true;
    public bool enableMovement = false;
    public float extrusionHeight = 150;

    // reference to setup example poly extruder 
    private PolyExtruder polyExtruder;
    private GameObject[] gameObjects;


    void Start()
    {
        filepath = PlayerPrefs.GetString("wallData");
        var filelines =  File.ReadAllLines(@filepath);
        Wall = new Vector2[filelines.Length];
        int count = 0;
        int begin = 0;
        int size = 0;
        for (int i = 0; i < filelines.Length; i++)
        {            
            string coords = filelines[i];
            if (coords == "###" || coords == "####")
            {
                GameObject polyExtruderGO = new GameObject();
                polyExtruderGO.transform.parent = this.transform;

                if(coords == "####")
                {
                    polyExtruderGO.tag = "window";
                }

                var WallPiece = new Vector2[size];
                for (int j = 0; j<size; j++)
                {
                    WallPiece[j] = Wall[j + begin + 1];
                }
                // add PolyExtruder script to newly created GameObject and keep track of its reference
                polyExtruder = polyExtruderGO.AddComponent<PolyExtruder>();

                // global PolyExtruder configurations
                polyExtruder.isOutlineRendered = isOutlineRendered;
                polyExtruderGO.name = "Wall" + count;
                polyExtruder.createPrism(polyExtruderGO.name, extrusionHeight, WallPiece, Color.grey, is3D);
                count++;
                begin = i;
                size = 0;
            }
            else
            {                
                string[] coord = coords.Split(' ');
                float coord0 = float.Parse(coord[0], CultureInfo.InvariantCulture);
                float coord1 = float.Parse(coord[1], CultureInfo.InvariantCulture);
                Wall[i] = new Vector2(coord0, coord1);
                size++;
            }            
        }

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.tag == "window")
            {
                GameObject child = go.transform.GetChild(2).gameObject;
                child.SetActive(false);
            }
                
        }
           


    }

    void Update()
    {
        // if movement is selected (via Unity Inspector), oscillate height accordingly
        if(enableMovement)
        {
            polyExtruder.updateHeight((Mathf.Sin(Time.time) + 1.0f) * this.extrusionHeight);
        }

        
    }

}
