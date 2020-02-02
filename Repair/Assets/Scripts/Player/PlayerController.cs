using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(DeathHandler))]
class PlayerController : MonoBehaviour
{
    public static int Coins;
    [SerializeField] GameObject weaponPrefab = null;
    [SerializeField] Transform handTransform = null;
    public static bool hasWeapon = false;

    private void Awake()
    {
        hasWeapon = Convert.ToBoolean(PlayerPrefs.GetInt("hasWeapon", 0));
        Coins = PlayerPrefs.GetInt("coins", 0);
    }

    private void GenerateSword()
    {
        Instantiate(weaponPrefab, handTransform);
        hasWeapon = true;
    }
}

