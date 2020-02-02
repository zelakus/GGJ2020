using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWind : MonoBehaviour
{
    public AudioClip wind;
    AudioSource source;
    float nextPlay;
    public float volume = 0.2f;

    void Start()
    {
        source = GetComponent<AudioSource>();
        GetTime();
    }

    void GetTime()
    {
        nextPlay = Time.time + Random.value * 20f;
    }

    void Update()
    {
        if (Time.time >= nextPlay)
        {
            GetTime();
                nextPlay += 4f;
            source.PlayOneShot(wind, volume);
        }
    }
}
