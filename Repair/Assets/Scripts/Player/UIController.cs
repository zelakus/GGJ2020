using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public GameObject InventoryPanel;
    public GameObject DeathScreenPanel;
    public GameObject EscMenuPanel;
    public IPopUp PopUpScreen;

    void Awake()
    {
        Instance = this;
        var scripts = GetComponentsInChildren<IInitable>(true);
        foreach (var script in scripts)
            script.Init();
    }

    public bool IsBusy
    {
        get
        {
            return isEscMenuVisible || isDeathScreenVisible || isInventoryVisible || IsPopUpVisible;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TriggerInventory();

        if (Input.GetKeyDown(KeyCode.Escape))
            TriggerEscMenu();

        //Debug only!
        if (Input.GetKeyDown(KeyCode.F))
            TriggerDeathScreen();
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
        DialogueManager.EndDialogue();
    }

    bool isInventoryVisible = false;
    void TriggerInventory()
    {
        if (isEscMenuVisible || isDeathScreenVisible || IsPopUpVisible || DialogueManager.DialogueVisible)
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
