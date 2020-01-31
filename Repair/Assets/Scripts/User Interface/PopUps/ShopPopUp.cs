using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopUp : MonoBehaviour, IPopUp
{

    void Start()
    {
        
    }

    void Update()
    {
        //Close
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
            return;
        }

    }

    public void Close()
    {
        UIController.IsPopUpVisible = false;
        Destroy(gameObject);
    }

    public void ForceClose()
    {
        UIController.IsPopUpVisible = false;
        Destroy(gameObject);
    }
}
