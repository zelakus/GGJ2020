using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public GameObject InventoryPanel;
    public GameObject ShopPanel;
    public GameObject DeathScreenPanel;
    public GameObject EscMenuPanel;

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
            return isEscMenuVisible || isDeathScreenVisible || isInventoryVisible || isShopVisible;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TriggerInventory();

        if (Input.GetKeyDown(KeyCode.Escape))
            TriggerEscMenu();
    }

    //TODO will be Private again.
    public bool isEscMenuVisible = false;
    public void TriggerEscMenu()
    {
        isEscMenuVisible ^= true; //Trigger
        EscMenuPanel.SetActive(isEscMenuVisible);
        if (isEscMenuVisible)
            Time.timeScale = 0;
        else
            Time.timeScale = 1f;
    }
    //TODO will be private again
    public bool isDeathScreenVisible = false;
    public void TriggerDeathScreen()
    {
        if (isInventoryVisible)
            TriggerInventory();

        isDeathScreenVisible ^= true; //Trigger
        DeathScreenPanel.SetActive(isDeathScreenVisible);
        DialogueManager.EndDialogue();
    }

    bool isInventoryVisible = false;
    void TriggerInventory()
    {
        if (isEscMenuVisible || isDeathScreenVisible || DialogueManager.DialogueVisible || isShopVisible)
            return;

        isInventoryVisible ^= true; //Trigger
        InventoryPanel.SetActive(isInventoryVisible);
    }

    bool isShopVisible = false;
    public static void TriggerShop()
    {
        if (Instance.isEscMenuVisible || Instance.isDeathScreenVisible || DialogueManager.DialogueVisible || Instance.isInventoryVisible)
            return;

        Instance.isShopVisible ^= true; //Trigger
        Instance.ShopPanel.SetActive(Instance.isShopVisible);
    }
}
