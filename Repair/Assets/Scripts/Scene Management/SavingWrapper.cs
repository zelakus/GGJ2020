using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";
    [SerializeField] float fadeInTime = 0.2f;

    private void Awake()
    {
        
    }

    //TODO bunları daha sonra scene Changer a al ,Take theese to Scene Changer
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        UIChanger();
    }



    public void LoadScene()
    {
        StartCoroutine(LoadLastScene());
        UIChanger();
       
    }



    public IEnumerator LoadLastScene()
    {
        //TODO bunu persistent objecte yerleştir. Make it Persistent Object maybe along with Menu in Main Menu Scene
        DontDestroyOnLoad(this.gameObject);
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        Fader fader = FindObjectOfType<Fader>();
        fader.FadeOutImmediate();
        yield return fader.FadeIn(fadeInTime);
        Destroy(gameObject);
       
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Load();
            Inventory.Deserialize();
            ShopManager.Serialize();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
            Inventory.Serialize();
            ShopManager.Serialize();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Delete();
        }
    }
    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }
    public void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
        UIChanger();
    }

    public void Delete()
    {
        GetComponent<SavingSystem>().Delete(defaultSaveFile);
    }


    private static void UIChanger()
    {
        UIController.Instance.EscMenuPanel.SetActive(false);
        UIController.Instance.DeathScreenPanel.SetActive(false);
        Time.timeScale = 1f;
        UIController.Instance.isEscMenuVisible = false;
        UIController.Instance.isDeathScreenVisible = false;
    }
}
