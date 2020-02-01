using System;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public Action<bool> Triggered { get; set; }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            Triggered?.Invoke(true);
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
            Triggered?.Invoke(false);
    }
}
