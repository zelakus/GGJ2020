using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(DeathHandler))]
class PlayerController : MonoBehaviour
{
    public static int Coins;
    [SerializeField] GameObject weaponPrefab = null;
    [SerializeField] Transform handTransform = null;


    private void Awake()
    {
       Coins = PlayerPrefs.GetInt("coins", 0);
    }

    private void Start()
    {
        Instantiate(weaponPrefab, handTransform);
    }
}

