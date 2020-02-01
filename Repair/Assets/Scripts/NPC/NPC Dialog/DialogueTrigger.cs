using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Trigger()
    {
        DialogueManager.StartDialogue(dialogue);
    }
}
