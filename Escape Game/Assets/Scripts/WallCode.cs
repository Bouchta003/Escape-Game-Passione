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
        //Checking if the puzzle is resolved (for test purposes the 'E' key is pressed)

        //Assigning the Material to the bricks of the codeSequence
    }
}
