using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IInitable
{
    private static DialogueManager Instance;
    public static bool DialogueVisible { get; private set; }
    public TMP_Text NameHolder;
    public TMP_Text SentenceHolder;
    public Animator animator;

    public void Init()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && DialogueVisible)
            DisplayNextSentence();
    }

    private Queue<string> sentences;
    public static void StartDialogue(Dialogue dialogue)
    {
        //Cancel if there is already a dialogue
        if (DialogueVisible || UIController.Instance.IsBusy)
            return;

        DialogueVisible = true;

        //Get sentence queue
        Instance.sentences = new Queue<string>();
        foreach (var sentence in dialogue.sentences)
            Instance.sentences.Enqueue(sentence);

        //Set UI
        Instance.NameHolder.SetText(dialogue.name);
        Instance.DisplayNextSentence();

        //Animation
        Instance.animator.SetBool("show", true);
    }

    Coroutine typer;
    public void DisplayNextSentence()
    {
        EventSystem.current.SetSelectedGameObject(null);
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

    string ctext = "";
    IEnumerator TypeText(string text)
    {
        ctext = "";
        foreach (var letter in text.ToCharArray())
        {
            ctext += letter;
            Instance.SentenceHolder.SetText(text);
            yield return null;
        }

        typer = null;
    }

    public static void EndDialogue()
    {
        DialogueVisible = false;
        //Animation
        Instance.animator.SetBool("show", false);
    }
}
