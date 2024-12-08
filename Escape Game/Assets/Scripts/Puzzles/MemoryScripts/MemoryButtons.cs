using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryButtons : MonoBehaviour
{
    public TextMeshProUGUI redButton;
    public TextMeshProUGUI greenButton;
    public TextMeshProUGUI blueButton;
    // Start is called before the first frame update
    void Start()
    {
        redButton.text = "Red";
        greenButton.text = "Green";
        blueButton.text = "Blue";
    }

}
