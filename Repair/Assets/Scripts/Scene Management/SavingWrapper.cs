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
        //StartCoroutine(LoadLastScene());
        //if (SceneManager.GetActiveScene().buildIndex!=0)
        //{
        //    Load();
        //}
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadLastScene());
        
    }

    public IEnumerator LoadLastScene()
    {
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        Fader fader = FindObjectOfType<Fader>();
        fader.FadeOutImmediate();
        yield return fader.FadeIn(fadeInTime);
        
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
    }

    public void Delete()
    {
        GetComponent<SavingSystem>().Delete(defaultSaveFile);
    }
}
