using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text nameText;
    public Text dialogueText;
    public GameObject sdButton;
    public GameObject cButton;

    [Header("Set Dynamically")]
    public string name; 

    //public Animator animator;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();

        sdButton.SetActive(true);
        cButton.SetActive(false);
        dialogueText.text = "";
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //animator.SetBool("IsOpen", true);

        sdButton.SetActive(false);
        cButton.SetActive(true);

        Debug.Log("nametext " + dialogue.name);

        name = nameText.text;
        nameText.text = "";

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("Done.");
        //animator.SetBool("IsOpen", false);
        dialogueText.text = "";
        nameText.text = name;

        sdButton.SetActive(true);
        cButton.SetActive(false);

        SceneManager.LoadScene("Combat", LoadSceneMode.Single);
    }

}