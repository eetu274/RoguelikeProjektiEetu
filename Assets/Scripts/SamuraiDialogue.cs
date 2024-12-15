using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;

public class SamuraiDialogue : MonoBehaviour
{
    FireShoot canShoot;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index = 0;

    public GameObject guideText;

    public GameObject boss;
    public GameObject bossHpBar;

    public float wordSpeed = 0.06f;
    public bool playerIsClose;


    void Start()
    {
        canShoot = GameObject.FindGameObjectWithTag("Player").GetComponent<FireShoot>();
        dialogueText.text = "";

    }
    // Update is called once per frame
    void Update()
    { 

        // Tulostetaan tekstit
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            canShoot.canShootFire = true;

            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }

        }
        // Painamalla Q skipataan dialogue
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }

    }

    // Dialoguen lopettaminen kesken
    public void RemoveText()
    {
        if (dialoguePanel != null)
        {
            dialogueText.text = "";
            dialoguePanel.SetActive(false);
        }
        index = 0;
    }

    // Kirjaimet tulee yksitellen dialogueen
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Seuraavaan puheeseen meno
    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            Vector2 bossSpawnPos = new Vector2(0.15f, 15);
            RemoveText();
            gameObject.SetActive(false);
            Instantiate(boss, bossSpawnPos, Quaternion.identity);
            bossHpBar.SetActive(true);

        }
    }

    // Tarvistetaan onko pelaaja lähellä objektia mille puhutaan
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;

            guideText.SetActive(true);
        }
    }

    // Kun pelaaja menee kauas puhuttavasta. Dialogue loppuu
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;

            guideText.SetActive(false);

            if (dialoguePanel != null)
            {
                RemoveText();
            }

        }
    }
}