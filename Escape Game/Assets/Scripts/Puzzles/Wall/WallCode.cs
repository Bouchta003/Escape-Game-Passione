using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCode : MonoBehaviour
{
    //The materials that will be used on the wall : defaultWallMaterial -> unresolved puzzle // codeWallMaterial -> resolved puzzle
    [SerializeField] Material defaultWallMaterial;
    [SerializeField] Material codeWallMaterial;

    //This string list will be changeable in the editor for test purposes
    [SerializeField] List<string> codeSequence = new List<string>();
    public bool Success = true;

    //The parent game object (wall) with all its childs (X.Y) where X is the row and Y the column of the brick.
    [SerializeField] GameObject _wall;

    private void Start()
    {
        //Assigning all the bricks the defaultWallMaterial
        if (_wall != null)
        {
            foreach (Transform child in _wall.transform)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = defaultWallMaterial;
                }
            }
        }
        else
        {
            Debug.LogWarning("_wall GameObject is not assigned in the inspector.");
        }
    }
    private void Update()
    {
        //Condition to verify if the necessary previous puzzles were completed, if they were show the code. (For testing purposes pressing 'E' key will trigger the code)
        if (Input.GetKeyDown(KeyCode.W))
        { Success = true; }
        if (Success) { 
            if (_wall != null)
            {
                foreach (Transform child in _wall.transform)
                {
                    if (codeSequence.Contains(child.name))
                    {
                        Renderer renderer = child.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material = codeWallMaterial;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("_wall GameObject is not assigned in the inspector.");
            }
        }
    }
}
