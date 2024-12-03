using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Key_Indicator : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the cube around its Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
