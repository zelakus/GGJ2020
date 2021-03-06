﻿using System;
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
    public static AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        hasWeapon = Convert.ToBoolean(PlayerPrefs.GetInt("hasWeapon", 0));
        Coins = PlayerPrefs.GetInt("coins", 0);
    }

    public void GenerateSword()
    {
        Instantiate(weaponPrefab, handTransform);
        hasWeapon = true;
    }
}

