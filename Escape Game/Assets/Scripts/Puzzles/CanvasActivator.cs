using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public GameObject canvasToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que le Player a le tag "Player"
        {
            canvasToActivate.SetActive(true);
        }
    }
}
