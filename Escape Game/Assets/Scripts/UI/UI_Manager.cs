using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    //Inventory Canva
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
        inventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) ToggleInventoryMenu();
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseMenu();
    }
    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf == false)
            Time.timeScale = 0;
        else Time.timeScale = 1;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    public void ToggleInventoryMenu()
    {
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
    }
}
