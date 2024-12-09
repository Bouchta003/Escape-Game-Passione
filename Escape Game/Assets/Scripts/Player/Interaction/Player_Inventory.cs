using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public static List<string> inventory = new List<string>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            string inventoryDisplay = "Inventory contains : ";
            foreach(string obj in inventory)
            {
                inventoryDisplay += obj;
                inventoryDisplay += " ";
            }
            Debug.Log(inventoryDisplay);
        }
    }
}
