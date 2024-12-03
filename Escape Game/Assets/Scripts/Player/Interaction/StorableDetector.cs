using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableDetector : MonoBehaviour
{
    public GameObject prefabToInstantiate; // Assign your prefab in the Inspector
    private GameObject currentPrefabInstance; // Tracks the currently instantiated prefab
    [SerializeField] float prefabHeight = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Storable"))
        {
            if (currentPrefabInstance != null)
            {
                Destroy(currentPrefabInstance);
            }

            // Instantiate a new prefab above the collided object
            Vector3 spawnPosition = other.transform.position + Vector3.up * prefabHeight;
            currentPrefabInstance = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
        }
    }
}
