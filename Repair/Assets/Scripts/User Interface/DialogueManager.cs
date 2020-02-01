using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour, IInitable
{
    private static DialogueManager Instance;
    public TMP_Text NameHolder;
    public TMP_Text SentenceHolder;

    public void Init()
    {
        Instance = this;
    }

    private Queue<string> sentences;
    public static void StartDialogue(Dialogue dialogue)
    {
        //Get sentence queue
        Instance.sentences = new Queue<string>();
        foreach (var sentence in dialogue.sentences)
            Instance.sentences.Enqueue(sentence);

        //Set UI
        Instance.NameHolder.SetText(dialogue.name);
        Instance.DisplayNextSentence();

        //TODO animation
    }

    Coroutine typer;
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Stop old typer
        if (typer != null)
            StopCoroutine(typer);
        
        //Start new typer
        typer = StartCoroutine(TypeText(sentences.Dequeue()));
    }

    IEnumerator TypeText(string text)
    {
        Instance.SentenceHolder.SetText("");
        foreach (var letter in text.ToCharArray())
        {
            Instance.SentenceHolder.SetText(Instance.SentenceHolder.text + letter);
            yield return null;
        }

        typer = null;
    }

    void EndDialogue()
    {
        //TODO animation
    }
}
