using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{



    private void Awake()
    {
        GetComponent<Health>().Died += Death;
    }
    

    void Death()
    {
        UIController.Instance.TriggerDeathScreen();
    }
}
