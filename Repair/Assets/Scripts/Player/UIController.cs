using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject DeathScreenPanel;
    public GameObject EscMenuPanel;
    public IPopUp PopUpScreen;


    void Awake()
    {
        var scripts = GetComponentsInChildren<IInitable>(true);
        foreach (var script in scripts)
            script.Init();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TriggerInventory();

        if (Input.GetKeyDown(KeyCode.Escape))
            TriggerEscMenu();

        if (Input.GetKeyDown(KeyCode.F))//Remove
            TriggerDeathScreen();//Remove
    }

    bool isEscMenuVisible = false;
    void TriggerEscMenu()
    {
        isEscMenuVisible ^= true; //Trigger
        EscMenuPanel.SetActive(isEscMenuVisible);
        if (isEscMenuVisible)
            Time.timeScale = 0;
        else
            Time.timeScale = 1f;
    }

    bool isDeathScreenVisible = false;
    public void TriggerDeathScreen()
    {
        if (isInventoryVisible)
            TriggerInventory();

        PopUpScreen?.Close();

        isDeathScreenVisible ^= true; //Trigger
        DeathScreenPanel.SetActive(isDeathScreenVisible);
    }

    bool isInventoryVisible = false;
    void TriggerInventory()
    {
        if (isEscMenuVisible || isDeathScreenVisible || IsPopUpVisible)
            return;

        isInventoryVisible ^= true; //Trigger
        InventoryPanel.SetActive(isInventoryVisible);
    }

    public static bool IsPopUpVisible = false;
    public static void ShowPopUp(GameObject popup)
    {
        if (IsPopUpVisible)
            return;

        IsPopUpVisible = true;
        popup.SetActive(true);
    }
}
