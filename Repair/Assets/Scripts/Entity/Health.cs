using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] uint maxHealth = 5;

    [SerializeField] uint currentHealth;
    public uint CurrentHealth
    {
        //TODO Buraya göz gezdir. Death Handlerdan sadece player çekiyor niyeyse. Check again Only player.
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = (uint)Mathf.Clamp(value, 0, maxHealth);

            //(currentHealth == 0 ? Died : HealthUpdated)?.Invoke();
            if (currentHealth == 0)
            {
                if (gameObject.tag != "Player")
                {
                    Destroy(gameObject);
                }
                else
                {
                    Died();
                }

            }
            //else
            //    HealthUpdated();
            
        }
    }
    //public event Action HealthUpdated;
    public event Action Died;

    private void Awake()
    {
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
        
    }

    public object CaptureState()
    {
        return currentHealth;
    }

    public void RestoreState(object state)
    {
        CurrentHealth= (uint)state;
    }
}
