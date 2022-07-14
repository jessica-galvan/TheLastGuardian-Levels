using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelName;
        public string nextLevel;
        public bool isEnd;
    }

    [Header("SceneNames")]
    [SerializeField] private List<Level> levels;
    private int currentIndexLevel;

    //SINGLETON
    public static GameManager instance;

    //PROPIEDADES
    public bool IsGameFreeze { get; private set; }
    public string MainMenuScene => levels[0].levelName;
    public bool IsEndLevel => levels[currentIndexLevel].isEnd;

    //EVENTS
    public Action OnPause;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Debug.Assert(levels.Count > 0, "There are no levels, game will not work");

        var initialScene = SceneManager.GetActiveScene().name;
        if (initialScene != levels[0].levelName)
        {
            for (int i = 1; i < levels.Count; i++)
            {
                if(initialScene == levels[i].levelName)
                {
                    currentIndexLevel = i;
                    break;
                }
            }
        } 
    }

    private void Start()
    {
        InputController.instance.OnChangeToPreviousLevel += ChangeToPreviousLevel;
        InputController.instance.OnChangeToNextLevel += ChangeToNextLevel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene(levels[1].levelName);
        else if (Input.GetKeyDown(KeyCode.F2)) SceneManager.LoadScene(levels[2].levelName);
    }

    public void Pause(bool value)
    {
        IsGameFreeze = value;
        if (value)
        {
            Time.timeScale = 0;
            //TODO: lower music
        }
        else
        {
            Time.timeScale = 1;
            //TODO: subir musica
        }         
    }

    public void ChangeLevel()
    {
        if (levels[currentIndexLevel].isEnd)
        {
            Debug.LogError("Something is wrong, we shouldn´t be calling the next level if it´s the end level");
        }
        else
        {
            currentIndexLevel++;
            SceneManager.LoadScene(levels[currentIndexLevel - 1].nextLevel);
        }
    }

    private void ChangeToPreviousLevel()
    {
        if (currentIndexLevel == 0 || currentIndexLevel == 1) return; //if it´s the first level or menu...
        currentIndexLevel--;
        SceneManager.LoadScene(levels[currentIndexLevel].levelName);
    }

    private void ChangeToNextLevel()
    {
        if (levels[currentIndexLevel].isEnd) return;
        ChangeLevel();
    }
}
