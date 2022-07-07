﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject helpMenu;

    [Header("PauseMenu Settings")]
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonHelp;
    [SerializeField] private Button buttonQuit;

    [Header("Help Settings")]
    [SerializeField] private Button goBackButton;

    [Header("Victory")]
    [SerializeField] private Button returnMenuButton;
    [SerializeField] private Button nextLevelButton;

    //Extras
    private bool isActive;
    private bool mainMenuActive;

    void Start()
    {
        InputController.instance.OnPause += CheckIfPause;
        GoBack();
        ExitMenu();
        ButtonsListeners();

        nextLevelButton.gameObject.SetActive(!GameManager.instance.IsEndLevel);
    }

    private void CheckIfPause()
    {
        if (!isActive)
        {
            Pause();
        }
        else
        {
            if (!mainMenuActive)
            {
                GoBack();
            }
            else
            {
                ExitMenu();
            }
        }
    }

    private void ButtonsListeners()
    {
        buttonResume.onClick.AddListener(OnClickResumeHandler);
        buttonHelp.onClick.AddListener(OnClickHelpHandler);
        buttonQuit.onClick.AddListener(OnClickQuitHandler);
        goBackButton.onClick.AddListener(OnClickGoBackHandler);
        buttonMainMenu.onClick.AddListener(OnClickMenuHandler);
        returnMenuButton.onClick.AddListener(OnClickMenuHandler);
        nextLevelButton.onClick.AddListener(OnClickNextLevelHandler);
    }

    private void Pause()
    {
        GameManager.instance.Pause(true);
        isActive = true;
        mainMenuActive = true;
        pauseMenu.SetActive(true);
    }

    private void GoBack()
    {
        mainMenuActive = true;
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void ExitMenu()
    {
        GameManager.instance.Pause(false);
        isActive = false;
        mainMenuActive = false;
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    private void OnClickHelpHandler()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
        mainMenuActive = false;
    }

    private void OnClickResumeHandler()
    {
        ExitMenu();
    }

    private void OnClickMenuHandler()
    {
        SceneManager.LoadScene(GameManager.instance.MainMenuScene);
    }

    private void OnClickNextLevelHandler()
    {
        GameManager.instance.ChangeLevel();
    }

    private void OnClickGoBackHandler()
    {
        GoBack();
    }

    private void OnClickQuitHandler()
    {
        Application.Quit();
        Debug.Log("Se cierra el juego");
    }
}
