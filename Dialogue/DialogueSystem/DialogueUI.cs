using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{       
    [SerializeField] private GameObject dialogueBox = null;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private bool automaticStep;
    [SerializeField] private float endStepTime;
    [SerializeField] private bool canCancel;

    public bool IsOpen { get; private set; }

    private TypeWriterEffect typeWriterEffect;
    private ResponseHandler responseHandler;
    private CharacterPortraitHandler charPortraitHandler;

    
    
    void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        charPortraitHandler = GetComponent<CharacterPortraitHandler>();
        CloseDialogue();

    }

    public void ShowDialogue(DialogueObject dialogueObject) 
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents) 
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            charPortraitHandler.ChangePortrait(int.Parse(dialogue.Substring(0, 1)));

            dialogue = dialogue.Remove(0, 1);
            
            yield return RunTypingEffect(dialogue);
            
            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
            yield return null;
            
            if (automaticStep) {
                yield return new WaitForSeconds(endStepTime);
            } else {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }

        if (dialogueObject.HasResponses) {
            responseHandler.ShowResponses(dialogueObject.Responses);
        } else {
            CloseDialogue();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue) 
    {
        typeWriterEffect.Run(dialogue, textLabel);

        while (typeWriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space) && canCancel) {
                typeWriterEffect.Stop();
            }
        }
    }

    public void CloseDialogue() 
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}