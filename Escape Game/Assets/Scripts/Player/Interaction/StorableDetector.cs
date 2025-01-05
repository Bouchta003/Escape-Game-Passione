using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableDetector : MonoBehaviour
{
    public GameObject prefabToInstantiate; // Assign your prefab in the Inspector
    private GameObject currentPrefabInstance; // Tracks the currently instantiated prefab
    private GameObject currentStorableObject; // Tracks the current "Storable" object
    [SerializeField] float prefabHeight = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Storable"))
        {
            // Destroy the existing prefab if it exists
            if (currentPrefabInstance != null)
            {
                Destroy(currentPrefabInstance);
            }

            // Instantiate a new prefab above the collided object
            Vector3 spawnPosition = other.transform.position + Vector3.up * prefabHeight;
            currentPrefabInstance = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);

            // Track the current storable object
            currentStorableObject = other.gameObject;
        }
    }

    private void Update()
    {
        // Check if the E key is pressed and there's a storable object to destroy
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentStorableObject != null)
            {
                Player_Inventory.inventory.Add(currentStorableObject.name);
                Destroy(currentStorableObject); // Destroy the storable object
                currentStorableObject = null;  // Clear the reference
            }

            if (currentPrefabInstance != null)
            {
                Destroy(currentPrefabInstance); // Destroy the prefab
                currentPrefabInstance = null;  // Clear the reference
            }
        }
    }
}
