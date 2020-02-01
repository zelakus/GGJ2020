using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(DeathHandler))]
class PlayerController : MonoBehaviour
{
    public static int Coins;


    private void Awake()
    {
       Coins = PlayerPrefs.GetInt("coins", 0);
    }
}

