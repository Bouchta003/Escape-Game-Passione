using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Storable"))
        {
            Debug.Log(other.gameObject.name+ " is storable");
        }
    }
}

