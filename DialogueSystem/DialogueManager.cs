using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance{set;get;}
    public TMP_Text textDisplay = null;
    public string[] sentences;
    public int index = 0;
    public float typingSpeed = 1f;
    private IEnumerator coroutine;
    public bool isActive = false;
    public bool isTyping= false;
    public GameObject dialogueBox = null;

    void Awake() {
        Instance = this;
        isActive = false;
        dialogueBox.SetActive(isActive);
    }

    void Update() {
        if (Input.GetButtonDown("Jump") && isActive) {
            if (isTyping) {
                FinishSentence();
            } else {
                NextSentence();
            }
        }
    }

    IEnumerator Type() {
        isTyping = true;
        foreach (char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            AudioManager.PlayTypeAudio();
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void FinishSentence() {
        StopCoroutine(coroutine);
        isTyping = false;
        textDisplay.text = sentences[index];
    }

    public void StartDialogue() {
        index = 0;
        textDisplay.text = "";
        isActive = true;
        dialogueBox.SetActive(isActive);
        StartCoroutine(coroutine =  Type());
    }

    // Returns true when the dialog is over
    private void NextSentence() {
        textDisplay.text = "";
        if (index < sentences.Length - 1) {
            index++;
            StartCoroutine(coroutine =  Type());
        } else {
            isActive = false;
            dialogueBox.SetActive(isActive);
        }
    }

}
