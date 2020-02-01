using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNPC : MonoBehaviour
{
    public DialogueTrigger dialogue;
    public Animator Pop;

    void Start()
    {
        GetComponentInChildren<InteractionTrigger>().Triggered = AreaTriggered;       
    }

    bool inArea;
    void AreaTriggered(bool inside)
    {
        if (Pop == null)
            return;

        inArea = inside;
        Pop.SetBool("show", inside);
    }

    void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.Q))
            dialogue.Trigger();
    }
}
