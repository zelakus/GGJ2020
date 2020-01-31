using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] uint maxHealth = 5;

    private uint currentHealth;
    public uint CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = (uint)Mathf.Clamp(value, 0, maxHealth);

            (currentHealth == 0 ? Died : HealthUpdated)?.Invoke();
        }
    }
    public static Action HealthUpdated;
    public static Action Died;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

}
