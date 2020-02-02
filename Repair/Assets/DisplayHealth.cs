using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public uint CurrentHealth;
    public uint numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    private void Update()
    {
        CurrentHealth = GameObject.FindWithTag("Player").GetComponent<Health>().CurrentHealth;

        if (CurrentHealth> numOfHearts)
        {
            CurrentHealth = numOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i<CurrentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i<numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }


        }
    }
}
