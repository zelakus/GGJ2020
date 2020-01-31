using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpType : MonoBehaviour, IPopUp
{
    public GameObject NextPopUp;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Close()
    {
        UIController.IsPopUpVisible = false;
        if (NextPopUp != null)
            Instantiate(NextPopUp);
        Destroy(gameObject);
    }

    public void ForceClose()
    {
        UIController.IsPopUpVisible = false;
        Destroy(gameObject);
    }
}
