using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private PlayerCharacterController playerCharacter;
    [SerializeField] private CharacterPanelController playerPanel;
    [SerializeField] private SpellPanelController spellPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Sprite[] spellImages = new Sprite[2]; 
    bool firstFrame = true;
    private void Start()
    {
        
    }

    void Update()
    {
        if (firstFrame)
        {
            playerCharacter.GetCharacter().OnStatusChange.AddListener(UpdateStatus);
            playerCharacter.GetCharacter().OnSpellsChange.AddListener(UpdateQuickBar);
            UpdateStatus();
            UpdateQuickBar();
            firstFrame = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            cursor.transform.position = playerCharacter.transform.position;
            cursor.SetActive(!cursor.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
        }
    }

    private void UpdateStatus()
    {
        playerPanel.Name = playerCharacter.GetCharacter().Name;
        playerPanel.MaxHP = (int)playerCharacter.GetCharacter().MaxHP;
        playerPanel.HP = (int)playerCharacter.GetCharacter().HP;
        playerPanel.MaxFP = (int)playerCharacter.GetCharacter().MaxFP;
        playerPanel.FP = (int)playerCharacter.GetCharacter().FP;
    }

    private void UpdateQuickBar()
    {
        for(int i = 0; i < playerCharacter.GetCharacter().Spells.Length; i++)
        {
            if (playerCharacter.GetCharacter().Spells[i] != null)
            {
                spellPanel.Icons[i].sprite = spellImages[1];
            }
            else spellPanel.Icons[i].sprite = spellImages[0];
        }
    }

    public void UnloadGame()
    {
        SceneManager.UnloadSceneAsync("TestScene");
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
    }
}
